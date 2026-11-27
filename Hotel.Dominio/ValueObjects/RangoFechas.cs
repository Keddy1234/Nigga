using Hotel.Dominio.Excepciones;

namespace Hotel.Dominio.ValueObjects;

// Intervalo [Entrada, Salida): salir y entrar el mismo dia no es traslape.
public readonly record struct RangoFechas
{
    public DateTime Entrada { get; }
    public DateTime Salida { get; }

    public RangoFechas(DateTime entrada, DateTime salida)
    {
        if (entrada.Date >= salida.Date)
            throw new ReglaNegocioException("La fecha de entrada debe ser anterior a la fecha de salida.");

        Entrada = entrada.Date;
        Salida = salida.Date;
    }

    public int Noches => (Salida - Entrada).Days;

    public bool SeTraslapaCon(DateTime entradaExistente, DateTime salidaExistente)
        => Entrada < salidaExistente.Date && Salida > entradaExistente.Date;

    public IEnumerable<DateTime> Dias()
    {
        for (var d = Entrada; d < Salida; d = d.AddDays(1))
            yield return d;
    }
}
