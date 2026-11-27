using System.Text.RegularExpressions;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;

namespace Hotel.LogicaNegocios.Servicios;

public class ClienteServicio : IClienteServicio
{
    private static readonly Regex FormatoCorreo = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    private readonly IUnidadTrabajo _uow;

    public ClienteServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<Cliente>> ListarAsync() => _uow.Clientes.ListarAsync();

    public async Task<Cliente> ObtenerAsync(int id)
        => await _uow.Clientes.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Cliente no encontrado.");

    public Task<List<Cliente>> BuscarAsync(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return _uow.Clientes.ListarAsync();
        return _uow.Clientes.BuscarAsync(texto);
    }

    public async Task<Cliente> CrearAsync(Cliente cliente)
    {
        Validar(cliente);
        if (await _uow.Clientes.ExisteDocumentoAsync(cliente.NumeroDocumento))
            throw new ConflictoException("Ya existe un cliente con ese número de documento.");

        cliente.Id = 0;
        cliente.FechaRegistro = DateTime.Now;
        await _uow.Clientes.AgregarAsync(cliente);
        await _uow.GuardarAsync();
        return cliente;
    }

    public async Task ActualizarAsync(int id, Cliente datos)
    {
        Validar(datos);
        var cliente = await ObtenerAsync(id);
        if (await _uow.Clientes.ExisteDocumentoAsync(datos.NumeroDocumento, id))
            throw new ConflictoException("Ya existe otro cliente con ese número de documento.");

        cliente.TipoDocumento = datos.TipoDocumento;
        cliente.NumeroDocumento = datos.NumeroDocumento.Trim();
        cliente.Nombres = datos.Nombres.Trim();
        cliente.Apellidos = datos.Apellidos.Trim();
        cliente.Email = datos.Email?.Trim();
        cliente.Telefono = datos.Telefono?.Trim();
        await _uow.GuardarAsync();
    }

    private static void Validar(Cliente c)
    {
        if (string.IsNullOrWhiteSpace(c.NumeroDocumento) || c.NumeroDocumento.Length > 20)
            throw new ReglaNegocioException("El número de documento es obligatorio (máximo 20 caracteres).");
        if (string.IsNullOrWhiteSpace(c.Nombres) || c.Nombres.Length > 100)
            throw new ReglaNegocioException("Los nombres son obligatorios (máximo 100 caracteres).");
        if (string.IsNullOrWhiteSpace(c.Apellidos) || c.Apellidos.Length > 100)
            throw new ReglaNegocioException("Los apellidos son obligatorios (máximo 100 caracteres).");
        if (!string.IsNullOrWhiteSpace(c.Email) && !FormatoCorreo.IsMatch(c.Email))
            throw new ReglaNegocioException("El correo electrónico no tiene un formato válido.");
    }
}
