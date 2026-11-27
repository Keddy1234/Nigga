using Hotel.AccesoDatos.Contexto;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccesoDatos.Repositorios;

public class ReservacionRepositorio : Repositorio<Reservacion>, IReservacionRepositorio
{
    private static readonly EstadoReservacion[] Activas =
        { EstadoReservacion.PendientePago, EstadoReservacion.Confirmada, EstadoReservacion.EnCurso };

    public ReservacionRepositorio(HotelDbContext db) : base(db) { }

    public Task<Reservacion?> ObtenerDetalleAsync(int id)
        => _db.Reservaciones
            .Include(r => r.Cliente)
            .Include(r => r.Habitacion).ThenInclude(h => h!.TipoHabitacion)
            .FirstOrDefaultAsync(r => r.Id == id);

    public Task<Reservacion?> ObtenerPorCodigoAsync(string codigo)
        => _db.Reservaciones
            .Include(r => r.Cliente)
            .Include(r => r.Habitacion).ThenInclude(h => h!.TipoHabitacion)
            .FirstOrDefaultAsync(r => r.Codigo == codigo);

    public async Task<List<Reservacion>> BuscarAsync(FiltroReservacionDto f)
    {
        IQueryable<Reservacion> q = _db.Reservaciones.AsNoTracking()
            .Include(r => r.Cliente)
            .Include(r => r.Habitacion);

        if (f.ClienteId.HasValue) q = q.Where(r => r.ClienteId == f.ClienteId);
        if (f.HabitacionId.HasValue) q = q.Where(r => r.HabitacionId == f.HabitacionId);
        if (f.Desde.HasValue) q = q.Where(r => r.FechaFin > f.Desde);
        if (f.Hasta.HasValue) q = q.Where(r => r.FechaInicio < f.Hasta);
        if (f.Estado.HasValue) q = q.Where(r => r.Estado == f.Estado);

        return await q.OrderByDescending(r => r.FechaInicio).ToListAsync();
    }

    public Task<List<Reservacion>> ListarActivasPorDocumentoAsync(string numeroDocumento)
        => _db.Reservaciones.AsNoTracking()
            .Include(r => r.Cliente)
            .Include(r => r.Habitacion)
            .Where(r => r.Cliente!.NumeroDocumento == numeroDocumento && Activas.Contains(r.Estado))
            .OrderBy(r => r.FechaInicio)
            .ToListAsync();

    // nuevaEntrada < salidaExistente && nuevaSalida > entradaExistente
    public Task<bool> ExisteTraslapeAsync(int habitacionId, DateTime entrada, DateTime salida, int? excluirId = null)
        => _db.Reservaciones.AnyAsync(r => r.HabitacionId == habitacionId
                                       && r.Id != excluirId
                                       && Activas.Contains(r.Estado)
                                       && entrada < r.FechaFin
                                       && salida > r.FechaInicio);

    public Task<List<Reservacion>> ListarQueCruzanAsync(DateTime desde, DateTime hasta)
        => _db.Reservaciones.AsNoTracking()
            .Where(r => Activas.Contains(r.Estado) || r.Estado == EstadoReservacion.Completada)
            .Where(r => r.FechaInicio < hasta && r.FechaFin > desde)
            .ToListAsync();

    public async Task<List<ResumenOcupacionMesDto>> ResumenPorMesAsync(int anio)
    {
        var datos = await _db.Reservaciones.AsNoTracking()
            .Where(r => r.FechaInicio.Year == anio && r.Estado != EstadoReservacion.Cancelada)
            .GroupBy(r => r.FechaInicio.Month)
            .Select(g => new
            {
                Mes = g.Key,
                Cantidad = g.Count(),
                Noches = g.Sum(r => EF.Functions.DateDiffDay(r.FechaInicio, r.FechaFin)),
                Monto = g.Sum(r => r.CostoTotal)
            })
            .OrderBy(x => x.Mes)
            .ToListAsync();

        return datos.Select(x => new ResumenOcupacionMesDto
        {
            Anio = anio,
            Mes = x.Mes,
            Reservaciones = x.Cantidad,
            Noches = x.Noches,
            Monto = x.Monto
        }).ToList();
    }

    public Task<int> ContarDelAnioAsync(int anio)
        => _db.Reservaciones.CountAsync(r => r.Codigo.StartsWith("RES-" + anio + "-"));
}

public class PagoRepositorio : Repositorio<Pago>, IPagoRepositorio
{
    public PagoRepositorio(HotelDbContext db) : base(db) { }

    public Task<List<Pago>> ListarPorReservacionAsync(int reservacionId)
        => _db.Pagos.AsNoTracking()
            .Where(p => p.ReservacionId == reservacionId)
            .OrderBy(p => p.FechaPago)
            .ToListAsync();

    public async Task<decimal> TotalPagadoAsync(int reservacionId)
        => await _db.Pagos.Where(p => p.ReservacionId == reservacionId).SumAsync(p => (decimal?)p.Monto) ?? 0m;

    public async Task<List<IngresoPeriodoDto>> IngresosPorPeriodoAsync(DateTime desde, DateTime hasta)
    {
        var datos = await _db.Pagos.AsNoTracking()
            .Where(p => p.FechaPago >= desde && p.FechaPago < hasta)
            .GroupBy(p => new { p.FechaPago.Year, p.FechaPago.Month, p.MetodoPago })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                g.Key.MetodoPago,
                Cantidad = g.Count(),
                Total = g.Sum(p => p.Monto)
            })
            .OrderBy(x => x.Year).ThenBy(x => x.Month)
            .ToListAsync();

        return datos.Select(x => new IngresoPeriodoDto
        {
            Anio = x.Year,
            Mes = x.Month,
            MetodoPago = x.MetodoPago.ToString(),
            CantidadPagos = x.Cantidad,
            Total = x.Total
        }).ToList();
    }
}
