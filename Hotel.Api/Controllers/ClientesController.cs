using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ApiControllerBase
{
    private readonly IClienteServicio _servicio;

    public ClientesController(IClienteServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? buscar)
        => await EjecutarAsync(async () => Ok(string.IsNullOrWhiteSpace(buscar)
            ? await _servicio.ListarAsync()
            : await _servicio.BuscarAsync(buscar)));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
        => await EjecutarAsync(async () => Ok(await _servicio.ObtenerAsync(id)));

    [HttpPost("login")]
    public async Task<IActionResult> IniciarSesion([FromBody] LoginClienteDto? dto)
        => await EjecutarAsync(async () => Ok(await _servicio.IniciarSesionPorEmailAsync(dto?.Email ?? string.Empty)));

    [HttpPost]
    public async Task<IActionResult> Crear(Cliente cliente)
        => await EjecutarAsync(async () =>
        {
            var creado = await _servicio.CrearAsync(cliente);
            return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
        });

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Cliente cliente)
        => await EjecutarAsync(async () =>
        {
            await _servicio.ActualizarAsync(id, cliente);
            return NoContent();
        });
}
