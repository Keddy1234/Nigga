using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Reglas;
using Hotel.Dominio.ValueObjects;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms
{
    public partial class FrmNuevaReservacion : Form
    {
        private List<TipoHabitacion> _tipos = new();
        private List<HabitacionDisponibleDto> _disponibles = new();
        private Cliente? _clienteActual;
        private bool _sincronizando;
        private decimal _ultimoTotalCotizado;

        private sealed record InfoTarifaTipo(
            string Nombre,
            string Descripcion,
            int AdultosBase,
            int NinosBase,
            decimal PrecioAdulto,
            decimal PrecioNino);

        private static readonly List<InfoTarifaTipo> _tarifasPorTipo = new()
        {
            new("Simple", "Habitación individual rústica con cama Queen, Wi-Fi 6 y frigobar", 1, 0, 75000m, 30000m),
            new("Doble Estándar", "Dos camas matrimoniales en madera de roble, Wi-Fi 6 y frigobar", 2, 1, 65000m, 28000m),
            new("Suite Ejecutiva", "Cama King con dosel, balcón, jacuzzi térmico y vista al mar", 2, 2, 110000m, 45000m),
            new("Suite Presidencial", "Piso alto de lujo, sala con chimenea, jacuzzi, vista al mar y frigobar", 4, 2, 145000m, 60000m),
            new("Familiar", "Dos ambientes amplios conectados, ideal para familias y grupos", 4, 3, 70000m, 32000m)
        };

        private sealed class ItemComboHabitacion
        {
            public int Id { get; set; }
            public string Texto { get; set; } = string.Empty;
        }

        public FrmNuevaReservacion()
        {
            InitializeComponent();
            InicializarCombosHuespedes();
            ConfigurarEventos();
        }

        private void InicializarCombosHuespedes()
        {
            _sincronizando = true;
            cmbAdultos.Items.Clear();
            for (int i = 1; i <= 6; i++) cmbAdultos.Items.Add(i);
            cmbAdultos.SelectedItem = 2;

            cmbNinos.Items.Clear();
            for (int i = 0; i <= 4; i++) cmbNinos.Items.Add(i);
            cmbNinos.SelectedItem = 0;
            _sincronizando = false;
        }

        private void ConfigurarEventos()
        {
            calFechas.SelectionStart = DateTime.Today.AddDays(1);
            calFechas.SelectionEnd = DateTime.Today.AddDays(3);
            cmbTipoDocumento.SelectedIndex = 0;
            if (cmbPlazoTasaCero.Items.Count > 0)
                cmbPlazoTasaCero.SelectedIndex = 0;

            rbContado.CheckedChanged += (s, e) => ActualizarTextoPlanPago();
            rbTasaCero.CheckedChanged += (s, e) =>
            {
                cmbPlazoTasaCero.Enabled = rbTasaCero.Checked;
                ActualizarTextoPlanPago();
            };
            cmbPlazoTasaCero.SelectedIndexChanged += (s, e) => ActualizarTextoPlanPago();

            RestringirSoloNumeros(txtNumeroDocumento, 10);
            RestringirSoloNumeros(txtTelefono, 8);

            Load += async (s, e) => await InicializarAsync();
            btnBuscarDisponibles.Click += async (s, e) => await BuscarDisponiblesAsync();
            btnBuscarCliente.Click += async (s, e) => await BuscarClienteAsync();

            cmbTipoHabitacion.SelectedIndexChanged += async (s, e) =>
            {
                if (_sincronizando) return;
                AplicarPresetSegunTipo(cmbTipoHabitacion.Text, cambiarHuespedes: true);
                await BuscarDisponiblesAsync();
            };

            cmbHabitacion.SelectedIndexChanged += (s, e) =>
            {
                if (_sincronizando) return;
                if (cmbHabitacion.SelectedValue is int habId)
                {
                    SincronizarSeleccionTabla(habId);
                    var hab = _disponibles.FirstOrDefault(x => x.Id == habId);
                    if (hab != null)
                        AplicarPresetSegunTipo(hab.Tipo, cambiarHuespedes: true);
                }
            };

            dgvDisponibles.SelectionChanged += (s, e) =>
            {
                if (_sincronizando || dgvDisponibles.CurrentRow == null || dgvDisponibles.CurrentRow.Index < 0) return;
                int id = Convert.ToInt32(dgvDisponibles.CurrentRow.Cells[0].Value);
                _sincronizando = true;
                cmbHabitacion.SelectedValue = id;
                _sincronizando = false;

                var hab = _disponibles.FirstOrDefault(x => x.Id == id);
                if (hab != null)
                    AplicarPresetSegunTipo(hab.Tipo, cambiarHuespedes: false);
            };

            cmbAdultos.SelectedIndexChanged += (s, e) =>
            {
                if (!_sincronizando) ActualizarResumenPreciosYDescripcion(ObtenerTipoSeleccionadoActual());
            };

            cmbNinos.SelectedIndexChanged += (s, e) =>
            {
                if (!_sincronizando) ActualizarResumenPreciosYDescripcion(ObtenerTipoSeleccionadoActual());
            };

            btnAnterior.Click += (s, e) =>
            {
                if (tabAsistente.SelectedIndex > 0)
                    tabAsistente.SelectedIndex--;
            };

            btnSiguiente.Click += async (s, e) =>
            {
                if (tabAsistente.SelectedIndex < tabAsistente.TabCount - 1)
                {
                    tabAsistente.SelectedIndex++;
                    if (tabAsistente.SelectedIndex == 2)
                        await ActualizarCotizacionAsync();
                }
            };

            tabAsistente.SelectedIndexChanged += async (s, e) =>
            {
                if (tabAsistente.SelectedIndex == 2)
                    await ActualizarCotizacionAsync();
            };

            btnConfirmar.Click += async (s, e) => await ConfirmarReservacionAsync();
        }

        private static void RestringirSoloNumeros(TextBox caja, int maxDigitos)
        {
            caja.MaxLength = maxDigitos;
            caja.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
            caja.TextChanged += (s, e) =>
            {
                string limpio = new string(caja.Text.Where(char.IsDigit).Take(maxDigitos).ToArray());
                if (caja.Text != limpio)
                {
                    int pos = Math.Min(caja.SelectionStart, limpio.Length);
                    caja.Text = limpio;
                    caja.SelectionStart = pos;
                }
            };
        }

        private string ObtenerTipoSeleccionadoActual()
        {
            var hab = HabitacionSeleccionada();
            if (hab != null && !string.IsNullOrWhiteSpace(hab.Tipo))
                return hab.Tipo;
            return cmbTipoHabitacion.Text;
        }

        private InfoTarifaTipo BuscarInfoTarifa(string? nombreTipo)
        {
            if (!string.IsNullOrWhiteSpace(nombreTipo))
            {
                var encontrada = _tarifasPorTipo.FirstOrDefault(t =>
                    t.Nombre.Equals(nombreTipo, StringComparison.OrdinalIgnoreCase));
                if (encontrada != null) return encontrada;
            }
            return _tarifasPorTipo[0];
        }

        private void AplicarPresetSegunTipo(string? nombreTipo, bool cambiarHuespedes)
        {
            var info = BuscarInfoTarifa(nombreTipo);
            _sincronizando = true;
            if (cambiarHuespedes)
            {
                cmbAdultos.SelectedItem = info.AdultosBase;
                cmbNinos.SelectedItem = info.NinosBase;
            }
            _sincronizando = false;

            ActualizarResumenPreciosYDescripcion(info.Nombre);
        }

        private void ActualizarResumenPreciosYDescripcion(string? nombreTipo)
        {
            var info = BuscarInfoTarifa(nombreTipo);
            int adultos = cmbAdultos.SelectedItem is int a ? a : info.AdultosBase;
            int ninos = cmbNinos.SelectedItem is int n ? n : info.NinosBase;
            int totalHuespedes = Math.Max(1, adultos + ninos);
            nudHuespedes.Value = Math.Clamp(totalHuespedes, (int)nudHuespedes.Minimum, (int)nudHuespedes.Maximum);

            decimal totalAdultos = adultos * info.PrecioAdulto;
            decimal totalNinos = ninos * info.PrecioNino;
            lblPreciosInfo.Text = $"Adulto: ₡{info.PrecioAdulto:N2} (₡{totalAdultos:N2}) | Niño: ₡{info.PrecioNino:N2} (₡{totalNinos:N2})";

            var tipoEntidad = _tipos.FirstOrDefault(t => t.Nombre.Equals(info.Nombre, StringComparison.OrdinalIgnoreCase));
            txtDescripcionHabitacion.Text = !string.IsNullOrWhiteSpace(tipoEntidad?.Descripcion)
                ? tipoEntidad.Descripcion
                : info.Descripcion;
        }

        private void SincronizarSeleccionTabla(int habitacionId)
        {
            _sincronizando = true;
            foreach (DataGridViewRow fila in dgvDisponibles.Rows)
            {
                if (Convert.ToInt32(fila.Cells[0].Value) == habitacionId)
                {
                    fila.Selected = true;
                    dgvDisponibles.CurrentCell = fila.Cells[1];
                    break;
                }
            }
            _sincronizando = false;
        }

        private async Task InicializarAsync()
        {
            try
            {
                _tipos = await ApiClient.GetAsync<List<TipoHabitacion>>("api/tiposhabitacion") ?? new();
            }
            catch
            {
                _tipos = _tarifasPorTipo.Select((t, idx) => new TipoHabitacion
                {
                    Id = idx + 1,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion,
                    CapacidadAdultos = t.AdultosBase,
                    CapacidadNinos = t.NinosBase,
                    PrecioBasePorNoche = (t.AdultosBase * t.PrecioAdulto) + (t.NinosBase * t.PrecioNino)
                }).ToList();
            }

            _sincronizando = true;
            var opciones = new List<TipoHabitacion> { new() { Id = 0, Nombre = "Todos" } };
            opciones.AddRange(_tipos);
            cmbTipoHabitacion.DataSource = opciones;
            cmbTipoHabitacion.DisplayMember = "Nombre";
            cmbTipoHabitacion.ValueMember = "Id";
            _sincronizando = false;

            await BuscarDisponiblesAsync();
            if (_disponibles.Count > 0)
                AplicarPresetSegunTipo(_disponibles[0].Tipo, cambiarHuespedes: true);
        }

        private (DateTime entrada, DateTime salida) ObtenerFechas()
        {
            var ini = calFechas.SelectionStart.Date;
            var fin = calFechas.SelectionEnd.Date;
            if (fin <= ini) fin = ini.AddDays(1);
            return (ini, fin);
        }

        private int ObtenerCantidadHuespedes()
        {
            int adultos = cmbAdultos.SelectedItem is int a ? a : 1;
            int ninos = cmbNinos.SelectedItem is int n ? n : 0;
            return Math.Max(1, adultos + ninos);
        }

        private async Task BuscarDisponiblesAsync()
        {
            var (entrada, salida) = ObtenerFechas();
            int huespedes = ObtenerCantidadHuespedes();
            int? tipoId = cmbTipoHabitacion.SelectedValue is int id && id > 0 ? id : null;

            try
            {
                string url = $"api/habitaciones/disponibles?entrada={entrada:yyyy-MM-dd}&salida={salida:yyyy-MM-dd}&huespedes={huespedes}";
                if (tipoId.HasValue) url += $"&tipoId={tipoId.Value}";
                _disponibles = await ApiClient.GetAsync<List<HabitacionDisponibleDto>>(url) ?? new();
            }
            catch
            {
                var ocupadasEnFechas = ApiClient.ReservacionesLocales
                    .Where(r => r.Estado != EstadoReservacion.Cancelada
                             && r.Estado != EstadoReservacion.Completada
                             && r.FechaInicio < salida && entrada < r.FechaFin)
                    .Select(r => r.HabitacionId)
                    .ToHashSet();

                _disponibles = CatalogoLocal.Habitaciones
                    .Where(h => !h.FueraDeServicio
                             && h.EstadoOperativo != EstadoHabitacion.Ocupada
                             && !ocupadasEnFechas.Contains(h.Id))
                    .Select(h =>
                    {
                        var info = BuscarInfoTarifa(CatalogoLocal.NombreTipo(h.TipoHabitacionId));
                        return new HabitacionDisponibleDto
                        {
                            Id = h.Id,
                            Numero = h.Numero,
                            Piso = h.Piso,
                            Tipo = CatalogoLocal.NombreTipo(h.TipoHabitacionId),
                            Capacidad = CatalogoLocal.CapacidadTipo(h.TipoHabitacionId),
                            PrecioBasePorNoche = (info.AdultosBase * info.PrecioAdulto) + (info.NinosBase * info.PrecioNino),
                            TieneJacuzzi = h.TieneJacuzzi,
                            TieneVistaMar = h.TieneVistaMar
                        };
                    })
                    .Where(h => h.Capacidad >= huespedes)
                    .OrderBy(h => h.Piso).ThenBy(h => h.Numero)
                    .ToList();

                if (tipoId.HasValue)
                {
                    var nombreFiltro = _tipos.FirstOrDefault(t => t.Id == tipoId.Value)?.Nombre;
                    if (!string.IsNullOrWhiteSpace(nombreFiltro))
                        _disponibles = _disponibles.Where(h => h.Tipo == nombreFiltro).ToList();
                }
            }

            _sincronizando = true;
            dgvDisponibles.Rows.Clear();
            foreach (var h in _disponibles)
            {
                dgvDisponibles.Rows.Add(h.Id, h.Numero, h.Piso, h.Tipo, h.Capacidad, $"₡{h.PrecioBasePorNoche:N2}");
            }

            var itemsCombo = _disponibles.Select(h => new ItemComboHabitacion
            {
                Id = h.Id,
                Texto = $"Hab. {h.Numero} - {h.Tipo} (₡{h.PrecioBasePorNoche:N2})"
            }).ToList();

            cmbHabitacion.DataSource = null;
            cmbHabitacion.DataSource = itemsCombo;
            cmbHabitacion.DisplayMember = "Texto";
            cmbHabitacion.ValueMember = "Id";
            _sincronizando = false;

            if (_disponibles.Count > 0)
                ActualizarResumenPreciosYDescripcion(_disponibles[0].Tipo);
        }

        private HabitacionDisponibleDto? HabitacionSeleccionada()
        {
            if (cmbHabitacion.SelectedValue is int idCombo)
            {
                var porCombo = _disponibles.FirstOrDefault(x => x.Id == idCombo);
                if (porCombo != null) return porCombo;
            }

            if (dgvDisponibles.CurrentRow == null || dgvDisponibles.CurrentRow.Index < 0)
                return _disponibles.FirstOrDefault();

            int id = Convert.ToInt32(dgvDisponibles.CurrentRow.Cells[0].Value);
            return _disponibles.FirstOrDefault(x => x.Id == id);
        }

        private async Task BuscarClienteAsync()
        {
            string doc = txtNumeroDocumento.Text.Trim();
            if (string.IsNullOrWhiteSpace(doc))
            {
                MessageBox.Show("Ingrese el número de documento a buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var encontrados = await ApiClient.GetAsync<List<Cliente>>($"api/clientes?buscar={Uri.EscapeDataString(doc)}") ?? new();
                _clienteActual = encontrados.FirstOrDefault(c => c.NumeroDocumento.Equals(doc, StringComparison.OrdinalIgnoreCase))
                    ?? encontrados.FirstOrDefault();

                if (_clienteActual != null)
                {
                    cmbTipoDocumento.SelectedItem = _clienteActual.TipoDocumento.ToString();
                    txtNumeroDocumento.Text = _clienteActual.NumeroDocumento;
                    txtNombres.Text = _clienteActual.Nombres;
                    txtApellidos.Text = _clienteActual.Apellidos;
                    txtEmail.Text = _clienteActual.Email ?? "";
                    txtTelefono.Text = _clienteActual.Telefono ?? "";
                    MessageBox.Show("Datos del cliente cargados.", "Cliente encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el cliente; complete los campos para registrarlo.", "Nuevo cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Complete los datos del cliente para continuar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task ActualizarCotizacionAsync()
        {
            var hab = HabitacionSeleccionada();
            if (hab == null) return;

            var (entrada, salida) = ObtenerFechas();
            int adultos = cmbAdultos.SelectedItem is int a ? a : 1;
            int ninos = cmbNinos.SelectedItem is int n ? n : 0;
            int huespedes = Math.Max(1, adultos + ninos);
            var info = BuscarInfoTarifa(hab.Tipo);
            decimal tarifaSegunPersonas = (adultos * info.PrecioAdulto) + (ninos * info.PrecioNino);

            CotizacionDto? cot = null;
            try
            {
                cot = await ApiClient.PostAsync<CotizarReservacionDto, CotizacionDto>("api/reservaciones/cotizar", new CotizarReservacionDto
                {
                    HabitacionId = hab.Id,
                    ClienteId = _clienteActual?.Id,
                    FechaInicio = entrada,
                    FechaFin = salida,
                    CantidadHuespedes = huespedes
                });
            }
            catch
            {
            }

            cot ??= CalculadoraCosto.Calcular(new RangoFechas(entrada, salida), tarifaSegunPersonas, huespedes, false);
            _ultimoTotalCotizado = cot.Total;

            lblNoches.Text = cot.Noches.ToString();
            lblTarifaBase.Text = $"₡{cot.TarifaBase:N2}";
            lblRecargo.Text = $"₡{cot.RecargoPersonasExtra:N2}";
            lblDescuento.Text = $"₡{cot.Descuento:N2}";
            lblSubtotal.Text = $"₡{cot.Subtotal:N2}";
            lblIva.Text = $"₡{cot.Iva:N2}";
            lblTotal.Text = $"₡{cot.Total:N2}";
            lblAnticipo.Text = $"₡{cot.AnticipoMinimo:N2}";

            ActualizarTextoPlanPago();
        }

        private int ObtenerMesesTasaCero()
        {
            string texto = cmbPlazoTasaCero.SelectedItem?.ToString() ?? "3 Meses";
            if (texto.StartsWith("12")) return 12;
            if (texto.StartsWith("6")) return 6;
            return 3;
        }

        private string ObtenerResumenPlanPago()
        {
            if (rbContado.Checked)
                return $"Pago de Contado (₡{_ultimoTotalCotizado:N2})";

            int meses = ObtenerMesesTasaCero();
            decimal cuota = meses > 0 ? Math.Round(_ultimoTotalCotizado / meses, 2) : _ultimoTotalCotizado;
            return $"Tasa Cero a {meses} meses (₡{cuota:N2} / mes)";
        }

        private void ActualizarTextoPlanPago()
        {
            if (rbContado.Checked)
            {
                lblDetallePlanPago.Text =
                    $"Modalidad: Pago de Contado (100%)\r\n" +
                    $"Total a cancelar: ₡{_ultimoTotalCotizado:N2}\r\n" +
                    $"Sin cargos ni intereses.";
            }
            else
            {
                int meses = ObtenerMesesTasaCero();
                decimal cuota = meses > 0 ? Math.Round(_ultimoTotalCotizado / meses, 2) : _ultimoTotalCotizado;
                lblDetallePlanPago.Text =
                    $"Modalidad: Tasa Cero ({meses} Meses)\r\n" +
                    $"Cuota mensual: ₡{cuota:N2} / mes\r\n" +
                    $"Total: ₡{_ultimoTotalCotizado:N2} (0% interés)";
            }
        }

        private async Task ConfirmarReservacionAsync()
        {
            var hab = HabitacionSeleccionada();
            if (hab == null)
            {
                MessageBox.Show("Seleccione una habitación disponible en el paso 1.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabAsistente.SelectedIndex = 0;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Complete el documento, nombres y apellidos del cliente en el paso 2.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabAsistente.SelectedIndex = 1;
                return;
            }

            var (entrada, salida) = ObtenerFechas();
            await ActualizarCotizacionAsync();
            string planPago = ObtenerResumenPlanPago();
            string obsFinal = string.IsNullOrWhiteSpace(txtObservaciones.Text)
                ? $"Modalidad: {planPago}"
                : $"{txtObservaciones.Text.Trim()} | Modalidad: {planPago}";

            try
            {
                if (_clienteActual == null)
                {
                    Enum.TryParse<TipoDocumento>(cmbTipoDocumento.SelectedItem?.ToString(), out var tipoDoc);
                    _clienteActual = await ApiClient.PostAsync<Cliente, Cliente>("api/clientes", new Cliente
                    {
                        TipoDocumento = tipoDoc,
                        NumeroDocumento = txtNumeroDocumento.Text.Trim(),
                        Nombres = txtNombres.Text.Trim(),
                        Apellidos = txtApellidos.Text.Trim(),
                        Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                        Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim()
                    });
                }

                var creada = await ApiClient.PostAsync<CrearReservacionDto, Reservacion>("api/reservaciones", new CrearReservacionDto
                {
                    ClienteId = _clienteActual!.Id,
                    HabitacionId = hab.Id,
                    FechaInicio = entrada,
                    FechaFin = salida,
                    CantidadHuespedes = ObtenerCantidadHuespedes(),
                    Observaciones = obsFinal
                });

                if (creada != null)
                {
                    creada.Cliente ??= _clienteActual;
                    creada.Habitacion ??= new Habitacion { Id = hab.Id, Numero = hab.Numero, Piso = hab.Piso };
                    ApiClient.ReservacionesLocales.RemoveAll(r => r.Codigo == creada.Codigo);
                    ApiClient.ReservacionesLocales.Insert(0, creada);
                }

                MessageBox.Show(
                    $"Reservación registrada con código {creada?.Codigo}.\nTotal: {lblTotal.Text} | {planPago}",
                    "Reservación creada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                string codigoLocal = $"RES-{entrada.Year}-{Random.Shared.Next(100, 999):D4}";
                int adultos = cmbAdultos.SelectedItem is int a ? a : 1;
                int ninos = cmbNinos.SelectedItem is int n ? n : 0;
                int huespedes = Math.Max(1, adultos + ninos);
                var info = BuscarInfoTarifa(hab.Tipo);
                decimal tarifaSegunPersonas = (adultos * info.PrecioAdulto) + (ninos * info.PrecioNino);
                var cot = CalculadoraCosto.Calcular(new RangoFechas(entrada, salida), tarifaSegunPersonas, huespedes, false);

                Enum.TryParse<TipoDocumento>(cmbTipoDocumento.SelectedItem?.ToString(), out var tipoDoc);
                var nuevoCliente = new Cliente
                {
                    Id = ApiClient.ReservacionesLocales.Count + 1,
                    TipoDocumento = tipoDoc,
                    NumeroDocumento = txtNumeroDocumento.Text.Trim(),
                    Nombres = txtNombres.Text.Trim(),
                    Apellidos = txtApellidos.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim()
                };

                ApiClient.ReservacionesLocales.Insert(0, new Reservacion
                {
                    Id = ApiClient.ReservacionesLocales.Count + 1,
                    Codigo = codigoLocal,
                    HabitacionId = hab.Id,
                    ClienteId = nuevoCliente.Id,
                    FechaInicio = entrada,
                    FechaFin = salida,
                    Estado = EstadoReservacion.Confirmada,
                    CostoTotal = cot.Total,
                    CantidadHuespedes = huespedes,
                    Observaciones = obsFinal,
                    Cliente = nuevoCliente,
                    Habitacion = new Habitacion { Id = hab.Id, Numero = hab.Numero, Piso = hab.Piso }
                });

                var habLocal = CatalogoLocal.Habitaciones.FirstOrDefault(h => h.Id == hab.Id);
                if (habLocal != null && habLocal.EstadoOperativo == EstadoHabitacion.Disponible)
                    habLocal.EstadoOperativo = EstadoHabitacion.Reservada;

                MessageBox.Show(
                    $"Reservación registrada exitosamente con código {codigoLocal}.\nHabitación {hab.Numero} | Total: {lblTotal.Text}\n{planPago}",
                    "Reservación registrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
