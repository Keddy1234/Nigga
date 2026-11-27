using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;
using Hotel.Dominio.Reglas;

namespace Hotel.LogicaNegocios.Servicios;

public class PlatilloServicio : IPlatilloServicio
{
    private readonly IUnidadTrabajo _uow;

    public PlatilloServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<CategoriaPlatillo>> ListarCategoriasAsync() => _uow.Categorias.ListarAsync();

    public async Task<CategoriaPlatillo> CrearCategoriaAsync(CategoriaPlatillo categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nombre))
            throw new ReglaNegocioException("El nombre de la categoría es obligatorio.");
        categoria.Id = 0;
        await _uow.Categorias.AgregarAsync(categoria);
        await _uow.GuardarAsync();
        return categoria;
    }

    public async Task ActualizarCategoriaAsync(int id, CategoriaPlatillo datos)
    {
        if (string.IsNullOrWhiteSpace(datos.Nombre))
            throw new ReglaNegocioException("El nombre de la categoría es obligatorio.");
        var cat = await _uow.Categorias.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Categoría no encontrada.");
        cat.Nombre = datos.Nombre.Trim();
        cat.Descripcion = datos.Descripcion;
        cat.Activo = datos.Activo;
        await _uow.GuardarAsync();
    }

    public Task<List<Platillo>> ListarAsync(int? categoriaId, bool soloDisponibles)
        => _uow.Platillos.ListarConCategoriaAsync(categoriaId, soloDisponibles);

    public async Task<Platillo> ObtenerAsync(int id)
        => await _uow.Platillos.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Platillo no encontrado.");

    public async Task<Platillo> CrearAsync(Platillo platillo)
    {
        await ValidarAsync(platillo);
        platillo.Id = 0;
        platillo.Categoria = null;
        await _uow.Platillos.AgregarAsync(platillo);
        await _uow.GuardarAsync();
        return platillo;
    }

    public async Task ActualizarAsync(int id, Platillo datos)
    {
        await ValidarAsync(datos);
        var p = await ObtenerAsync(id);
        p.CategoriaId = datos.CategoriaId;
        p.Nombre = datos.Nombre.Trim();
        p.Descripcion = datos.Descripcion;
        p.Precio = datos.Precio;
        p.TiempoPreparacionMinutos = datos.TiempoPreparacionMinutos;
        p.Disponible = datos.Disponible;
        await _uow.GuardarAsync();
    }

    public async Task DesactivarAsync(int id)
    {
        var p = await ObtenerAsync(id);
        p.Disponible = false;
        await _uow.GuardarAsync();
    }

    private async Task ValidarAsync(Platillo p)
    {
        if (string.IsNullOrWhiteSpace(p.Nombre))
            throw new ReglaNegocioException("El nombre del platillo es obligatorio.");
        if (p.Precio < 0)
            throw new ReglaNegocioException("El precio no puede ser negativo.");
        if (p.TiempoPreparacionMinutos < 0)
            throw new ReglaNegocioException("El tiempo de preparación no es válido.");
        if (await _uow.Categorias.ObtenerPorIdAsync(p.CategoriaId) == null)
            throw new ReglaNegocioException("La categoría no existe.");
    }
}

public class PedidoServicio : IPedidoServicio
{
    private readonly IUnidadTrabajo _uow;

    public PedidoServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<PedidoRestaurante>> BuscarAsync(FiltroPedidoDto filtro) => _uow.Pedidos.BuscarAsync(filtro);

    public async Task<PedidoRestaurante> ObtenerAsync(int id)
        => await _uow.Pedidos.ObtenerConDetallesAsync(id) ?? throw new NoEncontradoException("Pedido no encontrado.");

    public Task<List<TotalPedidoDto>> TotalesPorReservacionAsync(int reservacionId)
        => _uow.Pedidos.TotalesPorReservacionAsync(reservacionId);

