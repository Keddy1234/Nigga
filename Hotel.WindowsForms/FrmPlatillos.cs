using Hotel.Dominio.Entidades;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms;

public partial class FrmPlatillos : Form
{
    private List<CategoriaPlatillo> _categorias = new();
    private List<Platillo> _platillos = new();
    private int? _platilloSeleccionadoId;
    private int? _categoriaSeleccionadaId;
    private bool _actualizandoControles;

    public FrmPlatillos()
    {
        InitializeComponent();
        ConfigurarEventos();
    }

    private void ConfigurarEventos()
    {
        Load += async (_, _) => await CargarDatosAsync();

        cmbCategoria.SelectedIndexChanged += (_, _) =>
        {
            if (_actualizandoControles) return;
            FiltrarPorCategoriaSeleccionada();
        };

        cmbNombrePlatillo.SelectedIndexChanged += (_, _) =>
        {
            if (!_actualizandoControles)
                AplicarPlantillaPlatillo();
        };

        btnNuevoPlatillo.Click += (_, _) => LimpiarFormularioPlatillo();
        btnGuardarPlatillo.Click += async (_, _) => await GuardarPlatilloAsync();
        btnModificarPlatillo.Click += async (_, _) => await ModificarPlatilloAsync();
        btnDesactivarPlatillo.Click += async (_, _) => await DesactivarPlatilloAsync();
        dgvPlatillos.SelectionChanged += (_, _) => SeleccionarPlatilloDeTabla();

        btnNuevaCategoria.Click += (_, _) => LimpiarFormularioCategoria();
        btnGuardarCategoria.Click += async (_, _) => await GuardarCategoriaAsync();
        btnModificarCategoria.Click += async (_, _) => await ModificarCategoriaAsync();
        dgvCategorias.SelectionChanged += (_, _) => SeleccionarCategoriaDeTabla();
    }

    private int ObtenerCategoriaIdActual()
    {
        return cmbCategoria.SelectedValue is int id ? id : CatalogoLocal.CategoriaDesayuno;
    }

    private void FiltrarPorCategoriaSeleccionada()
    {
        int catId = ObtenerCategoriaIdActual();
        var deEstaCategoria = _platillos.Where(p => p.CategoriaId == catId).ToList();

        _actualizandoControles = true;
        cmbNombrePlatillo.Items.Clear();
        foreach (var p in deEstaCategoria)
        {
            if (!cmbNombrePlatillo.Items.Contains(p.Nombre))
                cmbNombrePlatillo.Items.Add(p.Nombre);
        }

        if (cmbNombrePlatillo.Items.Count > 0)
            cmbNombrePlatillo.SelectedIndex = 0;
        else
            cmbNombrePlatillo.Text = string.Empty;
        _actualizandoControles = false;

        AplicarPlantillaPlatillo();
        RefrescarTablaPlatillos();
    }

