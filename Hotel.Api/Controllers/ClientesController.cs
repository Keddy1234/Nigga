using Hotel.Api.Servicios;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

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
    {
        if (RequerirAdmin() is { } authError) return authError;
        return await EjecutarAsync(async () => Ok(string.IsNullOrWhiteSpace(buscar)
            ? await _servicio.ListarAsync()
            : await _servicio.BuscarAsync(buscar)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
    {
        if (RequerirAdmin() is { } authError) return authError;
        return await EjecutarAsync(async () => Ok(await _servicio.ObtenerAsync(id)));
    }

    [HttpPost("login")]
    [EnableRateLimiting("auth")]
    public async Task<IActionResult> IniciarSesion([FromBody] LoginClienteDto? dto)
        => await EjecutarAsync(async () =>
        {
            var cliente = await _servicio.IniciarSesionPorEmailAsync(dto?.Email ?? string.Empty);
            EmitirHeaderSesion(cliente);
            return Ok(cliente);
        });

    [HttpPost("logout")]
    public IActionResult CerrarSesion()
    {
        HttpContext?.RequestServices?.GetService<SeguridadApiServicio>()?.RevocarToken(Request);
        return NoContent();
    }

    [HttpPost]
    [EnableRateLimiting("auth")]
    public async Task<IActionResult> Crear(Cliente cliente)
        => await EjecutarAsync(async () =>
        {
            var creado = await _servicio.CrearAsync(cliente);
            EmitirHeaderSesion(creado);
            return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
        });

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Cliente cliente)
    {
        if (RequerirAdmin() is { } authError) return authError;
        return await EjecutarAsync(async () =>
        {
            await _servicio.ActualizarAsync(id, cliente);
            return NoContent();
        });
    }
}
