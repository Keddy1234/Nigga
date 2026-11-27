using Hotel.AccesoDatos.Contexto;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccesoDatos.Repositorios;

public class PlatilloRepositorio : Repositorio<Platillo>, IPlatilloRepositorio
{
    public PlatilloRepositorio(HotelDbContext db) : base(db) { }

    public async Task<List<Platillo>> ListarConCategoriaAsync(int? categoriaId = null, bool soloDisponibles = false)
    {
        IQueryable<Platillo> q = _db.Platillos.AsNoTracking().Include(p => p.Categoria);
        if (categoriaId.HasValue) q = q.Where(p => p.CategoriaId == categoriaId);
        if (soloDisponibles) q = q.Where(p => p.Disponible && p.Categoria!.Activo);
        return await q.OrderBy(p => p.Categoria!.Nombre).ThenBy(p => p.Nombre).ToListAsync();
    }

    public Task<List<Platillo>> ObtenerVariosAsync(IEnumerable<int> ids)
        => _db.Platillos.Include(p => p.Categoria).Where(p => ids.Contains(p.Id)).ToListAsync();

    public async Task<List<PlatilloSolicitadoDto>> MasSolicitadosAsync(int top, DateTime? desde = null, DateTime? hasta = null)
    {
        var q = _db.DetallePedidos.AsNoTracking()
            .Where(d => d.Pedido!.EstadoPedido != EstadoPedido.Cancelado);

        if (desde.HasValue) q = q.Where(d => d.Pedido!.FechaPedido >= desde);
        if (hasta.HasValue) q = q.Where(d => d.Pedido!.FechaPedido < hasta);

        return await q
            .GroupBy(d => new { d.PlatilloId, d.Platillo!.Nombre })
            .Select(g => new PlatilloSolicitadoDto
            {
                PlatilloId = g.Key.PlatilloId,
                Nombre = g.Key.Nombre,
                CantidadVendida = g.Sum(d => d.Cantidad),
                Total = g.Sum(d => d.Subtotal)
            })
            .OrderByDescending(x => x.CantidadVendida)
            .Take(top)
            .ToListAsync();
    }
}

public class PedidoRepositorio : Repositorio<PedidoRestaurante>, IPedidoRepositorio
{
    public PedidoRepositorio(HotelDbContext db) : base(db) { }

    public Task<PedidoRestaurante?> ObtenerConDetallesAsync(int id)
        => _db.PedidosRestaurante
            .Include(p => p.Detalles).ThenInclude(d => d.Platillo)
            .Include(p => p.Reservacion)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<PedidoRestaurante>> BuscarAsync(FiltroPedidoDto f)
    {
        IQueryable<PedidoRestaurante> q = _db.PedidosRestaurante.AsNoTracking()
            .Include(p => p.Reservacion).ThenInclude(r => r!.Cliente)
            .Include(p => p.Reservacion).ThenInclude(r => r!.Habitacion)
            .Include(p => p.Detalles).ThenInclude(d => d.Platillo);

        if (f.ClienteId.HasValue) q = q.Where(p => p.Reservacion!.ClienteId == f.ClienteId);
        if (f.HabitacionId.HasValue) q = q.Where(p => p.Reservacion!.HabitacionId == f.HabitacionId);
        if (f.Fecha.HasValue)
        {
            var dia = f.Fecha.Value.Date;
            q = q.Where(p => p.FechaPedido >= dia && p.FechaPedido < dia.AddDays(1));
        }
        if (f.Estado.HasValue) q = q.Where(p => p.EstadoPedido == f.Estado);

        return await q.OrderByDescending(p => p.FechaPedido).ToListAsync();
    }

    public async Task<decimal> TotalConsumosAsync(int reservacionId)
        => await _db.PedidosRestaurante
            .Where(p => p.ReservacionId == reservacionId && p.EstadoPedido != EstadoPedido.Cancelado)
            .SumAsync(p => (decimal?)p.Total) ?? 0m;

    public Task<List<TotalPedidoDto>> TotalesPorReservacionAsync(int reservacionId)
        => _db.PedidosRestaurante.AsNoTracking()
            .Where(p => p.ReservacionId == reservacionId)
            .Select(p => new TotalPedidoDto
            {
                PedidoId = p.Id,
                FechaPedido = p.FechaPedido,
                Estado = p.EstadoPedido.ToString(),
                Total = p.Detalles.Sum(d => d.Subtotal)
            })
            .ToListAsync();
}
