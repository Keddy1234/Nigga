using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms
{
    public partial class FrmHabitaciones : Form
    {
        private int? _idSeleccionado;
        private List<TipoHabitacion> _tipos = new();
        private List<Habitacion> _habitaciones = new();

        public FrmHabitaciones()
        {
            InitializeComponent();
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today.AddDays(3);

            txtNumero.MaxLength = 8;
            txtNumero.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
            txtNumero.TextChanged += (s, e) =>
            {
                string limpio = new string(txtNumero.Text.Where(char.IsDigit).Take(8).ToArray());
                if (txtNumero.Text != limpio)
                {
                    int pos = Math.Min(txtNumero.SelectionStart, limpio.Length);
                    txtNumero.Text = limpio;
                    txtNumero.SelectionStart = pos;
                }
            };

            Load += async (s, e) => await CargarTodoAsync();
            btnConsultarMapa.Click += async (s, e) => await RenderizarMapaAsync();
            btnNuevo.Click += (s, e) => Limpiar();
            btnGuardar.Click += async (s, e) => await GuardarAsync();
            btnModificar.Click += async (s, e) => await ModificarAsync();
            btnDesactivar.Click += async (s, e) => await DesactivarAsync();
            dgvHabitaciones.SelectionChanged += (s, e) => SeleccionarFila();
        }

        private async Task CargarTodoAsync()
        {
            try
            {
                _tipos = await ApiClient.GetAsync<List<TipoHabitacion>>("api/tiposhabitacion") ?? new();
                _habitaciones = await ApiClient.GetAsync<List<Habitacion>>("api/habitaciones") ?? new();
            }
            catch
            {
                _tipos = CatalogoLocal.Tipos;
                _habitaciones = CatalogoLocal.Habitaciones;
            }

            cmbTipo.DataSource = null;
            cmbTipo.DataSource = _tipos;
            cmbTipo.DisplayMember = "Nombre";
            cmbTipo.ValueMember = "Id";
            if (cmbEstado.SelectedIndex < 0) cmbEstado.SelectedIndex = 0;

            RefrescarTabla();
            await RenderizarMapaAsync();
        }

        private async Task RenderizarMapaAsync()
        {
            flpMapa.Controls.Clear();
            foreach (var h in _habitaciones.OrderBy(x => x.Piso).ThenBy(x => x.Numero))
            {
                string nombreTipo = h.TipoHabitacion?.Nombre
                    ?? _tipos.FirstOrDefault(t => t.Id == h.TipoHabitacionId)?.Nombre
                    ?? "";

                var btn = new Button
                {
                    Width = 148,
                    Height = 96,
                    Margin = new Padding(8),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.FromArgb(245, 232, 208),
                    BackColor = ObtenerColorEstado(h.EstadoOperativo),
                    Font = new Font("Georgia", 9F, FontStyle.Bold),
                    Text = $"Hab. {h.Numero} (Piso {h.Piso})\n{nombreTipo}\n[{h.EstadoOperativo}]",
                    Tag = h
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(46, 31, 20);
                btn.Click += (s, e) =>
                {
                    tabHabitaciones.SelectedTab = tabMantenimiento;
                    CargarEnFormulario(h);
                };
                flpMapa.Controls.Add(btn);
            }
            await Task.CompletedTask;
        }

        private static Color ObtenerColorEstado(EstadoHabitacion estado) => estado switch
        {
            EstadoHabitacion.Disponible => Color.FromArgb(58, 125, 68),
            EstadoHabitacion.Ocupada => Color.FromArgb(168, 50, 40),
            EstadoHabitacion.EnLimpieza => Color.FromArgb(204, 154, 43),
            EstadoHabitacion.Reservada => Color.FromArgb(52, 101, 148),
            _ => Color.FromArgb(115, 110, 102)
        };

        private void RefrescarTabla()
        {
            dgvHabitaciones.Rows.Clear();
            foreach (var h in _habitaciones)
            {
                string tipo = h.TipoHabitacion?.Nombre
                    ?? _tipos.FirstOrDefault(t => t.Id == h.TipoHabitacionId)?.Nombre
                    ?? h.TipoHabitacionId.ToString();

                dgvHabitaciones.Rows.Add(
                    h.Id,
                    h.Numero,
                    h.Piso,
                    tipo,
                    h.EstadoOperativo.ToString(),
                    h.TieneJacuzzi ? "Sí" : "No",
                    h.TieneVistaMar ? "Sí" : "No");
            }
        }

        private void SeleccionarFila()
        {
            if (dgvHabitaciones.CurrentRow == null || dgvHabitaciones.CurrentRow.Index < 0) return;
            int id = Convert.ToInt32(dgvHabitaciones.CurrentRow.Cells[0].Value);
            var hab = _habitaciones.FirstOrDefault(x => x.Id == id);
            if (hab != null) CargarEnFormulario(hab);
        }

        private void CargarEnFormulario(Habitacion hab)
        {
            _idSeleccionado = hab.Id;
            txtNumero.Text = hab.Numero;
            nudPiso.Value = Math.Max(1, hab.Piso);
            cmbTipo.SelectedValue = hab.TipoHabitacionId;
            cmbEstado.SelectedItem = hab.EstadoOperativo.ToString();
            chkJacuzzi.Checked = hab.TieneJacuzzi;
            chkVistaMar.Checked = hab.TieneVistaMar;
        }

        private void Limpiar()
        {
            _idSeleccionado = null;
            txtNumero.Clear();
            nudPiso.Value = 1;
            if (cmbTipo.Items.Count > 0) cmbTipo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            chkJacuzzi.Checked = false;
            chkVistaMar.Checked = false;
            txtNumero.Focus();
        }

        private Habitacion LeerFormulario()
        {
            Enum.TryParse<EstadoHabitacion>(cmbEstado.SelectedItem?.ToString(), out var estado);
            int tipoId = cmbTipo.SelectedValue is int id ? id : (_tipos.FirstOrDefault()?.Id ?? 1);
            return new Habitacion
            {
                Id = _idSeleccionado ?? 0,
                Numero = txtNumero.Text.Trim(),
                Piso = (int)nudPiso.Value,
                TipoHabitacionId = tipoId,
                EstadoOperativo = estado,
                TieneJacuzzi = chkJacuzzi.Checked,
                TieneVistaMar = chkVistaMar.Checked,
                TipoHabitacion = _tipos.FirstOrDefault(t => t.Id == tipoId)
            };
        }

        private async Task GuardarAsync()
        {
            var hab = LeerFormulario();
            if (string.IsNullOrWhiteSpace(hab.Numero))
            {
                MessageBox.Show("Ingrese el número de habitación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_habitaciones.Any(h => h.Numero == hab.Numero))
            {
                MessageBox.Show($"La habitación {hab.Numero} ya existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await ApiClient.PostAsync("api/habitaciones", hab);
                await CargarTodoAsync();
                Limpiar();
                MessageBox.Show("Habitación registrada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                hab.Id = _habitaciones.Count > 0 ? _habitaciones.Max(x => x.Id) + 1 : 1;
                _habitaciones.Add(hab);
                RefrescarTabla();
                await RenderizarMapaAsync();
                Limpiar();
                MessageBox.Show("Habitación registrada correctamente.", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task ModificarAsync()
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione una habitación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hab = LeerFormulario();
            try
            {
                await ApiClient.PutAsync($"api/habitaciones/{_idSeleccionado}", hab);
                await CargarTodoAsync();
                MessageBox.Show("Habitación actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                var idx = _habitaciones.FindIndex(x => x.Id == _idSeleccionado);
                if (idx >= 0) _habitaciones[idx] = hab;
                RefrescarTabla();
                await RenderizarMapaAsync();
                MessageBox.Show("Habitación actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task DesactivarAsync()
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccione una habitación para desactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await ApiClient.DeleteAsync($"api/habitaciones/{_idSeleccionado}");
                await CargarTodoAsync();
            }
            catch
            {
                var hab = _habitaciones.FirstOrDefault(x => x.Id == _idSeleccionado);
                if (hab != null) hab.EstadoOperativo = EstadoHabitacion.Bloqueada;
                RefrescarTabla();
                await RenderizarMapaAsync();
            }
            MessageBox.Show("Habitación marcada como Bloqueada.", "Estado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
