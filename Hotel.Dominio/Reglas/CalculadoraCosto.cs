using Hotel.Dominio.DTOs;
using Hotel.Dominio.ValueObjects;

namespace Hotel.Dominio.Reglas;

// Costo = (Noches * TarifaBase * FactorTemporada) + RecargoPersonasExtra - DescuentoCliente, mas IVA.
public static class CalculadoraCosto
{
    public const decimal Iva = 0.16m;
    public const decimal PorcentajeAnticipo = 0.30m;
    public const decimal RecargoFinDeSemana = 0.15m;
    public const decimal RecargoTemporadaAlta = 0.25m;
    public const decimal RecargoPorPersonaExtraNoche = 18000m;
    public const int PersonasIncluidas = 2;
    public const decimal DescuentoClienteFrecuente = 0.05m;
    public const decimal RecargoRoomService = 0.10m;

    public static CotizacionDto Calcular(RangoFechas rango, decimal tarifaBase, int huespedes, bool clienteFrecuente)
    {
        decimal hospedaje = 0;
        foreach (var dia in rango.Dias())
            hospedaje += tarifaBase * FactorTemporada(dia);

        int extras = Math.Max(0, huespedes - PersonasIncluidas);
        decimal recargo = extras * RecargoPorPersonaExtraNoche * rango.Noches;
        decimal descuento = clienteFrecuente ? Math.Round((hospedaje + recargo) * DescuentoClienteFrecuente, 2) : 0;

        decimal subtotal = Math.Round(hospedaje + recargo - descuento, 2);
        decimal iva = Math.Round(subtotal * Iva, 2);
        decimal total = subtotal + iva;

        return new CotizacionDto
        {
            Noches = rango.Noches,
            TarifaBase = tarifaBase,
            Hospedaje = Math.Round(hospedaje, 2),
            RecargoPersonasExtra = recargo,
            Descuento = descuento,
            Subtotal = subtotal,
            Iva = iva,
            Total = total,
            AnticipoMinimo = Math.Round(total * PorcentajeAnticipo, 2)
        };
    }

    public static decimal FactorTemporada(DateTime dia)
    {
        decimal factor = 1m;
        if (dia.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
            factor += RecargoFinDeSemana;
        if (EsTemporadaAlta(dia))
            factor += RecargoTemporadaAlta;
        return factor;
    }

    public static decimal CalcularTotalPedido(decimal subtotal, bool esRoomService)
    {
        return esRoomService
            ? Math.Round(subtotal * (1m + RecargoRoomService), 2)
            : Math.Round(subtotal, 2);
    }

    public static bool EsTemporadaAlta(DateTime dia)
    {
        // Verano (julio-agosto), Navidad (15 dic - 6 ene) y Semana Santa
        if (dia.Month is 7 or 8) return true;
        if ((dia.Month == 12 && dia.Day >= 15) || (dia.Month == 1 && dia.Day <= 6)) return true;

        var pascua = DomingoDePascua(dia.Year);
        return dia.Date >= pascua.AddDays(-7) && dia.Date <= pascua;
    }

    private static DateTime DomingoDePascua(int anio)
    {
        int a = anio % 19, b = anio / 100, c = anio % 100;
        int d = b / 4, e = b % 4, f = (b + 8) / 25, g = (b - f + 1) / 3;
        int h = (19 * a + b - d - g + 15) % 30;
        int i = c / 4, k = c % 4;
        int l = (32 + 2 * e + 2 * i - h - k) % 7;
        int m = (a + 11 * h + 22 * l) / 451;
        int mes = (h + l - 7 * m + 114) / 31;
        int dia = ((h + l - 7 * m + 114) % 31) + 1;
        return new DateTime(anio, mes, dia);
    }
}
