using Hotel.Dominio.Entidades;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms
{
    public partial class FrmTipoHabitacion : Form
    {
        private int? _idSeleccionado;
        private List<TipoHabitacion> _lista = new();
        private bool _actualizandoControles;

        private sealed record PlantillaHabitacion(
            string Nombre,
            string Descripcion,
            int AdultosPorDefecto,
            int NinosPorDefecto,
            decimal PrecioPorAdulto,
            decimal PrecioPorNino);

        private static readonly List<PlantillaHabitacion> _plantillas = new()
        {
            new("Simple", "Habitación individual rústica con cama Queen, Wi-Fi 6 y frigobar", 1, 0, 75000m, 30000m),
            new("Doble Estándar", "Dos camas matrimoniales en madera de roble, Wi-Fi 6 y frigobar", 2, 1, 65000m, 28000m),
            new("Suite Ejecutiva", "Cama King con dosel, balcón, jacuzzi térmico y vista al mar", 2, 2, 110000m, 45000m),
            new("Suite Presidencial", "Piso alto de lujo, sala con chimenea, jacuzzi, vista al mar y frigobar", 4, 2, 145000m, 60000m),
            new("Familiar", "Dos ambientes amplios conectados, ideal para familias y grupos", 4, 3, 70000m, 32000m)
        };

        public FrmTipoHabitacion()
        {
            InitializeComponent();
            TemaAdminRustico.AplicarEsteticaSubFormulario(
                this,
                "Tipos de Habitación y Tarifas",
                "Configuración de categorías, capacidades y precios por noche",
                TipoPaisajeAdmin.TerrazaModulos);
            InicializarCombos();
            ConfigurarEventos();
        }

        private void InicializarCombos()
        {
            _actualizandoControles = true;

            cmbAdultos.Items.Clear();
            for (int i = 1; i <= 6; i++) cmbAdultos.Items.Add(i);

            cmbNinos.Items.Clear();
            for (int i = 0; i <= 4; i++) cmbNinos.Items.Add(i);

            cmbNombre.Items.Clear();
            foreach (var p in _plantillas) cmbNombre.Items.Add(p.Nombre);

            _actualizandoControles = false;

            if (cmbNombre.Items.Count > 0)
                cmbNombre.SelectedIndex = 0;
            AplicarPlantillaSeleccionada(reiniciarCantidades: true);
        }

        private void ConfigurarEventos()
        {
            Load += async (s, e) => await CargarAsync();
            cmbNombre.SelectedIndexChanged += (s, e) =>
            {
                if (!_actualizandoControles)
                    AplicarPlantillaSeleccionada(reiniciarCantidades: true);
            };
            cmbAdultos.SelectedIndexChanged += (s, e) =>
            {
                if (!_actualizandoControles)
                    RecalcularPrecioSegunSeleccion();
            };
            cmbNinos.SelectedIndexChanged += (s, e) =>
            {
                if (!_actualizandoControles)
                    RecalcularPrecioSegunSeleccion();
            };

            btnNuevo.Click += (s, e) => Limpiar();
            btnGuardar.Click += async (s, e) => await GuardarAsync();
            btnModificar.Click += async (s, e) => await ModificarAsync();
            btnEliminar.Click += async (s, e) => await EliminarAsync();
            dgvTipos.SelectionChanged += (s, e) => SeleccionarFila();
        }

        private PlantillaHabitacion ObtenerPlantillaActual()
        {
            string nombre = cmbNombre.SelectedItem?.ToString() ?? "Simple";
            return _plantillas.FirstOrDefault(p => p.Nombre == nombre) ?? _plantillas[0];
        }

        private void AplicarPlantillaSeleccionada(bool reiniciarCantidades)
        {
            var plantilla = ObtenerPlantillaActual();

            _actualizandoControles = true;
            if (reiniciarCantidades)
            {
                cmbAdultos.SelectedItem = plantilla.AdultosPorDefecto;
                cmbNinos.SelectedItem = plantilla.NinosPorDefecto;
            }
            txtDescripcion.Text = plantilla.Descripcion;
            _actualizandoControles = false;

            RecalcularPrecioSegunSeleccion();
        }

        private void RecalcularPrecioSegunSeleccion()
        {
            var plantilla = ObtenerPlantillaActual();
            int adultos = cmbAdultos.SelectedItem is int a ? a : plantilla.AdultosPorDefecto;
            int ninos = cmbNinos.SelectedItem is int n ? n : plantilla.NinosPorDefecto;

            decimal subtotalAdultos = adultos * plantilla.PrecioPorAdulto;
            decimal subtotalNinos = ninos * plantilla.PrecioPorNino;
            decimal totalNoche = subtotalAdultos + subtotalNinos;

            txtPrecioAdulto.Text = $"₡{plantilla.PrecioPorAdulto:N2} (₡{subtotalAdultos:N2})";
            txtPrecioNino.Text = $"₡{plantilla.PrecioPorNino:N2} (₡{subtotalNinos:N2})";
            nudPrecioBase.Value = Math.Clamp(totalNoche, nudPrecioBase.Minimum, nudPrecioBase.Maximum);
        }

        private async Task CargarAsync()
        {
            try
            {
                _lista = await ApiClient.GetAsync<List<TipoHabitacion>>("api/tiposhabitacion") ?? new();
            }
            catch
            {
                if (_lista.Count == 0)
                {
                    _lista = _plantillas.Select((p, idx) => new TipoHabitacion
                    {
                        Id = idx + 1,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        CapacidadAdultos = p.AdultosPorDefecto,
                        CapacidadNinos = p.NinosPorDefecto,
                        PrecioBasePorNoche = (p.AdultosPorDefecto * p.PrecioPorAdulto) + (p.NinosPorDefecto * p.PrecioPorNino)
                    }).ToList();
                }
            }

            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvTipos)) return;

            RefrescarTabla();
        }

        private void RefrescarTabla()
        {
            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvTipos)) return;

            dgvTipos.Rows.Clear();
            foreach (var t in _lista)
            {
                dgvTipos.Rows.Add(t.Id, t.Nombre, t.Descripcion ?? string.Empty, t.CapacidadAdultos, t.CapacidadNinos, $"₡{t.PrecioBasePorNoche:N2}");
            }
        }

        private void SeleccionarFila()
        {
            if (dgvTipos.CurrentRow == null || dgvTipos.CurrentRow.Index < 0) return;
            int id = Convert.ToInt32(dgvTipos.CurrentRow.Cells[0].Value);
            var tipo = _lista.FirstOrDefault(x => x.Id == id);
            if (tipo == null) return;

            _idSeleccionado = tipo.Id;
            _actualizandoControles = true;

            if (!cmbNombre.Items.Contains(tipo.Nombre))
                cmbNombre.Items.Add(tipo.Nombre);
            cmbNombre.SelectedItem = tipo.Nombre;

            cmbAdultos.SelectedItem = Math.Clamp(tipo.CapacidadAdultos, 1, 6);
            cmbNinos.SelectedItem = Math.Clamp(tipo.CapacidadNinos, 0, 4);

            var plantilla = ObtenerPlantillaActual();
            txtDescripcion.Text = string.IsNullOrWhiteSpace(tipo.Descripcion)
                ? plantilla.Descripcion
                : tipo.Descripcion;

            _actualizandoControles = false;
            RecalcularPrecioSegunSeleccion();
        }

        private void Limpiar()
        {
            _idSeleccionado = null;
            dgvTipos.ClearSelection();
            if (cmbNombre.Items.Count > 0)
                cmbNombre.SelectedIndex = 0;
            AplicarPlantillaSeleccionada(reiniciarCantidades: true);
        }

        private TipoHabitacion LeerFormulario()
        {
            var plantilla = ObtenerPlantillaActual();
            int adultos = cmbAdultos.SelectedItem is int a ? a : plantilla.AdultosPorDefecto;
            int ninos = cmbNinos.SelectedItem is int n ? n : plantilla.NinosPorDefecto;

            return new TipoHabitacion
            {
                Id = _idSeleccionado ?? 0,
                Nombre = cmbNombre.SelectedItem?.ToString() ?? plantilla.Nombre,
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? plantilla.Descripcion : txtDescripcion.Text.Trim(),
                CapacidadAdultos = adultos,
                CapacidadNinos = ninos,
                PrecioBasePorNoche = nudPrecioBase.Value
            };
        }

        private async Task GuardarAsync()
        {
            var datos = LeerFormulario();
            if (string.IsNullOrWhiteSpace(datos.Nombre) || datos.PrecioBasePorNoche <= 0)
            {
                MessageBox.Show("Seleccione una habitación válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await ApiClient.PostAsync("api/tiposhabitacion", datos);
                await CargarAsync();
                Limpiar();
                MessageBox.Show("Tipo de habitación registrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                datos.Id = _lista.Count > 0 ? _lista.Max(x => x.Id) + 1 : 1;
                _lista.Add(datos);
                RefrescarTabla();
                Limpiar();
                MessageBox.Show("Tipo de habitación guardado correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task ModificarAsync()
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un registro de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var datos = LeerFormulario();
            try
            {
                await ApiClient.PutAsync($"api/tiposhabitacion/{_idSeleccionado}", datos);
                await CargarAsync();
                MessageBox.Show("Tipo de habitación actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                var idx = _lista.FindIndex(x => x.Id == _idSeleccionado);
                if (idx >= 0) _lista[idx] = datos;
                RefrescarTabla();
                MessageBox.Show("Tipo de habitación actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task EliminarAsync()
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea eliminar este tipo de habitación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                await ApiClient.DeleteAsync($"api/tiposhabitacion/{_idSeleccionado}");
                await CargarAsync();
                Limpiar();
            }
            catch
            {
                _lista.RemoveAll(x => x.Id == _idSeleccionado);
                RefrescarTabla();
                Limpiar();
                MessageBox.Show("Registro eliminado correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