    public async Task<PedidoRestaurante> CrearAsync(CrearPedidoDto dto)
    {
        await _uow.IniciarTransaccionAsync();
        try
        {
            var reservacion = await _uow.Reservaciones.ObtenerPorIdAsync(dto.ReservacionId)
                              ?? throw new NoEncontradoException("Reservación no encontrada.");
            if (reservacion.Estado != EstadoReservacion.EnCurso)
                throw new ReglaNegocioException("Solo se pueden cargar pedidos a huéspedes con estadía en curso.");

            var pedido = new PedidoRestaurante
            {
                ReservacionId = reservacion.Id,
                FechaPedido = DateTime.Now,
                EstadoPedido = EstadoPedido.Recibido
            };
            await ArmarPedidoAsync(pedido, dto, reservacion);

            await _uow.Pedidos.AgregarAsync(pedido);
            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
            return pedido;
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }

    public async Task ModificarAsync(int id, CrearPedidoDto dto)
    {
        await _uow.IniciarTransaccionAsync();
        try
        {
            var pedido = await _uow.Pedidos.ObtenerConDetallesAsync(id)
                         ?? throw new NoEncontradoException("Pedido no encontrado.");
            if (pedido.EstadoPedido != EstadoPedido.Recibido)
                throw new ReglaNegocioException("Solo se pueden modificar pedidos en estado Recibido.");

            pedido.Detalles.Clear();
            await ArmarPedidoAsync(pedido, dto, pedido.Reservacion!);

            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }

    public async Task AvanzarEstadoAsync(int id, EstadoPedido nuevoEstado)
    {
        var pedido = await _uow.Pedidos.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Pedido no encontrado.");
        if (nuevoEstado == EstadoPedido.Cancelado)
        {
            await CancelarAsync(id);
            return;
        }

        var siguiente = pedido.EstadoPedido switch
        {
            EstadoPedido.Recibido => EstadoPedido.EnCocina,
            EstadoPedido.EnCocina => EstadoPedido.EnCamino,
            EstadoPedido.EnCamino => EstadoPedido.Entregado,
            _ => (EstadoPedido?)null
        };
        if (siguiente != nuevoEstado)
            throw new ReglaNegocioException($"No se puede pasar de {pedido.EstadoPedido} a {nuevoEstado}.");

        pedido.EstadoPedido = nuevoEstado;
        await _uow.GuardarAsync();
    }

    public async Task CancelarAsync(int id)
    {
        var pedido = await _uow.Pedidos.ObtenerPorIdAsync(id) ?? throw new NoEncontradoException("Pedido no encontrado.");
        if (pedido.EstadoPedido != EstadoPedido.Recibido)
            throw new ReglaNegocioException("Solo se pueden cancelar pedidos en estado Recibido.");
        pedido.EstadoPedido = EstadoPedido.Cancelado;
        await _uow.GuardarAsync();
    }

    private async Task ArmarPedidoAsync(PedidoRestaurante pedido, CrearPedidoDto dto, Reservacion reservacion)
    {
        if (dto.Detalles.Count == 0)
            throw new ReglaNegocioException("El pedido debe tener al menos un platillo.");
        if (dto.Detalles.Any(d => d.Cantidad <= 0))
            throw new ReglaNegocioException("Las cantidades deben ser mayores que cero.");
        if (dto.FechaHoraDeseada.HasValue)
        {
            if (dto.FechaHoraDeseada < DateTime.Now.AddMinutes(-5))
                throw new ReglaNegocioException("La fecha y hora deseada no puede ser pasada.");
            if (dto.FechaHoraDeseada.Value.Date >= reservacion.FechaFin.Date.AddDays(1))
                throw new ReglaNegocioException("La fecha deseada está fuera de la estadía.");
        }

        var ids = dto.Detalles.Select(d => d.PlatilloId).Distinct().ToList();
        var platillos = await _uow.Platillos.ObtenerVariosAsync(ids);

        foreach (var linea in dto.Detalles.GroupBy(d => d.PlatilloId))
        {
            var platillo = platillos.FirstOrDefault(p => p.Id == linea.Key)
                           ?? throw new NoEncontradoException($"Platillo {linea.Key} no encontrado.");
            if (!platillo.Disponible || platillo.Categoria?.Activo == false)
                throw new ReglaNegocioException($"El platillo {platillo.Nombre} no está disponible.");

            int cantidad = linea.Sum(l => l.Cantidad);
            pedido.Detalles.Add(new DetallePedido
            {
                PlatilloId = platillo.Id,
                Cantidad = cantidad,
                PrecioUnitario = platillo.Precio,
                Subtotal = platillo.Precio * cantidad
            });
        }

        decimal subtotal = pedido.Detalles.Sum(d => d.Subtotal);
        decimal recargo = dto.EsRoomService ? Math.Round(subtotal * CalculadoraCosto.RecargoRoomService, 2) : 0;

        pedido.EsRoomService = dto.EsRoomService;
        pedido.FechaHoraDeseada = dto.FechaHoraDeseada;
        pedido.Instrucciones = dto.Instrucciones;
        pedido.Total = subtotal + recargo;
    }
}
