using Hotel.Api.Servicios;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Excepciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Api.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    private SeguridadApiServicio? ObtenerSeguridad()
        => HttpContext?.RequestServices?.GetService<SeguridadApiServicio>();

    protected bool EsPeticionAdmin()
    {
        if (HttpContext == null)
            return true;

        var seguridad = ObtenerSeguridad();
        return seguridad != null && seguridad.EsAdmin(Request);
    }

    protected SesionHuesped? ObtenerSesionHuesped()
    {
        if (HttpContext == null)
            return null;

        return ObtenerSeguridad()?.ValidarTokenHuesped(Request);
    }

    protected IActionResult? RequerirAdmin()
    {
        if (!EsPeticionAdmin())
            return Unauthorized(new { mensaje = "Acceso restringido al personal administrativo." });

        return null;
    }

    protected IActionResult? RequerirHuespedOAdmin(out SesionHuesped? sesion)
    {
        sesion = null;
        if (EsPeticionAdmin())
            return null;

        sesion = ObtenerSesionHuesped();
        if (sesion == null)
            return Unauthorized(new { mensaje = "Debe iniciar sesión para realizar esta operación." });

        return null;
    }

    protected void EmitirHeaderSesion(Cliente cliente)
    {
        if (HttpContext == null)
            return;

        var seguridad = ObtenerSeguridad();
        if (seguridad == null)
            return;

        var (token, expiraEn) = seguridad.EmitirTokenHuesped(cliente);
        Response.Headers["X-Session-Token"] = token;
        Response.Headers["X-Session-Expires"] = expiraEn.ToString("O");
    }

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
