using System.Data;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Interfaces;

public interface IRepositorio<T> where T : class
{
    Task<T?> ObtenerPorIdAsync(int id);
    Task<List<T>> ListarAsync();
    Task AgregarAsync(T entidad);
    void Actualizar(T entidad);
    void Eliminar(T entidad);
}

public interface IClienteRepositorio : IRepositorio<Cliente>
{
    Task<List<Cliente>> BuscarAsync(string texto);
    Task<Cliente?> ObtenerPorDocumentoAsync(string numeroDocumento);
    Task<bool> ExisteDocumentoAsync(string numeroDocumento, int? excluirId = null);
    Task<Cliente?> ObtenerPorEmailAsync(string email);
    Task<bool> ExisteEmailAsync(string email, int? excluirId = null);
    Task<bool> TieneReservacionesAsync(int clienteId);
    Task<int> ContarEstadiasCompletadasAsync(int clienteId);
}

public interface ITipoHabitacionRepositorio : IRepositorio<TipoHabitacion>
{
    Task<bool> TieneHabitacionesAsync(int tipoId);
}

public interface IHabitacionRepositorio : IRepositorio<Habitacion>
{
    Task<Habitacion?> ObtenerConTipoAsync(int id);
    Task<List<Habitacion>> ListarConTipoAsync(int? tipoId = null, int? capacidadMinima = null, decimal? tarifaMaxima = null);
    Task<List<HabitacionDisponibleDto>> ListarDisponiblesAsync(DateTime entrada, DateTime salida, int huespedes, int? tipoId = null);
    Task<List<Habitacion>> ListarOrdenadasPorTarifaYCapacidadAsync();
    Task<bool> ExisteNumeroAsync(string numero, int? excluirId = null);
}

public interface IReservacionRepositorio : IRepositorio<Reservacion>
{
    Task<Reservacion?> ObtenerDetalleAsync(int id);
    Task<Reservacion?> ObtenerPorCodigoAsync(string codigo);
    Task<List<Reservacion>> BuscarAsync(FiltroReservacionDto filtro);
    Task<List<Reservacion>> ListarActivasPorDocumentoAsync(string numeroDocumento);
    Task<bool> ExisteTraslapeAsync(int habitacionId, DateTime entrada, DateTime salida, int? excluirId = null);
    Task<List<Reservacion>> ListarQueCruzanAsync(DateTime desde, DateTime hasta);
    Task<List<ResumenOcupacionMesDto>> ResumenPorMesAsync(int anio);
    Task<int> ContarDelAnioAsync(int anio);
}

public interface IPagoRepositorio : IRepositorio<Pago>
{
    Task<List<Pago>> ListarPorReservacionAsync(int reservacionId);
    Task<decimal> TotalPagadoAsync(int reservacionId);
    Task<List<IngresoPeriodoDto>> IngresosPorPeriodoAsync(DateTime desde, DateTime hasta);
}

public interface ICategoriaPlatilloRepositorio : IRepositorio<CategoriaPlatillo>
{
}

public interface IPlatilloRepositorio : IRepositorio<Platillo>
{
    Task<List<Platillo>> ListarConCategoriaAsync(int? categoriaId = null, bool soloDisponibles = false);
    Task<List<Platillo>> ObtenerVariosAsync(IEnumerable<int> ids);
    Task<List<PlatilloSolicitadoDto>> MasSolicitadosAsync(int top, DateTime? desde = null, DateTime? hasta = null);
}

public interface IPedidoRepositorio : IRepositorio<PedidoRestaurante>
{
    Task<PedidoRestaurante?> ObtenerConDetallesAsync(int id);
    Task<List<PedidoRestaurante>> BuscarAsync(FiltroPedidoDto filtro);
    Task<decimal> TotalConsumosAsync(int reservacionId);
    Task<List<TotalPedidoDto>> TotalesPorReservacionAsync(int reservacionId);
}

public interface IUnidadTrabajo : IDisposable
{
    IClienteRepositorio Clientes { get; }
    ITipoHabitacionRepositorio TiposHabitacion { get; }
    IHabitacionRepositorio Habitaciones { get; }
    IReservacionRepositorio Reservaciones { get; }
    IPagoRepositorio Pagos { get; }
    ICategoriaPlatilloRepositorio Categorias { get; }
    IPlatilloRepositorio Platillos { get; }
    IPedidoRepositorio Pedidos { get; }

    Task<int> GuardarAsync();
    Task IniciarTransaccionAsync(IsolationLevel nivel = IsolationLevel.ReadCommitted);
    Task ConfirmarAsync();
    Task RevertirAsync();
}
