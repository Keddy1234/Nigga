using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposHabitacionController : ControllerBase
{
    private readonly ITipoHabitacionServicio _servicio;

    public TiposHabitacionController(ITipoHabitacionServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _servicio.ListarAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id) => Ok(await _servicio.ObtenerAsync(id));

    [HttpPost]
    public async Task<IActionResult> Crear(TipoHabitacion tipo)
    {
        var creado = await _servicio.CrearAsync(tipo);
        return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, TipoHabitacion tipo)
    {
        await _servicio.ActualizarAsync(id, tipo);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _servicio.EliminarAsync(id);
        return NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class HabitacionesController : ControllerBase
{
    private readonly IHabitacionServicio _servicio;

    public HabitacionesController(IHabitacionServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int? tipoId, [FromQuery] int? capacidad, [FromQuery] decimal? tarifaMaxima)
        => Ok(await _servicio.ListarAsync(tipoId, capacidad, tarifaMaxima));

    [HttpGet("ordenadas")]
    public async Task<IActionResult> Ordenadas()
        => Ok(await _servicio.OrdenadasPorTarifaYCapacidadAsync());

    [HttpGet("disponibles")]
    public async Task<IActionResult> Disponibles([FromQuery] DateTime? entrada, [FromQuery] DateTime? salida, [FromQuery] int huespedes = 1, [FromQuery] int? tipoId = null)
    {
        var ini = entrada ?? DateTime.Today;
        var fin = (salida.HasValue && salida.Value.Date > ini.Date) ? salida.Value : ini.AddDays(1);
        return Ok(await _servicio.DisponiblesAsync(ini, fin, huespedes, tipoId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id) => Ok(await _servicio.ObtenerAsync(id));

    [HttpPost]
    public async Task<IActionResult> Crear(Habitacion habitacion)
    {
        var creada = await _servicio.CrearAsync(habitacion);
        return CreatedAtAction(nameof(Obtener), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Habitacion habitacion)
    {
        await _servicio.ActualizarAsync(id, habitacion);
        return NoContent();
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromQuery] EstadoHabitacion estado)
    {
        await _servicio.CambiarEstadoAsync(id, estado);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        await _servicio.DesactivarAsync(id);
        return NoContent();
    }
}
