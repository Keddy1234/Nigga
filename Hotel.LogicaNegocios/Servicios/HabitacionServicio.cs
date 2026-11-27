using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;
using Hotel.Dominio.ValueObjects;

namespace Hotel.LogicaNegocios.Servicios;

public class TipoHabitacionServicio : ITipoHabitacionServicio
{
    private readonly IUnidadTrabajo _uow;

    public TipoHabitacionServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<TipoHabitacion>> ListarAsync() => _uow.TiposHabitacion.ListarAsync();

    public async Task<TipoHabitacion> ObtenerAsync(int id)
        => await _uow.TiposHabitacion.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Tipo de habitación no encontrado.");

    public async Task<TipoHabitacion> CrearAsync(TipoHabitacion tipo)
    {
        Validar(tipo);
        tipo.Id = 0;
        await _uow.TiposHabitacion.AgregarAsync(tipo);
        await _uow.GuardarAsync();
        return tipo;
    }

    public async Task ActualizarAsync(int id, TipoHabitacion datos)
    {
        Validar(datos);
        var tipo = await ObtenerAsync(id);
        tipo.Nombre = datos.Nombre.Trim();
        tipo.Descripcion = datos.Descripcion;
        tipo.CapacidadAdultos = datos.CapacidadAdultos;
        tipo.CapacidadNinos = datos.CapacidadNinos;
        tipo.PrecioBasePorNoche = datos.PrecioBasePorNoche;
        await _uow.GuardarAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var tipo = await ObtenerAsync(id);
        if (await _uow.TiposHabitacion.TieneHabitacionesAsync(id))
            throw new ConflictoException("No se puede eliminar: hay habitaciones de este tipo.");
        _uow.TiposHabitacion.Eliminar(tipo);
        await _uow.GuardarAsync();
    }

    private static void Validar(TipoHabitacion t)
    {
        if (string.IsNullOrWhiteSpace(t.Nombre))
            throw new ReglaNegocioException("El nombre del tipo es obligatorio.");
        if (t.CapacidadAdultos < 1)
            throw new ReglaNegocioException("La capacidad de adultos debe ser al menos 1.");
        if (t.CapacidadNinos < 0)
            throw new ReglaNegocioException("La capacidad de niños no puede ser negativa.");
        if (t.PrecioBasePorNoche <= 0)
            throw new ReglaNegocioException("El precio base por noche debe ser mayor que cero.");
    }
}

public class HabitacionServicio : IHabitacionServicio
{
    private readonly IUnidadTrabajo _uow;

    public HabitacionServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<Habitacion>> ListarAsync(int? tipoId, int? capacidadMinima, decimal? tarifaMaxima)
        => _uow.Habitaciones.ListarConTipoAsync(tipoId, capacidadMinima, tarifaMaxima);

    public async Task<Habitacion> ObtenerAsync(int id)
        => await _uow.Habitaciones.ObtenerConTipoAsync(id) ?? throw new NoEncontradoException("Habitación no encontrada.");

    public Task<List<HabitacionDisponibleDto>> DisponiblesAsync(DateTime entrada, DateTime salida, int huespedes, int? tipoId)
    {
        var rango = new RangoFechas(entrada, salida);
        if (huespedes < 1)
            throw new ReglaNegocioException("La cantidad de huéspedes debe ser al menos 1.");
        return _uow.Habitaciones.ListarDisponiblesAsync(rango.Entrada, rango.Salida, huespedes, tipoId);
    }

    public Task<List<Habitacion>> OrdenadasPorTarifaYCapacidadAsync()
        => _uow.Habitaciones.ListarOrdenadasPorTarifaYCapacidadAsync();

    public async Task<Habitacion> CrearAsync(Habitacion habitacion)
    {
        await ValidarAsync(habitacion, null);
        habitacion.Id = 0;
        habitacion.TipoHabitacion = null;
        await _uow.Habitaciones.AgregarAsync(habitacion);
        await _uow.GuardarAsync();
        return habitacion;
    }

    public async Task ActualizarAsync(int id, Habitacion datos)
    {
        await ValidarAsync(datos, id);
        var hab = await _uow.Habitaciones.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Habitación no encontrada.");
        hab.Numero = datos.Numero.Trim();
        hab.Piso = datos.Piso;
        hab.TipoHabitacionId = datos.TipoHabitacionId;
        hab.EstadoOperativo = datos.EstadoOperativo;
        hab.TieneJacuzzi = datos.TieneJacuzzi;
        hab.TieneVistaMar = datos.TieneVistaMar;
        await _uow.GuardarAsync();
    }

    public async Task CambiarEstadoAsync(int id, EstadoHabitacion estado)
    {
        var hab = await _uow.Habitaciones.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Habitación no encontrada.");
        if (hab.EstadoOperativo == EstadoHabitacion.Ocupada && estado != EstadoHabitacion.EnLimpieza)
            throw new ReglaNegocioException("Una habitación ocupada solo puede pasar a limpieza mediante el check-out.");
        hab.EstadoOperativo = estado;
        await _uow.GuardarAsync();
    }

    public Task DesactivarAsync(int id) => CambiarEstadoAsync(id, EstadoHabitacion.Bloqueada);

    private async Task ValidarAsync(Habitacion h, int? id)
    {
        if (string.IsNullOrWhiteSpace(h.Numero) || h.Numero.Length > 10)
            throw new ReglaNegocioException("El número de habitación es obligatorio (máximo 10 caracteres).");
        if (h.Piso < 0)
            throw new ReglaNegocioException("El piso no es válido.");
        if (await _uow.TiposHabitacion.ObtenerPorIdAsync(h.TipoHabitacionId) == null)
            throw new ReglaNegocioException("El tipo de habitación no existe.");
        if (await _uow.Habitaciones.ExisteNumeroAsync(h.Numero.Trim(), id))
            throw new ConflictoException("Ya existe una habitación con ese número.");
    }
}
