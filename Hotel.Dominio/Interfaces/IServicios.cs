using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Interfaces;

public interface IClienteServicio
{
    Task<List<Cliente>> ListarAsync();
    Task<Cliente> ObtenerAsync(int id);
    Task<List<Cliente>> BuscarAsync(string texto);
    Task<Cliente> CrearAsync(Cliente cliente);
    Task ActualizarAsync(int id, Cliente cliente);
}

public interface ITipoHabitacionServicio
{
    Task<List<TipoHabitacion>> ListarAsync();
    Task<TipoHabitacion> ObtenerAsync(int id);
    Task<TipoHabitacion> CrearAsync(TipoHabitacion tipo);
    Task ActualizarAsync(int id, TipoHabitacion tipo);
    Task EliminarAsync(int id);
}

public interface IHabitacionServicio
{
    Task<List<Habitacion>> ListarAsync(int? tipoId, int? capacidadMinima, decimal? tarifaMaxima);
    Task<Habitacion> ObtenerAsync(int id);
    Task<List<HabitacionDisponibleDto>> DisponiblesAsync(DateTime entrada, DateTime salida, int huespedes, int? tipoId);
    Task<List<Habitacion>> OrdenadasPorTarifaYCapacidadAsync();
    Task<Habitacion> CrearAsync(Habitacion habitacion);
    Task ActualizarAsync(int id, Habitacion habitacion);
    Task CambiarEstadoAsync(int id, EstadoHabitacion estado);
    Task DesactivarAsync(int id);
}

public interface IReservacionServicio
{
    Task<List<Reservacion>> BuscarAsync(FiltroReservacionDto filtro);
    Task<Reservacion> ObtenerAsync(int id);
    Task<Reservacion> ObtenerPorCodigoAsync(string codigo);
    Task<List<Reservacion>> ActivasPorDocumentoAsync(string numeroDocumento);
    Task<CotizacionDto> CotizarAsync(CotizarReservacionDto dto);
    Task<Reservacion> CrearAsync(CrearReservacionDto dto);
    Task ModificarAsync(int id, CrearReservacionDto dto);
    Task CancelarAsync(int id);
    Task CheckInAsync(int id, CheckInDto dto);
    Task<EstadoCuentaDto> CheckOutAsync(int id);
    Task<EstadoCuentaDto> EstadoCuentaAsync(int id);
}

public interface IPagoServicio
{
    Task<List<Pago>> ListarPorReservacionAsync(int reservacionId);
    Task<Pago> RegistrarAsync(RegistrarPagoDto dto);
}

public interface IPlatilloServicio
{
    Task<List<CategoriaPlatillo>> ListarCategoriasAsync();
    Task<CategoriaPlatillo> CrearCategoriaAsync(CategoriaPlatillo categoria);
    Task ActualizarCategoriaAsync(int id, CategoriaPlatillo categoria);
    Task<List<Platillo>> ListarAsync(int? categoriaId, bool soloDisponibles);
    Task<Platillo> ObtenerAsync(int id);
    Task<Platillo> CrearAsync(Platillo platillo);
    Task ActualizarAsync(int id, Platillo platillo);
    Task DesactivarAsync(int id);
}

public interface IPedidoServicio
{
    Task<List<PedidoRestaurante>> BuscarAsync(FiltroPedidoDto filtro);
    Task<PedidoRestaurante> ObtenerAsync(int id);
    Task<List<TotalPedidoDto>> TotalesPorReservacionAsync(int reservacionId);
    Task<PedidoRestaurante> CrearAsync(CrearPedidoDto dto);
    Task ModificarAsync(int id, CrearPedidoDto dto);
    Task AvanzarEstadoAsync(int id, EstadoPedido nuevoEstado);
    Task CancelarAsync(int id);
}

public interface IReporteServicio
{
    Task<List<OcupacionDiaDto>> OcupacionPorPeriodoAsync(DateTime desde, DateTime hasta);
    Task<List<ResumenOcupacionMesDto>> ResumenOcupacionPorMesAsync(int anio);
    Task<List<Reservacion>> ReservacionesAsync(DateTime desde, DateTime hasta, EstadoReservacion? estado);
    Task<List<IngresoPeriodoDto>> IngresosAsync(DateTime desde, DateTime hasta);
    Task<List<PedidoRestaurante>> PedidosAsync(DateTime? fecha, EstadoPedido? estado);
    Task<List<PlatilloSolicitadoDto>> PlatillosMasSolicitadosAsync(int top, DateTime? desde, DateTime? hasta);
}
