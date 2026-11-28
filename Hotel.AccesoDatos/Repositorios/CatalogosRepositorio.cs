using Hotel.AccesoDatos.Contexto;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccesoDatos.Repositorios;

public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
{
    public ClienteRepositorio(HotelDbContext db) : base(db) { }

    public async Task<List<Cliente>> BuscarAsync(string texto)
    {
        texto = texto.Trim();
        return await _db.Clientes.AsNoTracking()
            .Where(c => c.NumeroDocumento.Contains(texto)
                     || (c.Nombres + " " + c.Apellidos).Contains(texto)
                     || (c.Email != null && c.Email.Contains(texto)))
            .OrderBy(c => c.Apellidos).ThenBy(c => c.Nombres)
            .ToListAsync();
    }

    public Task<Cliente?> ObtenerPorDocumentoAsync(string numeroDocumento)
        => _db.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.NumeroDocumento == numeroDocumento);

    public Task<bool> ExisteDocumentoAsync(string numeroDocumento, int? excluirId = null)
        => _db.Clientes.AnyAsync(c => c.NumeroDocumento == numeroDocumento && c.Id != excluirId);

    public Task<Cliente?> ObtenerPorEmailAsync(string email)
    {
        var normalizado = email.Trim().ToLower();
        return _db.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email != null && c.Email.ToLower() == normalizado);
    }

    public Task<bool> ExisteEmailAsync(string email, int? excluirId = null)
    {
        var normalizado = email.Trim().ToLower();
        return _db.Clientes.AnyAsync(c => c.Email != null && c.Email.ToLower() == normalizado && c.Id != excluirId);
    }

    public Task<bool> TieneReservacionesAsync(int clienteId)
        => _db.Reservaciones.AnyAsync(r => r.ClienteId == clienteId);

    public Task<int> ContarEstadiasCompletadasAsync(int clienteId)
        => _db.Reservaciones.CountAsync(r => r.ClienteId == clienteId && r.Estado == EstadoReservacion.Completada);
}

public class TipoHabitacionRepositorio : Repositorio<TipoHabitacion>, ITipoHabitacionRepositorio
{
    public TipoHabitacionRepositorio(HotelDbContext db) : base(db) { }

    public Task<bool> TieneHabitacionesAsync(int tipoId)
        => _db.Habitaciones.AnyAsync(h => h.TipoHabitacionId == tipoId);
}

public class CategoriaPlatilloRepositorio : Repositorio<CategoriaPlatillo>, ICategoriaPlatilloRepositorio
{
    public CategoriaPlatilloRepositorio(HotelDbContext db) : base(db) { }
}
