using Hotel.Dominio.DTOs;
using Hotel.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservacionesController : ControllerBase
{
    private readonly IReservacionServicio _servicio;

    public ReservacionesController(IReservacionServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] FiltroReservacionDto filtro)
        => Ok(await _servicio.BuscarAsync(filtro));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id) => Ok(await _servicio.ObtenerAsync(id));

    [HttpGet("codigo/{codigo}")]
    public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        => Ok(await _servicio.ObtenerPorCodigoAsync(codigo));

    [HttpGet("documento/{numeroDocumento}")]
    public async Task<IActionResult> PorDocumento(string numeroDocumento)
        => Ok(await _servicio.ActivasPorDocumentoAsync(numeroDocumento));

    [HttpPost("cotizar")]
    public async Task<IActionResult> Cotizar(CotizarReservacionDto dto)
        => Ok(await _servicio.CotizarAsync(dto));

    [HttpPost]
    public async Task<IActionResult> Crear(CrearReservacionDto dto)
    {
        var creada = await _servicio.CrearAsync(dto);
        return CreatedAtAction(nameof(Obtener), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Modificar(int id, CrearReservacionDto dto)
    {
        await _servicio.ModificarAsync(id, dto);
        return NoContent();
    }

    [HttpPost("{id:int}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        await _servicio.CancelarAsync(id);
        return NoContent();
    }

    [HttpPost("{id:int}/checkin")]
    public async Task<IActionResult> CheckIn(int id, CheckInDto dto)
    {
        await _servicio.CheckInAsync(id, dto);
        return NoContent();
    }

    [HttpPost("{id:int}/checkout")]
    public async Task<IActionResult> CheckOut(int id)
        => Ok(await _servicio.CheckOutAsync(id));

    [HttpGet("{id:int}/cuenta")]
    public async Task<IActionResult> EstadoCuenta(int id)
        => Ok(await _servicio.EstadoCuentaAsync(id));
}

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly IPagoServicio _servicio;

    public PagosController(IPagoServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet("reservacion/{reservacionId:int}")]
    public async Task<IActionResult> ListarPorReservacion(int reservacionId)
        => Ok(await _servicio.ListarPorReservacionAsync(reservacionId));

    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarPagoDto dto)
        => Ok(await _servicio.RegistrarAsync(dto));
}
