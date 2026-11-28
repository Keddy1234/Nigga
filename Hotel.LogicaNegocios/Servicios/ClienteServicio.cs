using System.Text.RegularExpressions;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;

namespace Hotel.LogicaNegocios.Servicios;

public class ClienteServicio : IClienteServicio
{
    private static readonly Regex FormatoCorreo = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    private static readonly Regex FormatoSoloDigitos = new(@"^\d{5,20}$", RegexOptions.Compiled);
    private static readonly Regex FormatoPasaporte = new(@"^[A-Za-z0-9\-]{5,20}$", RegexOptions.Compiled);
    private static readonly Regex FormatoTelefonoCaracteres = new(@"^[\d\s+\-()]{8,20}$", RegexOptions.Compiled);
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
        var existenteDoc = await _uow.Clientes.ObtenerPorDocumentoAsync(cliente.NumeroDocumento);
        var existenteEmail = await _uow.Clientes.ObtenerPorEmailAsync(cliente.Email!);

        if (existenteDoc != null && existenteEmail != null && existenteDoc.Id != existenteEmail.Id)
            throw new ConflictoException("El número de documento y el correo electrónico ya pertenecen a cuentas distintas.");

        var existente = existenteEmail ?? existenteDoc;
        if (existente != null)
        {
            if (await _uow.Clientes.TieneReservacionesAsync(existente.Id))
            {
                if (existenteEmail != null)
                    throw new ConflictoException("Ya existe una cuenta registrada con ese correo electrónico. Como ya tiene reservaciones, ingrese desde 'Iniciar Sesión'.");
                throw new ConflictoException("Ya existe una cuenta registrada con ese número de documento. Inicie sesión con su correo electrónico registrado.");
            }

            // Si la cuenta existe pero aún tiene 0 reservaciones, se actualiza su perfil para que pueda completar su primera reserva.
            var rastreado = await _uow.Clientes.ObtenerPorIdAsync(existente.Id)
                            ?? throw new NoEncontradoException("Cliente no encontrado.");
            rastreado.TipoDocumento = cliente.TipoDocumento;
            rastreado.NumeroDocumento = cliente.NumeroDocumento;
            rastreado.Nombres = cliente.Nombres;
            rastreado.Apellidos = cliente.Apellidos;
            rastreado.Telefono = cliente.Telefono;
            rastreado.Email = cliente.Email;
            await _uow.GuardarAsync();
            return rastreado;
        }

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
        if (await _uow.Clientes.ExisteEmailAsync(datos.Email!, id))
            throw new ConflictoException("Ya existe otro cliente con ese correo electrónico.");

        cliente.TipoDocumento = datos.TipoDocumento;
        cliente.NumeroDocumento = datos.NumeroDocumento;
        cliente.Nombres = datos.Nombres;
        cliente.Apellidos = datos.Apellidos;
        cliente.Email = datos.Email;
        cliente.Telefono = datos.Telefono;
        await _uow.GuardarAsync();
    }

    public async Task<Cliente> IniciarSesionPorEmailAsync(string email)
    {
        var correo = (email ?? string.Empty).Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(correo) || !FormatoCorreo.IsMatch(correo))
            throw new ReglaNegocioException("Ingrese un correo electrónico válido.");

        var cliente = await _uow.Clientes.ObtenerPorEmailAsync(correo)
            ?? throw new NoEncontradoException("No existe una cuenta registrada con ese correo electrónico. Cree una cuenta para realizar su primera reservación.");

        if (!await _uow.Clientes.TieneReservacionesAsync(cliente.Id))
            throw new ReglaNegocioException("Este correo aún no tiene ninguna reservación registrada. Solo pueden iniciar sesión clientes que ya hayan registrado una reservación.");

        return cliente;
    }

    private static void Validar(Cliente c)
    {
        if (!Enum.IsDefined(typeof(TipoDocumento), c.TipoDocumento))
            throw new ReglaNegocioException("El tipo de documento es obligatorio y debe ser válido (Cédula, DNI o Pasaporte).");

        c.NumeroDocumento = (c.NumeroDocumento ?? string.Empty).Trim();
        if (c.TipoDocumento == TipoDocumento.Pasaporte)
        {
            if (!FormatoPasaporte.IsMatch(c.NumeroDocumento))
                throw new ReglaNegocioException("El número de pasaporte es obligatorio y debe tener entre 5 y 20 caracteres alfanuméricos.");
        }
        else if (!FormatoSoloDigitos.IsMatch(c.NumeroDocumento))
        {
            throw new ReglaNegocioException("El número de documento es obligatorio y debe contener entre 5 y 20 dígitos numéricos.");
        }

        c.Nombres = (c.Nombres ?? string.Empty).Trim();
        if (c.Nombres.Length < 2 || c.Nombres.Length > 100)
            throw new ReglaNegocioException("Los nombres son obligatorios (entre 2 y 100 caracteres).");
        if (ContieneCaracteresPeligrosos(c.Nombres))
            throw new ReglaNegocioException("Los nombres contienen caracteres no permitidos (<, > o caracteres de control).");

        c.Apellidos = (c.Apellidos ?? string.Empty).Trim();
        if (c.Apellidos.Length < 2 || c.Apellidos.Length > 100)
            throw new ReglaNegocioException("Los apellidos son obligatorios (entre 2 y 100 caracteres).");
        if (ContieneCaracteresPeligrosos(c.Apellidos))
            throw new ReglaNegocioException("Los apellidos contienen caracteres no permitidos (<, > o caracteres de control).");

        c.Telefono = (c.Telefono ?? string.Empty).Trim();
        int cantidadDigitosTelefono = c.Telefono.Count(char.IsDigit);
        if (string.IsNullOrWhiteSpace(c.Telefono)
            || !FormatoTelefonoCaracteres.IsMatch(c.Telefono)
            || cantidadDigitosTelefono < 8
            || cantidadDigitosTelefono > 15)
        {
            throw new ReglaNegocioException("El teléfono es obligatorio y debe contener entre 8 y 15 dígitos válidos.");
        }

        c.Email = (c.Email ?? string.Empty).Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(c.Email) || c.Email.Length > 150 || !FormatoCorreo.IsMatch(c.Email))
            throw new ReglaNegocioException("El correo electrónico es obligatorio y debe tener un formato válido (máximo 150 caracteres).");
    }

    private static bool ContieneCaracteresPeligrosos(string texto)
        => texto.Any(ch => ch == '<' || ch == '>' || char.IsControl(ch));
}
