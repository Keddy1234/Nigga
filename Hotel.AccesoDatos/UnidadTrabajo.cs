using System.Data;
using Hotel.AccesoDatos.Contexto;
using Hotel.AccesoDatos.Repositorios;
using Hotel.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hotel.AccesoDatos;

public class UnidadTrabajo : IUnidadTrabajo
{
    private readonly HotelDbContext _db;
    private IDbContextTransaction? _transaccion;

    public UnidadTrabajo(HotelDbContext db)
    {
        _db = db;
        Clientes = new ClienteRepositorio(db);
        TiposHabitacion = new TipoHabitacionRepositorio(db);
        Habitaciones = new HabitacionRepositorio(db);
        Reservaciones = new ReservacionRepositorio(db);
        Pagos = new PagoRepositorio(db);
        Categorias = new CategoriaPlatilloRepositorio(db);
        Platillos = new PlatilloRepositorio(db);
        Pedidos = new PedidoRepositorio(db);
    }

    public IClienteRepositorio Clientes { get; }
    public ITipoHabitacionRepositorio TiposHabitacion { get; }
    public IHabitacionRepositorio Habitaciones { get; }
    public IReservacionRepositorio Reservaciones { get; }
    public IPagoRepositorio Pagos { get; }
    public ICategoriaPlatilloRepositorio Categorias { get; }
    public IPlatilloRepositorio Platillos { get; }
    public IPedidoRepositorio Pedidos { get; }

    public Task<int> GuardarAsync() => _db.SaveChangesAsync();

    public async Task IniciarTransaccionAsync(IsolationLevel nivel = IsolationLevel.ReadCommitted)
    {
        _transaccion = await _db.Database.BeginTransactionAsync(nivel);
    }

    public async Task ConfirmarAsync()
    {
        if (_transaccion == null) return;
        await _transaccion.CommitAsync();
        await _transaccion.DisposeAsync();
        _transaccion = null;
    }

    public async Task RevertirAsync()
    {
        if (_transaccion == null) return;
        await _transaccion.RollbackAsync();
        await _transaccion.DisposeAsync();
        _transaccion = null;
        _db.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        _transaccion?.Dispose();
    }
}
