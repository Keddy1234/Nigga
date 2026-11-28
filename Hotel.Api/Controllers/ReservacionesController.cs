using Hotel.Dominio.DTOs;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Hotel.Dominio.Reglas;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservacionesController : ApiControllerBase
{
    private readonly IReservacionServicio _servicio;

    public ReservacionesController(IReservacionServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] FiltroReservacionDto filtro)
        => await EjecutarAsync(async () => Ok(await _servicio.BuscarAsync(filtro)));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
        => await EjecutarAsync(async () => Ok(await _servicio.ObtenerAsync(id)));

    [HttpGet("codigo/{codigo}")]
    public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        => await EjecutarAsync(async () => Ok(await _servicio.ObtenerPorCodigoAsync(codigo)));

    [HttpGet("documento/{numeroDocumento}")]
    public async Task<IActionResult> PorDocumento(string numeroDocumento)
        => await EjecutarAsync(async () => Ok(await _servicio.ActivasPorDocumentoAsync(numeroDocumento)));

    [HttpPost("cotizar")]
    public async Task<IActionResult> Cotizar(CotizarReservacionDto dto)
        => await EjecutarAsync(async () => Ok(await _servicio.CotizarAsync(dto)));

    [HttpPost]
    public async Task<IActionResult> Crear(CrearReservacionDto dto)
        => await EjecutarAsync(async () =>
        {
            var creada = await _servicio.CrearAsync(dto);
            return CreatedAtAction(nameof(Obtener), new { id = creada.Id }, creada);
        });

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Modificar(int id, CrearReservacionDto dto)
        => await EjecutarAsync(async () =>
        {
            await _servicio.ModificarAsync(id, dto);
            return NoContent();
        });

    [HttpPost("{id:int}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
        => await EjecutarAsync(async () =>
        {
            await _servicio.CancelarAsync(id);
            return NoContent();
        });

    [HttpPost("{id:int}/checkin")]
    public async Task<IActionResult> CheckIn(int id, CheckInDto dto)
        => await EjecutarAsync(async () =>
        {
            await _servicio.CheckInAsync(id, dto);
            return NoContent();
        });

    [HttpPost("{id:int}/checkout")]
    public async Task<IActionResult> CheckOut(int id)
        => await EjecutarAsync(async () => Ok(await _servicio.CheckOutAsync(id)));

    [HttpGet("{id:int}/cuenta")]
    public async Task<IActionResult> EstadoCuenta(int id)
        => await EjecutarAsync(async () => Ok(await _servicio.EstadoCuentaAsync(id)));

    [HttpGet("tipo-cambio")]
    public IActionResult ObtenerTipoCambio()
        => Ok(new TipoCambioDto
        {
            MonedaBase = "CRC",
            MonedaExtranjera = "USD",
            TipoCambioUsd = ConversorMoneda.TipoCambioUsdDefecto,
            FechaConsulta = DateTime.Now
        });

    [HttpGet("{id:int}/factura")]
    public async Task<IActionResult> Factura(int id, [FromQuery] Moneda? moneda)
        => await EjecutarAsync(async () => Ok(await _servicio.GenerarFacturaAsync(id, moneda)));
}

[ApiController]
[Route("api/[controller]")]
public class PagosController : ApiControllerBase
{
    private readonly IPagoServicio _servicio;

    public PagosController(IPagoServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet("reservacion/{reservacionId:int}")]
    public async Task<IActionResult> ListarPorReservacion(int reservacionId)
        => await EjecutarAsync(async () => Ok(await _servicio.ListarPorReservacionAsync(reservacionId)));

    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarPagoDto dto)
        => await EjecutarAsync(async () => Ok(await _servicio.RegistrarAsync(dto)));
}
