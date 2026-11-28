using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Reglas;

public static class ConversorMoneda
{
    public const decimal TipoCambioUsdDefecto = 515.00m;

    private static decimal NormalizarTipoCambio(decimal tipoCambio)
        => tipoCambio > 0 ? tipoCambio : TipoCambioUsdDefecto;

    public static decimal ColonesADolares(decimal montoCrc, decimal tipoCambio = TipoCambioUsdDefecto)
        => Math.Round(montoCrc / NormalizarTipoCambio(tipoCambio), 2, MidpointRounding.AwayFromZero);

    public static decimal DolaresAColones(decimal montoUsd, decimal tipoCambio = TipoCambioUsdDefecto)
        => Math.Round(montoUsd * NormalizarTipoCambio(tipoCambio), 2, MidpointRounding.AwayFromZero);

    public static decimal ConvertirDesdeColones(decimal montoCrc, Moneda destino, decimal tipoCambio = TipoCambioUsdDefecto)
        => destino == Moneda.USD
            ? ColonesADolares(montoCrc, tipoCambio)
            : Math.Round(montoCrc, 2, MidpointRounding.AwayFromZero);

    public static decimal ConvertirAColones(decimal monto, Moneda origen, decimal tipoCambio = TipoCambioUsdDefecto)
        => origen == Moneda.USD
            ? DolaresAColones(monto, tipoCambio)
            : Math.Round(monto, 2, MidpointRounding.AwayFromZero);

    public static string Simbolo(Moneda moneda)
        => moneda == Moneda.USD ? "$" : "₡";

    public static string Formatear(decimal monto, Moneda moneda)
        => $"{Simbolo(moneda)}{monto:N2}";

    public static Moneda DetectarMonedaDesdeObservaciones(string? observaciones)
    {
        if (string.IsNullOrWhiteSpace(observaciones))
            return Moneda.CRC;

        return observaciones.Contains("Moneda: USD", StringComparison.OrdinalIgnoreCase)
               || observaciones.Contains("[USD]", StringComparison.OrdinalIgnoreCase)
            ? Moneda.USD
            : Moneda.CRC;
    }

    public static string AdjuntarEtiquetaMoneda(string? observaciones, Moneda moneda, decimal tipoCambio = TipoCambioUsdDefecto)
    {
        decimal tc = NormalizarTipoCambio(tipoCambio);
        string etiqueta = moneda == Moneda.USD
            ? $"Moneda: USD (TC ₡{tc:N2})"
            : "Moneda: CRC";

        string obs = (observaciones ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(obs))
            return etiqueta;

        if (obs.Contains("Moneda: USD", StringComparison.OrdinalIgnoreCase)
            || obs.Contains("Moneda: CRC", StringComparison.OrdinalIgnoreCase))
        {
            return obs.Length <= 500 ? obs : obs[..500];
        }

        string separador = " | ";
        string combinada = $"{obs}{separador}{etiqueta}";
        if (combinada.Length <= 500)
            return combinada;

        int maxObs = Math.Max(0, 500 - separador.Length - etiqueta.Length);
        return $"{obs[..maxObs].TrimEnd()}{separador}{etiqueta}";
    }
}