    private Platillo? BuscarPlatilloBase(string nombre)
    {
        int catId = ObtenerCategoriaIdActual();
        return _platillos.FirstOrDefault(p => p.CategoriaId == catId && p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            ?? _platillos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            ?? CatalogoLocal.Platillos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    private void AplicarPlantillaPlatillo()
    {
        string nombre = cmbNombrePlatillo.Text.Trim();
        var basePlat = BuscarPlatilloBase(nombre);
        if (basePlat == null) return;

        _actualizandoControles = true;
        txtDescripcionPlatillo.Text = basePlat.Descripcion ?? string.Empty;
        nudPrecio.Value = Math.Clamp(basePlat.Precio, nudPrecio.Minimum, nudPrecio.Maximum);
        nudTiempo.Value = Math.Clamp(basePlat.TiempoPreparacionMinutos, (int)nudTiempo.Minimum, (int)nudTiempo.Maximum);
        chkDisponible.Checked = basePlat.Disponible;
        _actualizandoControles = false;
    }

    private async Task CargarDatosAsync()
    {
        try
        {
            _categorias = await ApiClient.GetAsync<List<CategoriaPlatillo>>("api/platillos/categorias") ?? new();
            _platillos = await ApiClient.GetAsync<List<Platillo>>("api/platillos") ?? new();
        }
        catch
        {
            _categorias = CatalogoLocal.Categorias;
            _platillos = CatalogoLocal.Platillos;
        }

        if (_categorias.Count == 0) _categorias = CatalogoLocal.Categorias;
        if (_platillos.Count == 0 || !_platillos.Any(p => p.CategoriaId == CatalogoLocal.CategoriaBebidas && p.Precio == 0m))
            _platillos = CatalogoLocal.Platillos;

        RefrescarComboCategorias();
        RefrescarTablaCategorias();
        FiltrarPorCategoriaSeleccionada();
    }

    private void RefrescarComboCategorias()
    {
        _actualizandoControles = true;
        var activas = _categorias.Where(c => c.Activo).ToList();
        cmbCategoria.DataSource = null;
        cmbCategoria.DataSource = activas;
        cmbCategoria.DisplayMember = "Nombre";
        cmbCategoria.ValueMember = "Id";
        _actualizandoControles = false;
    }

    private void RefrescarTablaCategorias()
    {
        dgvCategorias.Rows.Clear();
        foreach (var cat in _categorias)
        {
            dgvCategorias.Rows.Add(
                cat.Id,
                cat.Nombre,
                cat.Descripcion ?? "-",
                cat.Activo ? "Sí" : "No");
        }
    }

    private void RefrescarTablaPlatillos()
    {
        int catId = ObtenerCategoriaIdActual();
        dgvPlatillos.Rows.Clear();
        foreach (var p in _platillos.Where(x => x.CategoriaId == catId))
        {
            var nombreCat = p.Categoria?.Nombre
                ?? _categorias.FirstOrDefault(c => c.Id == p.CategoriaId)?.Nombre
                ?? "General";

            string textoPrecio = p.Precio <= 0m ? "Gratis (₡0.00)" : $"₡{p.Precio:N2}";

            dgvPlatillos.Rows.Add(
                p.Id,
                nombreCat,
                p.Nombre,
                textoPrecio,
                $"{p.TiempoPreparacionMinutos} min",
                p.Disponible ? "Sí" : "No");
        }
    }

    private void SeleccionarPlatilloDeTabla()
    {
        var valorId = dgvPlatillos.CurrentRow?.Cells["colPlatId"].Value?.ToString();
        if (!int.TryParse(valorId, out var id))
            return;

        var platillo = _platillos.FirstOrDefault(p => p.Id == id);
        if (platillo == null) return;

        _platilloSeleccionadoId = platillo.Id;
        _actualizandoControles = true;

        if (!cmbNombrePlatillo.Items.Contains(platillo.Nombre))
            cmbNombrePlatillo.Items.Add(platillo.Nombre);
        cmbNombrePlatillo.Text = platillo.Nombre;

        txtDescripcionPlatillo.Text = platillo.Descripcion ?? string.Empty;
        nudPrecio.Value = Math.Clamp(platillo.Precio, nudPrecio.Minimum, nudPrecio.Maximum);
        nudTiempo.Value = Math.Clamp(platillo.TiempoPreparacionMinutos, (int)nudTiempo.Minimum, (int)nudTiempo.Maximum);
        chkDisponible.Checked = platillo.Disponible;

        _actualizandoControles = false;
    }

    private void LimpiarFormularioPlatillo()
    {
        _platilloSeleccionadoId = null;
        dgvPlatillos.ClearSelection();
        _actualizandoControles = true;
        cmbNombrePlatillo.Text = string.Empty;
        txtDescripcionPlatillo.Clear();
        nudPrecio.Value = ObtenerCategoriaIdActual() == CatalogoLocal.CategoriaBebidas ? 3500m : 12500m;
        nudTiempo.Value = ObtenerCategoriaIdActual() == CatalogoLocal.CategoriaBebidas ? 5 : 15;
        chkDisponible.Checked = true;
        _actualizandoControles = false;
        cmbNombrePlatillo.Focus();
    }

    private async Task GuardarPlatilloAsync()
    {
        if (!ValidarPlatillo(out var modelo)) return;

        try
        {
            var creado = await ApiClient.PostAsync<Platillo, Platillo>("api/platillos", modelo);
            if (creado != null && !CatalogoLocal.Platillos.Any(x => x.Nombre.Equals(creado.Nombre, StringComparison.OrdinalIgnoreCase)))
                CatalogoLocal.Platillos.Add(creado);
            await CargarDatosAsync();
        }
        catch
        {
            modelo.Id = _platillos.Count > 0 ? _platillos.Max(x => x.Id) + 1 : 1;
            modelo.Categoria = _categorias.FirstOrDefault(c => c.Id == modelo.CategoriaId);
            if (!ReferenceEquals(_platillos, CatalogoLocal.Platillos))
                CatalogoLocal.Platillos.Add(modelo);
            _platillos.Add(modelo);
            FiltrarPorCategoriaSeleccionada();
        }

        string precioMsg = modelo.Precio <= 0m ? "Gratis (₡0.00)" : $"₡{modelo.Precio:N2}";
        MessageBox.Show($"'{modelo.Nombre}' registrado en la categoría seleccionada con precio {precioMsg}.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task ModificarPlatilloAsync()
    {
        if (_platilloSeleccionadoId == null)
        {
            MessageBox.Show("Seleccione un registro de la tabla para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!ValidarPlatillo(out var modelo)) return;
        modelo.Id = _platilloSeleccionadoId.Value;

        try
        {
            await ApiClient.PutAsync($"api/platillos/{modelo.Id}", modelo);
            await CargarDatosAsync();
        }
        catch
        {
            var idx = _platillos.FindIndex(x => x.Id == modelo.Id);
            if (idx >= 0)
            {
                modelo.Categoria = _categorias.FirstOrDefault(c => c.Id == modelo.CategoriaId);
                _platillos[idx] = modelo;
            }
            var idxLocal = CatalogoLocal.Platillos.FindIndex(x => x.Id == modelo.Id);
            if (idxLocal >= 0)
                CatalogoLocal.Platillos[idxLocal] = modelo;

            FiltrarPorCategoriaSeleccionada();
        }

        MessageBox.Show("Registro actualizado correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task DesactivarPlatilloAsync()
    {
        if (_platilloSeleccionadoId == null)
        {
            MessageBox.Show("Seleccione un platillo o bebida de la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int idBorrar = _platilloSeleccionadoId.Value;
        var platillo = _platillos.FirstOrDefault(x => x.Id == idBorrar);
        string nombre = platillo?.Nombre ?? $"#{idBorrar}";

        if (MessageBox.Show($"¿Desea eliminar '{nombre}' del catálogo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            await ApiClient.DeleteAsync($"api/platillos/{idBorrar}");
        }
        catch
        {
        }

        _platillos.RemoveAll(x => x.Id == idBorrar);
        CatalogoLocal.Platillos.RemoveAll(x => x.Id == idBorrar);

        FiltrarPorCategoriaSeleccionada();
        LimpiarFormularioPlatillo();
        MessageBox.Show($"'{nombre}' fue eliminado del catálogo.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private bool ValidarPlatillo(out Platillo platillo)
    {
        platillo = new Platillo();
        string nombreBase = cmbNombrePlatillo.Text.Trim();
        if (string.IsNullOrWhiteSpace(nombreBase))
        {
            MessageBox.Show("Seleccione o escriba el nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (cmbCategoria.SelectedValue is not int categoriaId)
        {
            MessageBox.Show("Seleccione una categoría válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (categoriaId != CatalogoLocal.CategoriaBebidas && nudPrecio.Value <= 0)
        {
            MessageBox.Show("El precio fijo de la comida debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        platillo = new Platillo
        {
            Nombre = nombreBase,
            CategoriaId = categoriaId,
            Descripcion = txtDescripcionPlatillo.Text.Trim(),
            Precio = nudPrecio.Value,
            TiempoPreparacionMinutos = (int)nudTiempo.Value,
            Disponible = chkDisponible.Checked
        };
        return true;
    }

    private void SeleccionarCategoriaDeTabla()
    {
        var valorId = dgvCategorias.CurrentRow?.Cells["colCatId"].Value?.ToString();
        if (!int.TryParse(valorId, out var id))
            return;

        var cat = _categorias.FirstOrDefault(c => c.Id == id);
        if (cat == null) return;

        _categoriaSeleccionadaId = cat.Id;
        txtNombreCategoria.Text = cat.Nombre;
        txtDescripcionCategoria.Text = cat.Descripcion ?? string.Empty;
        chkCategoriaActiva.Checked = cat.Activo;
    }

    private void LimpiarFormularioCategoria()
    {
        _categoriaSeleccionadaId = null;
        txtNombreCategoria.Clear();
        txtDescripcionCategoria.Clear();
        chkCategoriaActiva.Checked = true;
        dgvCategorias.ClearSelection();
        txtNombreCategoria.Focus();
    }

    private async Task GuardarCategoriaAsync()
    {
        if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
        {
            MessageBox.Show("Ingrese el nombre de la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var nueva = new CategoriaPlatillo
        {
            Nombre = txtNombreCategoria.Text.Trim(),
            Descripcion = txtDescripcionCategoria.Text.Trim(),
            Activo = chkCategoriaActiva.Checked
        };

        try
        {
            await ApiClient.PostAsync<CategoriaPlatillo, CategoriaPlatillo>("api/platillos/categorias", nueva);
            await CargarDatosAsync();
        }
        catch
        {
            nueva.Id = _categorias.Count > 0 ? _categorias.Max(c => c.Id) + 1 : 1;
            _categorias.Add(nueva);
            RefrescarComboCategorias();
            RefrescarTablaCategorias();
        }

        LimpiarFormularioCategoria();
        MessageBox.Show("Categoría guardada correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task ModificarCategoriaAsync()
    {
        if (_categoriaSeleccionadaId == null)
        {
            MessageBox.Show("Seleccione una categoría para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
        {
            MessageBox.Show("Ingrese el nombre de la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var editada = new CategoriaPlatillo
        {
            Id = _categoriaSeleccionadaId.Value,
            Nombre = txtNombreCategoria.Text.Trim(),
            Descripcion = txtDescripcionCategoria.Text.Trim(),
            Activo = chkCategoriaActiva.Checked
        };

        try
        {
            await ApiClient.PutAsync($"api/platillos/categorias/{editada.Id}", editada);
            await CargarDatosAsync();
        }
        catch
        {
            var idx = _categorias.FindIndex(c => c.Id == editada.Id);
            if (idx >= 0) _categorias[idx] = editada;
            RefrescarComboCategorias();
            RefrescarTablaCategorias();
        }

        MessageBox.Show("Categoría modificada correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
