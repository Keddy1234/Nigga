using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Hotel.Dominio.ValueObjects;

namespace Hotel.LogicaNegocios.Servicios;

public class ReporteServicio : IReporteServicio
{
    private readonly IUnidadTrabajo _uow;

    public ReporteServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public async Task<List<OcupacionDiaDto>> OcupacionPorPeriodoAsync(DateTime desde, DateTime hasta)
    {
        var rango = new RangoFechas(desde, hasta);
        var habitaciones = await _uow.Habitaciones.ListarAsync();
        var reservas = await _uow.Reservaciones.ListarQueCruzanAsync(rango.Entrada, rango.Salida);
        int fueraServicio = habitaciones.Count(h => h.FueraDeServicio);

        return rango.Dias().Select(dia =>
        {
            int ocupadas = reservas
                .Where(r => r.Estado != EstadoReservacion.PendientePago && r.FechaInicio.Date <= dia && r.FechaFin.Date > dia)
                .Select(r => r.HabitacionId).Distinct().Count();

            return new OcupacionDiaDto
            {
                Fecha = dia,
                Ocupadas = ocupadas,
                FueraDeServicio = fueraServicio,
                Disponibles = Math.Max(0, habitaciones.Count - ocupadas - fueraServicio)
            };
        }).ToList();
    }

    public Task<List<ResumenOcupacionMesDto>> ResumenOcupacionPorMesAsync(int anio)
        => _uow.Reservaciones.ResumenPorMesAsync(anio);

    public Task<List<Reservacion>> ReservacionesAsync(DateTime desde, DateTime hasta, EstadoReservacion? estado)
        => _uow.Reservaciones.BuscarAsync(new FiltroReservacionDto { Desde = desde, Hasta = hasta, Estado = estado });

    public Task<List<IngresoPeriodoDto>> IngresosAsync(DateTime desde, DateTime hasta)
        => _uow.Pagos.IngresosPorPeriodoAsync(desde.Date, hasta.Date.AddDays(1));

    public Task<List<PedidoRestaurante>> PedidosAsync(DateTime? fecha, EstadoPedido? estado)
        => _uow.Pedidos.BuscarAsync(new FiltroPedidoDto { Fecha = fecha, Estado = estado });

    public Task<List<PlatilloSolicitadoDto>> PlatillosMasSolicitadosAsync(int top, DateTime? desde, DateTime? hasta)
        => _uow.Platillos.MasSolicitadosAsync(top <= 0 ? 5 : top, desde, hasta?.Date.AddDays(1));
}
