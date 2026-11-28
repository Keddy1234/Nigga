using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Hotel.Dominio.Entidades;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Api.Servicios;

public sealed record SesionHuesped(int ClienteId, string Email, string Jti, DateTimeOffset ExpiraEn);

public sealed class SeguridadApiServicio
{
    private const string AdminKeyDefecto = "PosadaRoble-Admin-2026-Key!";
    private const string ClaveTokenDefecto = "PosadaDelRoble-HMAC-SHA256-SessionKey-2026-CR-9f8e7d6c5b4a";

    private readonly byte[] _adminKeyBytes;
    private readonly byte[] _claveHmacBytes;
    private readonly int _duracionMinutos;
    private readonly ConcurrentDictionary<string, DateTimeOffset> _revocados = new();

    [ActivatorUtilitiesConstructor]
    public SeguridadApiServicio(IConfiguration config)
        : this(
            config["Seguridad:AdminApiKey"] ?? AdminKeyDefecto,
            config["Seguridad:ClaveToken"] ?? ClaveTokenDefecto,
            int.TryParse(config["Seguridad:DuracionSesionMinutos"], out var min) && min > 0 ? min : 30)
    {
    }

    public SeguridadApiServicio(string adminApiKey, string claveToken, int duracionMinutos = 30)
    {
        _adminKeyBytes = Encoding.UTF8.GetBytes(string.IsNullOrWhiteSpace(adminApiKey) ? AdminKeyDefecto : adminApiKey);
        _claveHmacBytes = Encoding.UTF8.GetBytes(string.IsNullOrWhiteSpace(claveToken) ? ClaveTokenDefecto : claveToken);
        _duracionMinutos = duracionMinutos > 0 ? duracionMinutos : 30;
    }

    public bool EsAdmin(HttpRequest request)
    {
        string? valor = request.Headers["X-Hotel-Admin-Key"].FirstOrDefault();
        if (string.IsNullOrEmpty(valor))
            return false;

        byte[] recibida = Encoding.UTF8.GetBytes(valor);
        return recibida.Length == _adminKeyBytes.Length
               && CryptographicOperations.FixedTimeEquals(recibida, _adminKeyBytes);
    }

    public (string Token, DateTimeOffset ExpiraEn) EmitirTokenHuesped(Cliente cliente)
    {
        var ahora = DateTimeOffset.UtcNow;
        var expira = ahora.AddMinutes(_duracionMinutos);
        var payload = new TokenPayload
        {
            cid = cliente.Id,
            email = (cliente.Email ?? string.Empty).Trim().ToLowerInvariant(),
            jti = Guid.NewGuid().ToString("N"),
            exp = expira.ToUnixTimeSeconds()
        };

        byte[] payloadBytes = JsonSerializer.SerializeToUtf8Bytes(payload);
        string payloadB64 = WebEncoders.Base64UrlEncode(payloadBytes);
        byte[] firma = HMACSHA256.HashData(_claveHmacBytes, Encoding.UTF8.GetBytes(payloadB64));
        string firmaB64 = WebEncoders.Base64UrlEncode(firma);
        return ($"{payloadB64}.{firmaB64}", expira);
    }

    public SesionHuesped? ValidarTokenHuesped(HttpRequest request)
    {
        string? auth = request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(auth) || !auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return null;

        string token = auth["Bearer ".Length..].Trim();
        return ValidarTokenRaw(token);
    }

    public SesionHuesped? ValidarTokenRaw(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        int punto = token.IndexOf('.');
        if (punto <= 0 || punto >= token.Length - 1)
            return null;

        string payloadB64 = token[..punto];
        string firmaB64 = token[(punto + 1)..];

        try
        {
            byte[] firmaRecibida = WebEncoders.Base64UrlDecode(firmaB64);
            byte[] firmaEsperada = HMACSHA256.HashData(_claveHmacBytes, Encoding.UTF8.GetBytes(payloadB64));
            if (firmaRecibida.Length != firmaEsperada.Length
                || !CryptographicOperations.FixedTimeEquals(firmaRecibida, firmaEsperada))
            {
                return null;
            }

            byte[] payloadBytes = WebEncoders.Base64UrlDecode(payloadB64);
            var payload = JsonSerializer.Deserialize<TokenPayload>(payloadBytes);
            if (payload == null || payload.cid <= 0 || string.IsNullOrWhiteSpace(payload.jti))
                return null;

            var expiraEn = DateTimeOffset.FromUnixTimeSeconds(payload.exp);
            if (expiraEn <= DateTimeOffset.UtcNow)
                return null;

            if (_revocados.ContainsKey(payload.jti))
                return null;

            return new SesionHuesped(payload.cid, payload.email ?? string.Empty, payload.jti, expiraEn);
        }
        catch
        {
            return null;
        }
    }

    public void RevocarToken(HttpRequest request)
    {
        var sesion = ValidarTokenHuesped(request);
        if (sesion == null)
            return;

        _revocados[sesion.Jti] = sesion.ExpiraEn;
        DepurarExpirados();
    }

    private void DepurarExpirados()
    {
        var ahora = DateTimeOffset.UtcNow;
        foreach (var kv in _revocados)
        {
            if (kv.Value <= ahora)
                _revocados.TryRemove(kv.Key, out _);
        }
    }

    private sealed class TokenPayload
    {
        public int cid { get; set; }
        public string? email { get; set; }
        public string jti { get; set; } = string.Empty;
        public long exp { get; set; }
    }
}
