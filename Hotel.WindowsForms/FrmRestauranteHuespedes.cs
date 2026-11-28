using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Reglas;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms;

public partial class FrmRestauranteHuespedes : Form
{
    private List<Reservacion> _reservacionesActivas = new();
    private List<CategoriaPlatillo> _categorias = new();
    private List<Platillo> _platillos = new();
    private List<PedidoRestaurante> _pedidos = new();
    private readonly List<ItemCarrito> _carrito = new();
    private Platillo? _platilloSeleccionado;
    private bool _sincronizandoMenu;

    private sealed class ItemCarrito
    {
        public int PlatilloId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

    private sealed class OpcionReservacion
    {
        public int ReservacionId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    private sealed class OpcionPlatilloCombo
    {
        public int PlatilloId { get; set; }
        public string Texto { get; set; } = string.Empty;
    }

    private sealed record OpcionBebida(int PlatilloId, string Nombre, decimal Precio)
    {
        public string Etiqueta => Precio <= 0m
            ? $"{Nombre} (Gratis - ₡0.00)"
            : $"{Nombre} (₡{Precio:N2})";
    }

    public FrmRestauranteHuespedes()
    {
        InitializeComponent();
        TemaAdminRustico.AplicarEsteticaSubFormulario(
            this,
            "Restaurante y Room Service",
            "Toma de pedidos a habitación o mesa y control de comandas",
            TipoPaisajeAdmin.RestauranteRustico);
        ConfigurarControlesIniciales();
        ConfigurarEventos();
    }

    private void ConfigurarControlesIniciales()
    {
        if (cmbEstadoFiltro.Items.Count > 0) cmbEstadoFiltro.SelectedIndex = 0;
        if (cmbNuevoEstado.Items.Count > 0) cmbNuevoEstado.SelectedIndex = 0;
        dtpHoraDeseada.Value = DateTime.Now.AddMinutes(30);
        dtpFechaFiltro.Value = DateTime.Today;

        cmbMesa.Items.Clear();
        for (int i = 1; i <= 15; i++)
            cmbMesa.Items.Add($"Mesa #{i}");
        cmbMesa.SelectedIndex = 0;
        cmbMesa.Enabled = rbMesa.Checked;

        RefrescarComboBebidas();
    }

    private void RefrescarComboBebidas()
    {
        var origen = _platillos.Count > 0 ? _platillos : CatalogoLocal.Platillos;
        var bebidas = origen
            .Where(p => p.CategoriaId == CatalogoLocal.CategoriaBebidas && p.Disponible)
            .OrderBy(p => p.Precio)
            .Select(p => new OpcionBebida(p.Id, p.Nombre, p.Precio))
            .ToList();

        if (bebidas.Count == 0)
        {
            bebidas = CatalogoLocal.Platillos
                .Where(p => p.CategoriaId == CatalogoLocal.CategoriaBebidas)
                .Select(p => new OpcionBebida(p.Id, p.Nombre, p.Precio))
                .ToList();
        }

        cmbRefresco.DataSource = null;
        cmbRefresco.DataSource = bebidas;
        cmbRefresco.DisplayMember = "Etiqueta";
        cmbRefresco.Enabled = chkRefresco.Checked;
    }

