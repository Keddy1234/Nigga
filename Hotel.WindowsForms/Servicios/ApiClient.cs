using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;

namespace Hotel.WindowsForms.Servicios
{
    public static class ApiClient
    {
        private static readonly HttpClient _http;
        private static readonly JsonSerializerOptions _jsonOptions;

        public static List<Reservacion> ReservacionesLocales { get; } = new()
        {
            new Reservacion
            {
                Id = 1,
                Codigo = "RES-2026-0001",
                HabitacionId = 2,
                ClienteId = 1,
                FechaInicio = DateTime.Today.AddDays(-1),
                FechaFin = DateTime.Today.AddDays(2),
                Estado = EstadoReservacion.EnCurso,
                CostoTotal = 261000m,
                CantidadHuespedes = 1,
                Cliente = new Cliente { Id = 1, Nombres = "Carlos", Apellidos = "Mendoza Delgado", NumeroDocumento = "101110111", Telefono = "88881111" },
                Habitacion = new Habitacion { Id = 2, Numero = "102", Piso = 1 }
            },
            new Reservacion
            {
                Id = 2,
                Codigo = "RES-2026-0002",
                HabitacionId = 5,
                ClienteId = 2,
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(4),
                Estado = EstadoReservacion.Confirmada,
                CostoTotal = 549840m,
                CantidadHuespedes = 2,
                Cliente = new Cliente { Id = 2, Nombres = "Ana", Apellidos = "Salazar Ruiz", NumeroDocumento = "202220222", Telefono = "88882222" },
                Habitacion = new Habitacion { Id = 5, Numero = "201", Piso = 2 }
            },
            new Reservacion
            {
                Id = 3,
                Codigo = "RES-2026-0003",
                HabitacionId = 7,
                ClienteId = 3,
                FechaInicio = DateTime.Today.AddDays(-2),
                FechaFin = DateTime.Today.AddDays(1),
                Estado = EstadoReservacion.EnCurso,
                CostoTotal = 1308480m,
                CantidadHuespedes = 4,
                Cliente = new Cliente { Id = 3, Nombres = "Roberto", Apellidos = "Gomez Solano", NumeroDocumento = "402220333", Telefono = "88883333" },
                Habitacion = new Habitacion { Id = 7, Numero = "203", Piso = 2 }
            }
        };

        public static List<PedidoRestaurante> PedidosLocales { get; } = new()
        {
            new PedidoRestaurante
            {
                Id = 1,
                ReservacionId = 1,
                FechaPedido = DateTime.Today.AddHours(9),
                EstadoPedido = EstadoPedido.Recibido,
                EsRoomService = true,
                Instrucciones = "[Room Service] 1x Asado del Roble, 1x Fresco Natural de Cas",
                Total = 33000m
            }
        };

        static ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5158/"),
                Timeout = TimeSpan.FromSeconds(5)
            };
            _http.DefaultRequestHeaders.Add("X-Hotel-Admin-Key", "PosadaRoble-Admin-2026-Key!");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static async Task<T?> GetAsync<T>(string ruta)
        {
            var resp = await _http.GetAsync(ruta);
            await AsegurarExitoAsync(resp);
            return await resp.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }

        public static async Task<TResp?> PostAsync<TReq, TResp>(string ruta, TReq datos)
        {
            var resp = await _http.PostAsJsonAsync(ruta, datos, _jsonOptions);
            await AsegurarExitoAsync(resp);
            if (resp.Content.Headers.ContentLength == 0) return default;
            return await resp.Content.ReadFromJsonAsync<TResp>(_jsonOptions);
        }

        public static async Task PostAsync<TReq>(string ruta, TReq datos)
        {
            var resp = await _http.PostAsJsonAsync(ruta, datos, _jsonOptions);
            await AsegurarExitoAsync(resp);
        }

        public static async Task PostSinCuerpoAsync(string ruta)
        {
            var resp = await _http.PostAsync(ruta, null);
            await AsegurarExitoAsync(resp);
        }

        public static async Task PutAsync<TReq>(string ruta, TReq datos)
        {
            var resp = await _http.PutAsJsonAsync(ruta, datos, _jsonOptions);
            await AsegurarExitoAsync(resp);
        }

        public static async Task PatchAsync(string ruta)
        {
            var req = new HttpRequestMessage(HttpMethod.Patch, ruta);
            var resp = await _http.SendAsync(req);
            await AsegurarExitoAsync(resp);
        }

        public static async Task DeleteAsync(string ruta)
        {
            var resp = await _http.DeleteAsync(ruta);
            await AsegurarExitoAsync(resp);
        }

        private static async Task AsegurarExitoAsync(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode) return;

            string detalle = await resp.Content.ReadAsStringAsync();
            try
            {
                using var doc = JsonDocument.Parse(detalle);
                if (doc.RootElement.TryGetProperty("mensaje", out var msg))
                    throw new Exception(msg.GetString());
            }
            catch (JsonException)
            {
            }

            throw new Exception(string.IsNullOrWhiteSpace(detalle)
                ? $"Error HTTP {(int)resp.StatusCode}"
                : detalle);
        }
    }
}
