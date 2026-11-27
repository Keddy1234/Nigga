using Hotel.AccesoDatos.Contexto;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccesoDatos.Repositorios;

public class HabitacionRepositorio : Repositorio<Habitacion>, IHabitacionRepositorio
{
    public HabitacionRepositorio(HotelDbContext db) : base(db) { }

    public Task<Habitacion?> ObtenerConTipoAsync(int id)
        => _db.Habitaciones.Include(h => h.TipoHabitacion).FirstOrDefaultAsync(h => h.Id == id);

    public async Task<List<Habitacion>> ListarConTipoAsync(int? tipoId = null, int? capacidadMinima = null, decimal? tarifaMaxima = null)
    {
        IQueryable<Habitacion> q = _db.Habitaciones.AsNoTracking().Include(h => h.TipoHabitacion);

        if (tipoId.HasValue)
            q = q.Where(h => h.TipoHabitacionId == tipoId);
        if (capacidadMinima.HasValue)
            q = q.Where(h => h.TipoHabitacion!.CapacidadAdultos + h.TipoHabitacion.CapacidadNinos >= capacidadMinima);
        if (tarifaMaxima.HasValue)
            q = q.Where(h => h.TipoHabitacion!.PrecioBasePorNoche <= tarifaMaxima);

        return await q.OrderBy(h => h.Piso).ThenBy(h => h.Numero).ToListAsync();
    }

    // Disponibles en [entrada, salida): sin reservaciones activas que se crucen y sin estar fuera de servicio
    public async Task<List<HabitacionDisponibleDto>> ListarDisponiblesAsync(DateTime entrada, DateTime salida, int huespedes, int? tipoId = null)
    {
        var activas = new[] { EstadoReservacion.PendientePago, EstadoReservacion.Confirmada, EstadoReservacion.EnCurso };

        var q = _db.Habitaciones.AsNoTracking()
            .Where(h => h.EstadoOperativo != EstadoHabitacion.EnMantenimiento
                     && h.EstadoOperativo != EstadoHabitacion.Bloqueada)
            .Where(h => h.TipoHabitacion!.CapacidadAdultos + h.TipoHabitacion.CapacidadNinos >= huespedes)
            .Where(h => !h.Reservaciones.Any(r => activas.Contains(r.Estado)
                                               && entrada < r.FechaFin
                                               && salida > r.FechaInicio));

        if (tipoId.HasValue)
            q = q.Where(h => h.TipoHabitacionId == tipoId);

        return await q
            .OrderBy(h => h.TipoHabitacion!.PrecioBasePorNoche)
            .Select(h => new HabitacionDisponibleDto
            {
                Id = h.Id,
                Numero = h.Numero,
                Piso = h.Piso,
                Tipo = h.TipoHabitacion!.Nombre,
                Capacidad = h.TipoHabitacion.CapacidadAdultos + h.TipoHabitacion.CapacidadNinos,
                PrecioBasePorNoche = h.TipoHabitacion.PrecioBasePorNoche,
                TieneJacuzzi = h.TieneJacuzzi,
                TieneVistaMar = h.TieneVistaMar
            })
            .ToListAsync();
    }

    public Task<List<Habitacion>> ListarOrdenadasPorTarifaYCapacidadAsync()
        => _db.Habitaciones.AsNoTracking().Include(h => h.TipoHabitacion)
            .OrderBy(h => h.TipoHabitacion!.PrecioBasePorNoche)
            .ThenByDescending(h => h.TipoHabitacion!.CapacidadAdultos + h.TipoHabitacion.CapacidadNinos)
            .ToListAsync();

    public Task<bool> ExisteNumeroAsync(string numero, int? excluirId = null)
        => _db.Habitaciones.AnyAsync(h => h.Numero == numero && h.Id != excluirId);
}
