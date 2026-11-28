using Hotel.Dominio.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected async Task<IActionResult> EjecutarAsync(Func<Task<IActionResult>> accion)
    {
        try
        {
            return await accion();
        }
        catch (NoEncontradoException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ConflictoException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (ReglaNegocioException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
