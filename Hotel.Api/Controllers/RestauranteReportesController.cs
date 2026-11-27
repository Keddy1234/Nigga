using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatillosController : ControllerBase
{
    private readonly IPlatilloServicio _servicio;

    public PlatillosController(IPlatilloServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet("categorias")]
    public async Task<IActionResult> Categorias() => Ok(await _servicio.ListarCategoriasAsync());

    [HttpPost("categorias")]
    public async Task<IActionResult> CrearCategoria(CategoriaPlatillo categoria)
        => Ok(await _servicio.CrearCategoriaAsync(categoria));

    [HttpPut("categorias/{id:int}")]
    public async Task<IActionResult> ActualizarCategoria(int id, CategoriaPlatillo categoria)
    {
        await _servicio.ActualizarCategoriaAsync(id, categoria);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int? categoriaId, [FromQuery] bool soloDisponibles = false)
        => Ok(await _servicio.ListarAsync(categoriaId, soloDisponibles));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id) => Ok(await _servicio.ObtenerAsync(id));

    [HttpPost]
    public async Task<IActionResult> Crear(Platillo platillo)
    {
        var creado = await _servicio.CrearAsync(platillo);
        return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, Platillo platillo)
    {
        await _servicio.ActualizarAsync(id, platillo);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        await _servicio.DesactivarAsync(id);
        return NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoServicio _servicio;

    public PedidosController(IPedidoServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] FiltroPedidoDto filtro)
        => Ok(await _servicio.BuscarAsync(filtro));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id) => Ok(await _servicio.ObtenerAsync(id));

    [HttpGet("reservacion/{reservacionId:int}/totales")]
    public async Task<IActionResult> TotalesPorReservacion(int reservacionId)
        => Ok(await _servicio.TotalesPorReservacionAsync(reservacionId));

    [HttpPost]
    public async Task<IActionResult> Crear(CrearPedidoDto dto)
    {
        var creado = await _servicio.CrearAsync(dto);
        return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Modificar(int id, CrearPedidoDto dto)
    {
        await _servicio.ModificarAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromQuery] EstadoPedido estado)
    {
        await _servicio.AvanzarEstadoAsync(id, estado);
        return NoContent();
    }

    [HttpPost("{id:int}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        await _servicio.CancelarAsync(id);
        return NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteServicio _servicio;

    public ReportesController(IReporteServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet("ocupacion")]
    public async Task<IActionResult> Ocupacion([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        => Ok(await _servicio.OcupacionPorPeriodoAsync(desde, hasta));

    [HttpGet("ocupacion-mes")]
    public async Task<IActionResult> OcupacionPorMes([FromQuery] int anio)
        => Ok(await _servicio.ResumenOcupacionPorMesAsync(anio <= 0 ? DateTime.Today.Year : anio));

    [HttpGet("reservaciones")]
    public async Task<IActionResult> Reservaciones([FromQuery] DateTime desde, [FromQuery] DateTime hasta, [FromQuery] EstadoReservacion? estado)
        => Ok(await _servicio.ReservacionesAsync(desde, hasta, estado));

    [HttpGet("ingresos")]
    public async Task<IActionResult> Ingresos([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        => Ok(await _servicio.IngresosAsync(desde, hasta));

    [HttpGet("pedidos")]
    public async Task<IActionResult> Pedidos([FromQuery] DateTime? fecha, [FromQuery] EstadoPedido? estado)
        => Ok(await _servicio.PedidosAsync(fecha, estado));

    [HttpGet("platillos-mas-solicitados")]
    public async Task<IActionResult> PlatillosMasSolicitados([FromQuery] int top = 5, [FromQuery] DateTime? desde = null, [FromQuery] DateTime? hasta = null)
        => Ok(await _servicio.PlatillosMasSolicitadosAsync(top, desde, hasta));
}