    private void ConfigurarEventos()
    {
        Load += async (_, _) => await CargarDatosInicialesAsync();
        cmbCategoriaFiltro.SelectedIndexChanged += (_, _) =>
        {
            if (!_sincronizandoMenu)
            {
                _platilloSeleccionado = null;
                RenderizarTarjetasMenu();
            }
        };

        cmbPlatillo.SelectedIndexChanged += (_, _) =>
        {
            if (_sincronizandoMenu) return;
            if (cmbPlatillo.SelectedValue is int platId)
            {
                _platilloSeleccionado = _platillos.FirstOrDefault(p => p.Id == platId);
                ActualizarPrecioMostrado();
                ResaltarTarjetaSeleccionada();
            }
        };

        chkRefresco.CheckedChanged += (_, _) =>
        {
            cmbRefresco.Enabled = chkRefresco.Checked;
            ActualizarPrecioMostrado();
        };

        cmbRefresco.SelectedIndexChanged += (_, _) => ActualizarPrecioMostrado();

        rbRoomService.CheckedChanged += (_, _) => ActualizarModalidadEntrega();
        rbMesa.CheckedChanged += (_, _) => ActualizarModalidadEntrega();
        btnAgregar.Click += (_, _) => AgregarPlatilloSeleccionado();
        btnQuitar.Click += (_, _) => QuitarItemSeleccionado();
        btnNuevoPlatilloMenu.Click += async (_, _) =>
        {
            using var frm = new FrmPlatillos();
            frm.ShowDialog(this);
            await CargarDatosInicialesAsync();
        };
        btnCargarHabitacion.Click += async (_, _) => await ConfirmarPedidoAsync();

        btnBuscarPedidos.Click += async (_, _) => await BuscarPedidosAsync();
        btnCambiarEstado.Click += async (_, _) => await CambiarEstadoPedidoAsync();
        btnCancelarPedido.Click += async (_, _) => await CancelarPedidoAsync();
    }

    private async Task CargarDatosInicialesAsync()
    {
        try
        {
            _reservacionesActivas = await ApiClient.GetAsync<List<Reservacion>>("api/reservaciones?Estado=EnCurso") ?? new();
            _categorias = await ApiClient.GetAsync<List<CategoriaPlatillo>>("api/platillos/categorias") ?? new();
            _platillos = await ApiClient.GetAsync<List<Platillo>>("api/platillos?soloDisponibles=true") ?? new();
            _pedidos = await ApiClient.GetAsync<List<PedidoRestaurante>>("api/pedidos") ?? new();
        }
        catch
        {
            CargarRespaldoLocal();
        }

        if (_platillos.Count == 0 || !_platillos.Any(p => p.CategoriaId == CatalogoLocal.CategoriaBebidas && p.Precio == 0m))
            CargarRespaldoLocal();

        if (TemaAdminRustico.EstaCerradoODispuesto(this, cmbHabitacion, flpMenu, dgvPedidos)) return;

        PoblarHabitacionesActivas();
        PoblarCategoriasFiltro();
        RefrescarComboBebidas();
        RenderizarTarjetasMenu();
        RefrescarTablaPedidos(_pedidos);
    }

    private void PoblarHabitacionesActivas()
    {
        var opciones = _reservacionesActivas.Select(r => new OpcionReservacion
        {
            ReservacionId = r.Id,
            Descripcion = $"Hab. {r.Habitacion?.Numero ?? r.HabitacionId.ToString()} - {r.Cliente?.NombreCompleto ?? r.Codigo}"
        }).ToList();

        cmbHabitacion.DataSource = null;
        cmbHabitacion.DataSource = opciones;
        cmbHabitacion.DisplayMember = "Descripcion";
        cmbHabitacion.ValueMember = "ReservacionId";
    }

    private void PoblarCategoriasFiltro()
    {
        _sincronizandoMenu = true;
        // Solo categorías de comidas separadas por tiempo (Desayuno, Almuerzo, Cena, Postres).
        // Las bebidas nunca se mezclan aquí; van únicamente en el selector de Bebidas.
        var soloComidas = _categorias
            .Where(c => c.Activo && c.Id != CatalogoLocal.CategoriaBebidas && !c.Nombre.Equals("Bebidas", StringComparison.OrdinalIgnoreCase))
            .ToList();

        cmbCategoriaFiltro.DataSource = null;
        cmbCategoriaFiltro.DataSource = soloComidas;
        cmbCategoriaFiltro.DisplayMember = "Nombre";
        cmbCategoriaFiltro.ValueMember = "Id";
        _sincronizandoMenu = false;
    }

