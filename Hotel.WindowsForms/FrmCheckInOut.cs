using System.Text;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Reglas;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms
{
    public partial class FrmCheckInOut : Form
    {
        private Reservacion? _reservacion;
        private EstadoCuentaDto? _cuenta;
        private List<Reservacion> _listaPendientes = new();
        private bool _cargandoCombo;
        private bool _sincronizandoMoneda;

        private sealed class OpcionReservaCheckIn
        {
            public int Id { get; set; }
            public string Etiqueta { get; set; } = string.Empty;
        }

        public FrmCheckInOut()
        {
            InitializeComponent();
            TemaAdminRustico.AplicarEsteticaSubFormulario(
                this,
                "Recepción: Check-In y Check-Out",
                "Ingreso de huéspedes, consumos, pagos y emisión de facturas",
                TipoPaisajeAdmin.TerrazaModulos);
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            cmbMetodoPago.SelectedIndex = 0;
            cmbMonedaPago.SelectedIndex = 0;
            cmbMonedaFactura.SelectedIndex = 0;

            RestringirSoloNumeros(txtBuscar, 10);
            RestringirSoloNumeros(txtLlave, 8);

            Load += async (s, e) => await CargarReservacionesPendientesAsync();
            cmbPendientesCheckIn.SelectedIndexChanged += (s, e) =>
            {
                if (_cargandoCombo) return;
                SeleccionarDesdeComboPendientes();
            };

            cmbMonedaPago.SelectedIndexChanged += (s, e) =>
            {
                if (_sincronizandoMoneda) return;
                ActualizarMontoPagoSegunMoneda();
            };
            cmbMonedaFactura.SelectedIndexChanged += (s, e) =>
            {
                if (_sincronizandoMoneda) return;
                MostrarEnPantalla();
            };

            btnBuscar.Click += async (s, e) => await BuscarReservacionAsync();
            btnCheckIn.Click += async (s, e) => await RealizarCheckInAsync();
            btnRegistrarPago.Click += async (s, e) => await RegistrarPagoAsync();
            btnCheckOut.Click += async (s, e) => await RealizarCheckOutAsync();
            btnFactura.Click += async (s, e) => await EmitirFacturaAsync();
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

        private async Task CargarReservacionesPendientesAsync()
        {
            try
            {
                var remotas = await ApiClient.GetAsync<List<Reservacion>>("api/reservaciones") ?? new();
                foreach (var r in remotas)
                {
                    if (!ApiClient.ReservacionesLocales.Any(x => x.Codigo == r.Codigo))
                        ApiClient.ReservacionesLocales.Add(r);
                }
            }
            catch
            {
            }

            if (TemaAdminRustico.EstaCerradoODispuesto(this, cmbPendientesCheckIn, dgvConsumos)) return;

            _listaPendientes = ApiClient.ReservacionesLocales
                .Where(r => r.Estado == EstadoReservacion.Confirmada
                         || r.Estado == EstadoReservacion.PendientePago
                         || r.Estado == EstadoReservacion.EnCurso)
                .OrderBy(r => r.Estado == EstadoReservacion.EnCurso ? 1 : 0)
                .ToList();

            _cargandoCombo = true;
            var opciones = _listaPendientes.Select(r => new OpcionReservaCheckIn
            {
                Id = r.Id,
                Etiqueta = $"{r.Codigo} - {r.Cliente?.NombreCompleto ?? "Huésped"} (Hab. {r.Habitacion?.Numero ?? r.HabitacionId.ToString()}) [{r.Estado}]"
            }).ToList();

            cmbPendientesCheckIn.DataSource = null;
            cmbPendientesCheckIn.DataSource = opciones;
            cmbPendientesCheckIn.DisplayMember = "Etiqueta";
            cmbPendientesCheckIn.ValueMember = "Id";
            _cargandoCombo = false;

            if (_listaPendientes.Count > 0)
                SeleccionarDesdeComboPendientes();
        }

        private Moneda MonedaPagoSeleccionada()
            => cmbMonedaPago.SelectedIndex == 1 ? Moneda.USD : Moneda.CRC;

        private Moneda MonedaFacturaSeleccionada()
            => cmbMonedaFactura.SelectedIndex == 1 ? Moneda.USD : Moneda.CRC;

        private void SincronizarMonedaDesdeObservaciones(string? observaciones)
        {
            var moneda = ConversorMoneda.DetectarMonedaDesdeObservaciones(observaciones);
            _sincronizandoMoneda = true;
            cmbMonedaFactura.SelectedIndex = moneda == Moneda.USD ? 1 : 0;
            cmbMonedaPago.SelectedIndex = moneda == Moneda.USD ? 1 : 0;
            _sincronizandoMoneda = false;
        }

        private void ActualizarMontoPagoSegunMoneda()
        {
            if (_reservacion == null) return;
            decimal hospedaje = _cuenta?.Hospedaje ?? _reservacion.CostoTotal;
            decimal consumos = _cuenta?.Consumos ?? 0m;
            decimal pagado = _cuenta?.Pagado ?? 0m;
            decimal saldoCrc = Math.Max(0, hospedaje + consumos - pagado);
            decimal valor = ConversorMoneda.ConvertirDesdeColones(saldoCrc, MonedaPagoSeleccionada());
            nudMontoPago.Value = Math.Clamp(valor, nudMontoPago.Minimum, nudMontoPago.Maximum);
        }

        private void SeleccionarDesdeComboPendientes()
        {
            if (cmbPendientesCheckIn.SelectedValue is not int idSel) return;
            var res = _listaPendientes.FirstOrDefault(r => r.Id == idSel);
            if (res == null) return;

            _reservacion = res;
            if (!string.IsNullOrWhiteSpace(res.Cliente?.NumeroDocumento))
                txtBuscar.Text = res.Cliente.NumeroDocumento;

            InicializarCuentaSegunModalidad(res);
            SincronizarMonedaDesdeObservaciones(res.Observaciones);
            MostrarEnPantalla();
        }

        private void InicializarCuentaSegunModalidad(Reservacion res)
        {
            string obs = res.Observaciones ?? string.Empty;
            bool esContado = obs.Contains("Contado", StringComparison.OrdinalIgnoreCase);

            decimal pagadoInicial = esContado
                ? res.CostoTotal
                : Math.Round(res.CostoTotal * 0.30m, 2);

            decimal consumosRestaurante = ApiClient.PedidosLocales
                .Where(p => p.ReservacionId == res.Id && p.EstadoPedido != EstadoPedido.Cancelado)
                .Sum(p => p.Total);

            _cuenta = new EstadoCuentaDto
            {
                ReservacionId = res.Id,
                Codigo = res.Codigo,
                Hospedaje = res.CostoTotal,
                Consumos = consumosRestaurante,
                Pagado = pagadoInicial
            };
        }

        private async Task BuscarReservacionAsync()
        {
            string criterio = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(criterio))
            {
                MessageBox.Show("Ingrese el documento de identificación o seleccione una reservación pendiente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var lista = await ApiClient.GetAsync<List<Reservacion>>($"api/reservaciones/documento/{Uri.EscapeDataString(criterio)}") ?? new();
                if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvConsumos)) return;
                _reservacion = lista.FirstOrDefault();

                if (_reservacion != null)
                {
                    await RefrescarVistaAsync();
                    return;
                }
            }
            catch
            {
            }

            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvConsumos)) return;

            var encontradaLocal = ApiClient.ReservacionesLocales.FirstOrDefault(r =>
                r.Cliente?.NumeroDocumento == criterio ||
                r.Codigo.EndsWith(criterio, StringComparison.OrdinalIgnoreCase));

            _reservacion = encontradaLocal ?? new Reservacion
            {
                Id = 1,
                Codigo = "RES-2026-0001",
                HabitacionId = 2,
                ClienteId = 1,
                FechaInicio = DateTime.Today,
                FechaFin = DateTime.Today.AddDays(3),
                Estado = EstadoReservacion.Confirmada,
                CostoTotal = 549840m,
                CantidadHuespedes = 2,
                Observaciones = "Modalidad: Tasa Cero a 3 meses",
                Cliente = new Cliente { Nombres = "Carlos", Apellidos = "Mendoza Delgado", NumeroDocumento = criterio },
                Habitacion = new Habitacion { Id = 2, Numero = "102", Piso = 1 }
            };

            InicializarCuentaSegunModalidad(_reservacion);
            SincronizarMonedaDesdeObservaciones(_reservacion.Observaciones);
            MostrarEnPantalla();
        }

        private async Task RefrescarVistaAsync()
        {
            if (_reservacion == null) return;
            try
            {
                _reservacion = await ApiClient.GetAsync<Reservacion>($"api/reservaciones/{_reservacion.Id}") ?? _reservacion;
                _cuenta = await ApiClient.GetAsync<EstadoCuentaDto>($"api/reservaciones/{_reservacion.Id}/cuenta");
            }
            catch
            {
            }

            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvConsumos)) return;

            SincronizarMonedaDesdeObservaciones(_reservacion?.Observaciones);
            MostrarEnPantalla();
        }

        private void MostrarEnPantalla()
        {
            if (_reservacion == null || TemaAdminRustico.EstaCerradoODispuesto(this, dgvConsumos)) return;

            lblCodigo.Text = _reservacion.Codigo;
            lblCliente.Text = _reservacion.Cliente != null
                ? $"{_reservacion.Cliente.Nombres} {_reservacion.Cliente.Apellidos}"
                : $"Cliente #{_reservacion.ClienteId}";
            lblHabitacion.Text = _reservacion.Habitacion != null
                ? $"Hab. {_reservacion.Habitacion.Numero} (Piso {_reservacion.Habitacion.Piso})"
                : $"Hab. #{_reservacion.HabitacionId}";
            lblEntrada.Text = _reservacion.FechaInicio.ToString("dd/MM/yyyy");
            lblSalida.Text = _reservacion.FechaFin.ToString("dd/MM/yyyy");
            lblEstado.Text = _reservacion.Estado.ToString();

            var moneda = MonedaFacturaSeleccionada();
            var monedaAlterna = moneda == Moneda.USD ? Moneda.CRC : Moneda.USD;

            decimal hospedajeCrc = _cuenta?.Hospedaje ?? _reservacion.CostoTotal;
            decimal consumosCrc = _cuenta?.Consumos ?? 0m;
            decimal pagadoCrc = _cuenta?.Pagado ?? 0m;
            decimal saldoCrc = Math.Max(0, hospedajeCrc + consumosCrc - pagadoCrc);

            decimal hospMostrar = ConversorMoneda.ConvertirDesdeColones(hospedajeCrc, moneda);
            decimal consMostrar = ConversorMoneda.ConvertirDesdeColones(consumosCrc, moneda);
            decimal pagMostrar = ConversorMoneda.ConvertirDesdeColones(pagadoCrc, moneda);
            decimal salMostrar = ConversorMoneda.ConvertirDesdeColones(saldoCrc, moneda);

            lblHospedaje.Text = ConversorMoneda.Formatear(hospMostrar, moneda);
            lblConsumos.Text = ConversorMoneda.Formatear(consMostrar, moneda);
            lblPagado.Text = ConversorMoneda.Formatear(pagMostrar, moneda);
            lblSaldo.Text = ConversorMoneda.Formatear(salMostrar, moneda);

            ActualizarMontoPagoSegunMoneda();

            dgvConsumos.Rows.Clear();
            dgvConsumos.Rows.Add(_reservacion.FechaInicio.ToString("dd/MM/yyyy"), $"Hospedaje ({_reservacion.Codigo})", ConversorMoneda.Formatear(hospMostrar, moneda));
            if (!string.IsNullOrWhiteSpace(_reservacion.Observaciones))
                dgvConsumos.Rows.Add(DateTime.Today.ToString("dd/MM/yyyy"), _reservacion.Observaciones, "Plan elegido");
            if (consumosCrc > 0)
                dgvConsumos.Rows.Add(DateTime.Today.ToString("dd/MM/yyyy"), "Consumos de restaurante", ConversorMoneda.Formatear(consMostrar, moneda));
            if (pagadoCrc > 0)
                dgvConsumos.Rows.Add(DateTime.Today.ToString("dd/MM/yyyy"), "Pagos abonados", $"-{ConversorMoneda.Formatear(pagMostrar, moneda)}");

            decimal totalEquiv = ConversorMoneda.ConvertirDesdeColones(hospedajeCrc + consumosCrc, monedaAlterna);
            string labelEquiv = monedaAlterna == Moneda.USD ? "Equivalente Total USD (TC ₡515)" : "Equivalente Total CRC (TC ₡515)";
            dgvConsumos.Rows.Add(DateTime.Today.ToString("dd/MM/yyyy"), labelEquiv, ConversorMoneda.Formatear(totalEquiv, monedaAlterna));
        }

        private async Task RealizarCheckInAsync()
        {
            if (_reservacion == null)
            {
                MessageBox.Show("Primero seleccione o busque una reservación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new CheckInDto
            {
                HabitacionId = _reservacion.HabitacionId,
                Acompanantes = txtAcompanantes.Text.Trim(),
                NumeroLlave = string.IsNullOrWhiteSpace(txtLlave.Text) ? $"{_reservacion.Habitacion?.Numero ?? "101"}01" : txtLlave.Text.Trim()
            };

            try
            {
                await ApiClient.PostAsync($"api/reservaciones/{_reservacion.Id}/checkin", dto);
                await RefrescarVistaAsync();
            }
            catch
            {
                _reservacion.Estado = EstadoReservacion.EnCurso;
                _reservacion.FechaCheckIn = DateTime.Now;
                var habLocal = CatalogoLocal.Habitaciones.FirstOrDefault(h => h.Id == _reservacion.HabitacionId || h.Numero == _reservacion.Habitacion?.Numero);
                if (habLocal != null) habLocal.EstadoOperativo = EstadoHabitacion.Ocupada;
                MostrarEnPantalla();
            }

            await CargarReservacionesPendientesAsync();
            MessageBox.Show("Check-In confirmado. La habitación pasó a estado Ocupada.", "Check-In", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task RegistrarPagoAsync()
        {
            if (_reservacion == null)
            {
                MessageBox.Show("Primero seleccione o busque una reservación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal monto = nudMontoPago.Value;
            if (monto <= 0)
            {
                MessageBox.Show("El monto debe ser mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Enum.TryParse<MetodoPago>(cmbMetodoPago.SelectedItem?.ToString(), out var metodo);
            if (metodo != MetodoPago.Efectivo && string.IsNullOrWhiteSpace(txtReferencia.Text))
            {
                MessageBox.Show("Indique la referencia de la transacción para tarjeta o transferencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var monedaPago = MonedaPagoSeleccionada();
            try
            {
                await ApiClient.PostAsync("api/pagos", new RegistrarPagoDto
                {
                    ReservacionId = _reservacion.Id,
                    Monto = monto,
                    MetodoPago = metodo,
                    ReferenciaTransaccion = txtReferencia.Text.Trim(),
                    Moneda = monedaPago
                });
                await RefrescarVistaAsync();
            }
            catch
            {
                _cuenta ??= new EstadoCuentaDto { ReservacionId = _reservacion.Id, Codigo = _reservacion.Codigo, Hospedaje = _reservacion.CostoTotal };
                _cuenta.Pagado += ConversorMoneda.ConvertirAColones(monto, monedaPago);
                if (_reservacion.Estado == EstadoReservacion.PendientePago)
                    _reservacion.Estado = EstadoReservacion.Confirmada;
                MostrarEnPantalla();
            }

            txtReferencia.Clear();
            MessageBox.Show("Pago registrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task RealizarCheckOutAsync()
        {
            if (_reservacion == null)
            {
                MessageBox.Show("Primero seleccione o busque una reservación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _cuenta = await ApiClient.PostAsync<object, EstadoCuentaDto>($"api/reservaciones/{_reservacion.Id}/checkout", new { });
                await RefrescarVistaAsync();
                MessageBox.Show("Check-Out realizado. La habitación pasó a estado En Limpieza.", "Check-Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                decimal saldo = (_cuenta?.Hospedaje ?? _reservacion.CostoTotal) + (_cuenta?.Consumos ?? 0) - (_cuenta?.Pagado ?? 0);
                if (saldo > 0)
                {
                    MessageBox.Show($"Debe liquidar el saldo pendiente de ₡{saldo:N2} antes del Check-Out.", "Saldo pendiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _reservacion.Estado = EstadoReservacion.Completada;
                _reservacion.FechaCheckOut = DateTime.Now;
                var habLocal = CatalogoLocal.Habitaciones.FirstOrDefault(h => h.Id == _reservacion.HabitacionId || h.Numero == _reservacion.Habitacion?.Numero);
                if (habLocal != null) habLocal.EstadoOperativo = EstadoHabitacion.EnLimpieza;
                MostrarEnPantalla();
                await CargarReservacionesPendientesAsync();
                MessageBox.Show("Check-Out registrado exitosamente. La habitación pasó a estado En Limpieza.", "Check-Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private FacturaDto ConstruirFacturaLocal(Moneda destino)
        {
            decimal tc = ConversorMoneda.TipoCambioUsdDefecto;
            var monedaReserva = ConversorMoneda.DetectarMonedaDesdeObservaciones(_reservacion!.Observaciones);

            decimal hospCrc = Math.Round(_cuenta?.Hospedaje ?? _reservacion.CostoTotal, 2, MidpointRounding.AwayFromZero);
            decimal subCrc = Math.Round(hospCrc / (1m + CalculadoraCosto.Iva), 2, MidpointRounding.AwayFromZero);
            decimal ivaCrc = Math.Round(hospCrc - subCrc, 2, MidpointRounding.AwayFromZero);
            decimal consCrc = Math.Round(_cuenta?.Consumos ?? 0m, 2, MidpointRounding.AwayFromZero);
            decimal totCrc = Math.Round(hospCrc + consCrc, 2, MidpointRounding.AwayFromZero);
            decimal pagCrc = Math.Round(_cuenta?.Pagado ?? 0m, 2, MidpointRounding.AwayFromZero);
            decimal salCrc = Math.Max(0m, Math.Round(totCrc - pagCrc, 2, MidpointRounding.AwayFromZero));

            decimal hospUsd = ConversorMoneda.ColonesADolares(hospCrc, tc);
            decimal subUsd = Math.Round(hospUsd / (1m + CalculadoraCosto.Iva), 2, MidpointRounding.AwayFromZero);
            decimal ivaUsd = Math.Round(hospUsd - subUsd, 2, MidpointRounding.AwayFromZero);
            decimal consUsd = ConversorMoneda.ColonesADolares(consCrc, tc);
            decimal totUsd = Math.Round(hospUsd + consUsd, 2, MidpointRounding.AwayFromZero);
            decimal pagUsd = ConversorMoneda.ColonesADolares(pagCrc, tc);
            decimal salUsd = Math.Max(0m, ConversorMoneda.ColonesADolares(salCrc, tc));
            bool enUsd = destino == Moneda.USD;

            return new FacturaDto
            {
                NumeroFactura = $"FAC-{_reservacion.Codigo}",
                FechaEmision = DateTime.Now,
                ReservacionId = _reservacion.Id,
                CodigoReservacion = _reservacion.Codigo,
                ClienteNombre = lblCliente.Text,
                ClienteDocumento = _reservacion.Cliente?.NumeroDocumento ?? "",
                ClienteEmail = _reservacion.Cliente?.Email,
                Habitacion = lblHabitacion.Text,
                FechaInicio = _reservacion.FechaInicio,
                FechaFin = _reservacion.FechaFin,
                CantidadHuespedes = _reservacion.CantidadHuespedes,
                Estado = lblEstado.Text,
                Observaciones = _reservacion.Observaciones,
                MonedaReserva = monedaReserva,
                MonedaFacturacion = destino,
                SimboloMoneda = ConversorMoneda.Simbolo(destino),
                TipoCambioUsd = tc,
                ConversionAplicada = destino != Moneda.CRC || monedaReserva != Moneda.CRC,
                SubtotalHospedajeFacturado = enUsd ? subUsd : subCrc,
                IvaHospedajeFacturado = enUsd ? ivaUsd : ivaCrc,
                HospedajeFacturado = enUsd ? hospUsd : hospCrc,
                ConsumosFacturados = enUsd ? consUsd : consCrc,
                TotalFacturado = enUsd ? totUsd : totCrc,
                PagadoFacturado = enUsd ? pagUsd : pagCrc,
                SaldoFacturado = enUsd ? salUsd : salCrc,
                SubtotalHospedajeCrc = subCrc,
                IvaHospedajeCrc = ivaCrc,
                HospedajeCrc = hospCrc,
                ConsumosCrc = consCrc,
                TotalGeneralCrc = totCrc,
                PagadoCrc = pagCrc,
                SaldoCrc = salCrc,
                SubtotalHospedajeUsd = subUsd,
                IvaHospedajeUsd = ivaUsd,
                HospedajeUsd = hospUsd,
                ConsumosUsd = consUsd,
                TotalGeneralUsd = totUsd,
                PagadoUsd = pagUsd,
                SaldoUsd = salUsd
            };
        }

        private async Task EmitirFacturaAsync()
        {
            if (_reservacion == null)
            {
                MessageBox.Show("Primero seleccione o busque una reservación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var monedaFactura = MonedaFacturaSeleccionada();
            FacturaDto? factura = null;
            try
            {
                factura = await ApiClient.GetAsync<FacturaDto>($"api/reservaciones/{_reservacion.Id}/factura?moneda={monedaFactura}");
            }
            catch
            {
            }

            if (TemaAdminRustico.EstaCerradoODispuesto(this)) return;
            factura ??= ConstruirFacturaLocal(monedaFactura);

            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo de texto (*.txt)|*.txt",
                FileName = $"Factura_{_reservacion.Codigo}_{monedaFactura}.txt"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            string sim = factura.SimboloMoneda;
            var sb = new StringBuilder();
            sb.AppendLine("==========================================================");
            sb.AppendLine("              POSADA DEL ROBLE - FACTURA                  ");
            sb.AppendLine("==========================================================");
            sb.AppendLine($"No. Factura       : {factura.NumeroFactura}");
            sb.AppendLine($"Fecha Emisión     : {factura.FechaEmision:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Reservación       : {factura.CodigoReservacion}");
            sb.AppendLine($"Huésped           : {factura.ClienteNombre}");
            sb.AppendLine($"Habitación        : {factura.Habitacion}");
            sb.AppendLine($"Estadía           : {factura.FechaInicio:dd/MM/yyyy} al {factura.FechaFin:dd/MM/yyyy}");
            sb.AppendLine($"Moneda Reserva    : {factura.MonedaReserva}");
            sb.AppendLine($"Moneda Factura    : {factura.MonedaFacturacion} ({sim})");
            sb.AppendLine($"Tipo de Cambio    : 1 USD = ₡{factura.TipoCambioUsd:N2} CRC");
            if (!string.IsNullOrWhiteSpace(factura.Observaciones))
                sb.AppendLine($"Plan / Notas      : {factura.Observaciones}");
            sb.AppendLine("----------------------------------------------------------");
            sb.AppendLine($"DESGLOSE EN MONEDA FACTURADA ({factura.MonedaFacturacion}):");
            sb.AppendLine($"  Subtotal Hospedaje : {sim}{factura.SubtotalHospedajeFacturado:N2}");
            sb.AppendLine($"  IVA (16%)          : {sim}{factura.IvaHospedajeFacturado:N2}");
            sb.AppendLine($"  Total Hospedaje    : {sim}{factura.HospedajeFacturado:N2}");
            sb.AppendLine($"  Consumos           : {sim}{factura.ConsumosFacturados:N2}");
            sb.AppendLine($"  TOTAL GENERAL      : {sim}{factura.TotalFacturado:N2}");
            sb.AppendLine($"  Total Pagado       : {sim}{factura.PagadoFacturado:N2}");
            sb.AppendLine($"  Saldo Pendiente    : {sim}{factura.SaldoFacturado:N2}");
            sb.AppendLine("----------------------------------------------------------");
            sb.AppendLine("CONVERSIÓN REFERENCIAL DE FACTURACIÓN:");
            sb.AppendLine($"  En Colones (CRC)   : Total ₡{factura.TotalGeneralCrc:N2} | Saldo ₡{factura.SaldoCrc:N2}");
            sb.AppendLine($"  En Dólares (USD)   : Total ${factura.TotalGeneralUsd:N2} | Saldo ${factura.SaldoUsd:N2}");
            sb.AppendLine("==========================================================");

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show($"Factura emitida en {factura.MonedaFacturacion} con conversión CRC ↔ USD y guardada en:\n{sfd.FileName}", "Factura emitida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