    private void RenderizarTarjetasMenu()
    {
        flpMenu.SuspendLayout();
        flpMenu.Controls.Clear();

        int categoriaId = cmbCategoriaFiltro.SelectedValue is int id ? id : CatalogoLocal.CategoriaDesayuno;
        // Solo mostrar comidas del tiempo seleccionado (Desayuno, Almuerzo, Cena o Postres), nunca bebidas.
        var filtrados = _platillos
            .Where(p => p.Disponible
                     && p.CategoriaId == categoriaId
                     && p.CategoriaId != CatalogoLocal.CategoriaBebidas)
            .ToList();

        _sincronizandoMenu = true;
        var itemsCombo = filtrados.Select(p => new OpcionPlatilloCombo
        {
            PlatilloId = p.Id,
            Texto = $"{p.Nombre} (₡{p.Precio:N2})"
        }).ToList();

        cmbPlatillo.DataSource = null;
        cmbPlatillo.DataSource = itemsCombo;
        cmbPlatillo.DisplayMember = "Texto";
        cmbPlatillo.ValueMember = "PlatilloId";

        if (_platilloSeleccionado == null || !filtrados.Any(f => f.Id == _platilloSeleccionado.Id))
            _platilloSeleccionado = filtrados.FirstOrDefault();

        if (_platilloSeleccionado != null)
            cmbPlatillo.SelectedValue = _platilloSeleccionado.Id;
        _sincronizandoMenu = false;

        ActualizarPrecioMostrado();

        foreach (var platillo in filtrados)
        {
            bool seleccionado = _platilloSeleccionado?.Id == platillo.Id;
            var tarjeta = new Panel
            {
                Width = 205,
                Height = 96,
                Margin = new Padding(8),
                BackColor = seleccionado ? Color.FromArgb(212, 178, 118) : Color.FromArgb(245, 232, 208),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = platillo
            };

            var lblNombre = new Label
            {
                Text = platillo.Nombre,
                Font = new Font("Georgia", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(58, 34, 18),
                Location = new Point(8, 8),
                Size = new Size(188, 36),
                Tag = platillo
            };

            var lblInfo = new Label
            {
                Text = $"₡{platillo.Precio:N2}  •  {platillo.TiempoPreparacionMinutos} min",
                Font = new Font("Georgia", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(92, 58, 33),
                Location = new Point(8, 48),
                Size = new Size(188, 18),
                Tag = platillo
            };

            var lblDesc = new Label
            {
                Text = string.IsNullOrWhiteSpace(platillo.Descripcion) ? "Especialidad de la posada" : platillo.Descripcion,
                Font = new Font("Georgia", 7.8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(110, 82, 56),
                Location = new Point(8, 68),
                Size = new Size(188, 22),
                Tag = platillo
            };

            void SeleccionarHandler(object? s, EventArgs e)
            {
                _platilloSeleccionado = platillo;
                _sincronizandoMenu = true;
                cmbPlatillo.SelectedValue = platillo.Id;
                _sincronizandoMenu = false;
                ActualizarPrecioMostrado();
                ResaltarTarjetaSeleccionada();
            }

            void DobleClickHandler(object? s, EventArgs e)
            {
                _platilloSeleccionado = platillo;
                _sincronizandoMenu = true;
                cmbPlatillo.SelectedValue = platillo.Id;
                _sincronizandoMenu = false;
                ActualizarPrecioMostrado();
                AgregarPlatilloSeleccionado();
            }

            tarjeta.Click += SeleccionarHandler;
            lblNombre.Click += SeleccionarHandler;
            lblInfo.Click += SeleccionarHandler;
            lblDesc.Click += SeleccionarHandler;

            tarjeta.DoubleClick += DobleClickHandler;
            lblNombre.DoubleClick += DobleClickHandler;
            lblInfo.DoubleClick += DobleClickHandler;

            tarjeta.Controls.Add(lblNombre);
            tarjeta.Controls.Add(lblInfo);
            tarjeta.Controls.Add(lblDesc);
            flpMenu.Controls.Add(tarjeta);
        }

        flpMenu.ResumeLayout();
    }

    private void ResaltarTarjetaSeleccionada()
    {
        foreach (Control ctrl in flpMenu.Controls)
        {
            if (ctrl is Panel p && p.Tag is Platillo plat)
            {
                p.BackColor = _platilloSeleccionado?.Id == plat.Id
                    ? Color.FromArgb(212, 178, 118)
                    : Color.FromArgb(245, 232, 208);
            }
        }
    }

    private void ActualizarPrecioMostrado()
    {
        if (_platilloSeleccionado == null)
        {
            txtPrecioPlatillo.Text = "₡0.00";
            return;
        }

        txtPrecioPlatillo.Text = $"₡{_platilloSeleccionado.Precio:N2}";
    }

    private void AgregarPlatilloSeleccionado()
    {
        if (_platilloSeleccionado == null && cmbPlatillo.SelectedValue is int idCombo)
            _platilloSeleccionado = _platillos.FirstOrDefault(p => p.Id == idCombo);

        if (_platilloSeleccionado == null)
        {
            MessageBox.Show("Seleccione una comida en el ComboBox o en las tarjetas del menú.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int cantidad = (int)nudCantidad.Value;

        // 1. Agregar la comida por separado
        AgregarOIncrementarItem(_platilloSeleccionado.Id, _platilloSeleccionado.Nombre, cantidad, _platilloSeleccionado.Precio);

        // 2. Si marcó Bebidas (Opcional), agregar la bebida como una línea separada (Agua gratis, fresco o cóctel)
        if (chkRefresco.Checked && cmbRefresco.SelectedItem is OpcionBebida bebidaSel)
        {
            string etiquetaBebida = bebidaSel.Precio <= 0m
                ? $"[Bebida] {bebidaSel.Nombre} (Gratis)"
                : $"[Bebida] {bebidaSel.Nombre}";
            AgregarOIncrementarItem(bebidaSel.PlatilloId, etiquetaBebida, cantidad, bebidaSel.Precio);
        }

        nudCantidad.Value = 1;
        RefrescarCarrito();
    }

    private void AgregarOIncrementarItem(int platilloId, string nombre, int cantidad, decimal precioUnitario)
    {
        var existente = _carrito.FirstOrDefault(x =>
            x.PlatilloId == platilloId &&
            x.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (existente != null)
        {
            existente.Cantidad += cantidad;
        }
        else
        {
            _carrito.Add(new ItemCarrito
            {
                PlatilloId = platilloId,
                Nombre = nombre,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario
            });
        }
    }

    private void QuitarItemSeleccionado()
    {
        if (dgvDetalle.CurrentRow == null) return;
        int idx = dgvDetalle.CurrentRow.Index;
        if (idx >= 0 && idx < _carrito.Count)
        {
            _carrito.RemoveAt(idx);
            RefrescarCarrito();
        }
    }

    private void RefrescarCarrito()
    {
        dgvDetalle.Rows.Clear();
        foreach (var item in _carrito)
        {
            string textoPrecio = item.PrecioUnitario <= 0m ? "Gratis (₡0.00)" : $"₡{item.PrecioUnitario:N2}";
            string textoSubtotal = item.Subtotal <= 0m ? "Gratis (₡0.00)" : $"₡{item.Subtotal:N2}";

            dgvDetalle.Rows.Add(
                item.Nombre,
                item.Cantidad,
                textoPrecio,
                textoSubtotal);
        }

        RecalcularTotalesCarrito();
    }

    private void ActualizarModalidadEntrega()
    {
        cmbMesa.Enabled = rbMesa.Checked;
        btnCargarHabitacion.Text = rbMesa.Checked ? "Finalizar Pedido" : "Cargar a Habitación";
        RecalcularTotalesCarrito();
    }

    private void RecalcularTotalesCarrito()
    {
        decimal subtotal = _carrito.Sum(x => x.Subtotal);
        decimal total = CalculadoraCosto.CalcularTotalPedido(subtotal, rbRoomService.Checked);
        decimal recargo = total - subtotal;

        lblSubtotal.Text = $"₡{subtotal:N2}";
        lblRecargo.Text = $"₡{recargo:N2}";
        lblTotal.Text = $"₡{total:N2}";
    }

    private async Task ConfirmarPedidoAsync()
    {
        if (cmbHabitacion.SelectedValue is not int reservacionId)
        {
            MessageBox.Show("Seleccione una habitación con estadía activa.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_carrito.Count == 0)
        {
            MessageBox.Show("Agregue al menos una comida al pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string mesaAsignada = cmbMesa.SelectedItem?.ToString() ?? "Mesa #1";
        string prefijoEntrega = rbMesa.Checked ? $"[{mesaAsignada}] " : "[Room Service] ";
        var resumenArticulos = string.Join(", ", _carrito.Select(c => $"{c.Cantidad}x {c.Nombre}"));
        var notaFinal = string.IsNullOrWhiteSpace(txtInstrucciones.Text)
            ? $"{prefijoEntrega}{resumenArticulos}"
            : $"{prefijoEntrega}{txtInstrucciones.Text.Trim()} ({resumenArticulos})";

        var dto = new CrearPedidoDto
        {
            ReservacionId = reservacionId,
            EsRoomService = rbRoomService.Checked,
            FechaHoraDeseada = dtpHoraDeseada.Value,
            Instrucciones = notaFinal,
            Detalles = _carrito
                .GroupBy(x => x.PlatilloId)
                .Select(g => new DetallePedidoDto
                {
                    PlatilloId = g.Key,
                    Cantidad = g.Sum(i => i.Cantidad)
                }).ToList()
        };

        try
        {
            var creado = await ApiClient.PostAsync<CrearPedidoDto, PedidoRestaurante>("api/pedidos", dto);
            if (creado != null) _pedidos.Insert(0, creado);
            await BuscarPedidosAsync();
        }
        catch
        {
            decimal subtotal = _carrito.Sum(x => x.Subtotal);
            decimal total = CalculadoraCosto.CalcularTotalPedido(subtotal, rbRoomService.Checked);
            var res = _reservacionesActivas.FirstOrDefault(r => r.Id == reservacionId);

            var pedidoLocal = new PedidoRestaurante
            {
                Id = _pedidos.Count > 0 ? _pedidos.Max(p => p.Id) + 1 : 1,
                ReservacionId = reservacionId,
                Reservacion = res,
                FechaPedido = DateTime.Now,
                FechaHoraDeseada = dtpHoraDeseada.Value,
                EsRoomService = rbRoomService.Checked,
                Instrucciones = notaFinal,
                EstadoPedido = EstadoPedido.Recibido,
                Total = total
            };
            _pedidos.Insert(0, pedidoLocal);
            RefrescarTablaPedidos(_pedidos);
        }

        _carrito.Clear();
        txtInstrucciones.Clear();
        chkRefresco.Checked = false;
        RefrescarCarrito();

        if (rbMesa.Checked)
        {
            MessageBox.Show($"Pedido finalizado exitosamente.\nMesa asignada: {mesaAsignada}", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (cmbMesa.Items.Count > 0)
                cmbMesa.SelectedIndex = (cmbMesa.SelectedIndex + 1) % cmbMesa.Items.Count;
        }
        else
        {
            MessageBox.Show("Pedido registrado y cargado a la cuenta de la habitación.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private async Task BuscarPedidosAsync()
    {
        string estadoTexto = cmbEstadoFiltro.SelectedItem?.ToString() ?? "Todos";
        string fechaIso = dtpFechaFiltro.Value.Date.ToString("yyyy-MM-dd");

        try
        {
            string url = $"api/pedidos?Fecha={fechaIso}";
            if (estadoTexto != "Todos") url += $"&Estado={estadoTexto}";
            var lista = await ApiClient.GetAsync<List<PedidoRestaurante>>(url) ?? new();
            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvPedidos)) return;
            _pedidos = lista;
            RefrescarTablaPedidos(lista);
        }
        catch
        {
            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvPedidos)) return;
            var filtrados = _pedidos.Where(p => p.FechaPedido.Date == dtpFechaFiltro.Value.Date);
            if (estadoTexto != "Todos" && Enum.TryParse<EstadoPedido>(estadoTexto, out var est))
                filtrados = filtrados.Where(p => p.EstadoPedido == est);

            RefrescarTablaPedidos(filtrados.ToList());
        }
    }

    private void RefrescarTablaPedidos(IEnumerable<PedidoRestaurante> pedidos)
    {
        if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvPedidos)) return;

        dgvPedidos.Rows.Clear();
        foreach (var p in pedidos)
        {
            var res = p.Reservacion ?? _reservacionesActivas.FirstOrDefault(r => r.Id == p.ReservacionId);
            string hab = res?.Habitacion?.Numero ?? $"Hab #{res?.HabitacionId ?? p.ReservacionId}";
            if (!p.EsRoomService && !string.IsNullOrWhiteSpace(p.Instrucciones) && p.Instrucciones.StartsWith("[Mesa #"))
            {
                int fin = p.Instrucciones.IndexOf(']');
                if (fin > 1)
                {
                    string mesa = p.Instrucciones.Substring(1, fin - 1);
                    hab = $"{mesa} (Hab. {hab})";
                }
            }

            string cli = res?.Cliente?.NombreCompleto ?? res?.Codigo ?? "Huésped";

            dgvPedidos.Rows.Add(
                p.Id,
                hab,
                cli,
                p.FechaPedido.ToString("dd/MM/yyyy HH:mm"),
                p.EstadoPedido.ToString(),
                $"₡{p.Total:N2}");
        }
    }

    private async Task CambiarEstadoPedidoAsync()
    {
        if (dgvPedidos.CurrentRow == null || dgvPedidos.CurrentRow.Cells["colPedId"].Value == null)
        {
            MessageBox.Show("Seleccione un pedido de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["colPedId"].Value);
        string nuevoEstadoStr = cmbNuevoEstado.SelectedItem?.ToString() ?? "EnCocina";
        if (!Enum.TryParse<EstadoPedido>(nuevoEstadoStr, out var nuevoEstado)) return;

        try
        {
            await ApiClient.PatchAsync($"api/pedidos/{pedidoId}/estado?estado={nuevoEstado}");
            await BuscarPedidosAsync();
            MessageBox.Show($"Estado del pedido #{pedidoId} actualizado a {nuevoEstado}.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            var pedido = _pedidos.FirstOrDefault(x => x.Id == pedidoId);
            if (pedido != null && EsTransicionValida(pedido.EstadoPedido, nuevoEstado))
            {
                pedido.EstadoPedido = nuevoEstado;
                RefrescarTablaPedidos(_pedidos);
                MessageBox.Show($"Estado del pedido #{pedidoId} actualizado a {nuevoEstado}.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(ex.Message, "Transición no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private async Task CancelarPedidoAsync()
    {
        if (dgvPedidos.CurrentRow == null || dgvPedidos.CurrentRow.Cells["colPedId"].Value == null)
        {
            MessageBox.Show("Seleccione un pedido para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int pedidoId = Convert.ToInt32(dgvPedidos.CurrentRow.Cells["colPedId"].Value);

        try
        {
            await ApiClient.PostSinCuerpoAsync($"api/pedidos/{pedidoId}/cancelar");
            await BuscarPedidosAsync();
            MessageBox.Show($"Pedido #{pedidoId} cancelado correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            var pedido = _pedidos.FirstOrDefault(x => x.Id == pedidoId);
            if (pedido != null && pedido.EstadoPedido == EstadoPedido.Recibido)
            {
                pedido.EstadoPedido = EstadoPedido.Cancelado;
                RefrescarTablaPedidos(_pedidos);
                MessageBox.Show($"Pedido #{pedidoId} cancelado correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(ex.Message, "No se puede cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private static bool EsTransicionValida(EstadoPedido actual, EstadoPedido siguiente) =>
        (actual == EstadoPedido.Recibido && siguiente == EstadoPedido.EnCocina) ||
        (actual == EstadoPedido.EnCocina && siguiente == EstadoPedido.EnCamino) ||
        (actual == EstadoPedido.EnCamino && siguiente == EstadoPedido.Entregado);

    private void CargarRespaldoLocal()
    {
        _categorias = CatalogoLocal.Categorias;
        _platillos = CatalogoLocal.Platillos;

        _reservacionesActivas = ApiClient.ReservacionesLocales
            .Where(r => r.Estado == EstadoReservacion.EnCurso || r.Estado == EstadoReservacion.Confirmada)
            .ToList();

        foreach (var p in ApiClient.PedidosLocales)
        {
            p.Reservacion ??= ApiClient.ReservacionesLocales.FirstOrDefault(r => r.Id == p.ReservacionId);
        }
        _pedidos = ApiClient.PedidosLocales;
    }
}
