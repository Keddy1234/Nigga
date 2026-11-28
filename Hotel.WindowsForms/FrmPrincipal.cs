using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms
{
    public partial class FrmPrincipal : Form
    {
        private PanelPaisajeRustico? _bannerHero;
        private PanelPaisajeRustico? _vignetteSidebar;
        private System.Windows.Forms.Timer? _relojTimer;

        public FrmPrincipal()
        {
            InitializeComponent();
            InicializarEsteticaAdmin();
            ConfigurarEventos();
        }

        private void InicializarEsteticaAdmin()
        {
            _bannerHero = new PanelPaisajeRustico
            {
                Dock = DockStyle.Fill,
                EsHeroPrincipal = true,
                SoloVignette = false,
                ImagenFondo = TemaAdminRustico.ObtenerImagen(TipoPaisajeAdmin.PrincipalHero),
                Eyebrow = "ADMINISTRACIÓN HOTELERA",
                Titulo = "Posada del Roble",
                Subtitulo = "Gestión integral de hospedaje, recepción, restaurante y reportes"
            };
            pnlHeroContenedor.Controls.Add(_bannerHero);
            btnActualizarDashboard.BringToFront();

            _vignetteSidebar = new PanelPaisajeRustico
            {
                Dock = DockStyle.Fill,
                EsHeroPrincipal = false,
                SoloVignette = true,
                ImagenFondo = TemaAdminRustico.ObtenerImagen(TipoPaisajeAdmin.SidebarBosque),
                Titulo = "Posada del Roble",
                Subtitulo = "Refugio Rústico de Montaña"
            };
            pnlPaisajeSidebar.Controls.Add(_vignetteSidebar);

            ActualizarRelojHero();
            _relojTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _relojTimer.Tick += (_, _) => ActualizarRelojHero();
            _relojTimer.Start();

            FormClosed += (_, _) =>
            {
                if (_relojTimer != null)
                {
                    _relojTimer.Stop();
                    _relojTimer.Dispose();
                    _relojTimer = null;
                }
            };

            TemaAdminRustico.EstilizarControlesRecursivo(this);

            TemaAdminRustico.EstilizarTarjetaKpi(pnlKpiOcupacion, Color.FromArgb(196, 154, 69));
            TemaAdminRustico.EstilizarTarjetaKpi(pnlKpiLlegadas, Color.FromArgb(141, 194, 120));
            TemaAdminRustico.EstilizarTarjetaKpi(pnlKpiSalidas, Color.FromArgb(230, 169, 87));
            TemaAdminRustico.EstilizarTarjetaKpi(pnlKpiIngresos, Color.FromArgb(224, 130, 117));

            pnlAlertaLimpieza.Paint += (_, e) =>
            {
                if (pnlAlertaLimpieza.Width <= 4 || pnlAlertaLimpieza.Height <= 2) return;
                using var brochaIzq = new SolidBrush(Color.FromArgb(140, 82, 32));
                e.Graphics.FillRectangle(brochaIzq, 0, 0, 4, pnlAlertaLimpieza.Height);
                using var plumaBorde = new Pen(Color.FromArgb(175, 136, 70), 1f);
                e.Graphics.DrawRectangle(plumaBorde, 0, 0, pnlAlertaLimpieza.Width - 1, pnlAlertaLimpieza.Height - 1);
            };

            dgvLlegadasHoy.CellFormatting += DgvLlegadasHoy_CellFormatting;
        }

        private void ActualizarRelojHero()
        {
            if (_bannerHero == null || _bannerHero.IsDisposed) return;

            var ahora = DateTime.Now;
            string fechaRaw = ahora.ToString("dddd, d 'de' MMMM 'de' yyyy");
            string fechaCapitalizada = string.IsNullOrEmpty(fechaRaw)
                ? string.Empty
                : char.ToUpper(fechaRaw[0]) + fechaRaw[1..];
            string hora = ahora.ToString("hh:mm:ss tt");

            if (_bannerHero.TextoDerechaSuperior != fechaCapitalizada ||
                _bannerHero.TextoDerechaInferior != hora)
            {
                _bannerHero.TextoDerechaSuperior = fechaCapitalizada;
                _bannerHero.TextoDerechaInferior = hora;
            }
        }

        private void DgvLlegadasHoy_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colEstado.Index || e.Value == null)
                return;

            string estado = e.Value.ToString() ?? string.Empty;
            Color colorTexto = estado switch
            {
                nameof(EstadoReservacion.Confirmada) or nameof(EstadoReservacion.EnCurso)
                    => Color.FromArgb(42, 102, 46),
                nameof(EstadoReservacion.PendientePago)
                    => Color.FromArgb(156, 98, 18),
                nameof(EstadoReservacion.Cancelada)
                    => Color.FromArgb(145, 38, 32),
                _ => Color.FromArgb(88, 68, 52)
            };

            if (e.CellStyle != null)
            {
                e.CellStyle.ForeColor = colorTexto;
                e.CellStyle.Font = new Font("Georgia", 9F, FontStyle.Bold);
            }
        }

        private void ConfigurarEventos()
        {
            Load += async (s, e) => await CargarDashboardAsync();
            btnActualizarDashboard.Click += async (s, e) => await CargarDashboardAsync();

            btnVerMapaRapido.Click += async (s, e) =>
            {
                using var frm = new FrmHabitaciones();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnHabitaciones.Click += async (s, e) =>
            {
                using var frm = new FrmHabitaciones();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnTiposHabitacion.Click += async (s, e) =>
            {
                using var frm = new FrmTipoHabitacion();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnNuevaReservacion.Click += async (s, e) =>
            {
                using var frm = new FrmNuevaReservacion();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnCheckInOut.Click += async (s, e) =>
            {
                using var frm = new FrmCheckInOut();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnPlatillos.Click += async (s, e) =>
            {
                using var frm = new FrmPlatillos();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnRestaurante.Click += async (s, e) =>
            {
                using var frm = new FrmRestauranteHuespedes();
                frm.ShowDialog(this);
                await CargarDashboardAsync();
            };

            btnReportes.Click += (s, e) =>
            {
                using var frm = new FrmReportesOcupacion();
                frm.ShowDialog(this);
            };
        }

        private async Task CargarDashboardAsync()
        {
            try
            {
                var habitaciones = await ApiClient.GetAsync<List<Habitacion>>("api/habitaciones") ?? new();
                var reservaciones = await ApiClient.GetAsync<List<Reservacion>>("api/reservaciones") ?? new();
                var inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var finMes = inicioMes.AddMonths(1);
                var ingresos = await ApiClient.GetAsync<List<IngresoPeriodoDto>>(
                    $"api/reportes/ingresos?desde={inicioMes:yyyy-MM-dd}&hasta={finMes:yyyy-MM-dd}") ?? new();

                if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvLlegadasHoy)) return;

                int totalHab = habitaciones.Count;
                int ocupadas = habitaciones.Count(h => h.EstadoOperativo == EstadoHabitacion.Ocupada);
                int porLimpiar = habitaciones.Count(h => h.EstadoOperativo == EstadoHabitacion.EnLimpieza);
                decimal pct = totalHab > 0 ? Math.Round((decimal)ocupadas * 100 / totalHab, 1) : 0;

                var hoy = DateTime.Today;
                int llegadasHoy = reservaciones.Count(r =>
                    r.FechaInicio.Date == hoy &&
                    (r.Estado == EstadoReservacion.Confirmada || r.Estado == EstadoReservacion.PendientePago));

                int salidasHoy = reservaciones.Count(r =>
                    r.FechaFin.Date == hoy && r.Estado == EstadoReservacion.EnCurso);

                decimal totalIngresos = ingresos.Sum(i => i.Total);

                lblOcupacionValor.Text = $"{pct:0.#} %";
                lblLlegadasValor.Text = llegadasHoy.ToString();
                lblSalidasValor.Text = salidasHoy.ToString();
                lblIngresosValor.Text = $"₡{totalIngresos:N2}";
                lblAlertaLimpieza.Text = $"Habitaciones por limpiar: {porLimpiar}";

                dgvLlegadasHoy.Rows.Clear();
                foreach (var r in reservaciones.Take(20))
                {
                    string cliente = r.Cliente != null ? $"{r.Cliente.Nombres} {r.Cliente.Apellidos}" : r.ClienteId.ToString();
                    string hab = r.Habitacion != null ? r.Habitacion.Numero : r.HabitacionId.ToString();
                    dgvLlegadasHoy.Rows.Add(r.Codigo, cliente, hab, r.FechaInicio.ToString("dd/MM/yyyy"), r.Estado.ToString());
                }
            }
            catch
            {
                CargarDatosLocalesMuestra();
            }
        }

        private void CargarDatosLocalesMuestra()
        {
            if (TemaAdminRustico.EstaCerradoODispuesto(this, dgvLlegadasHoy)) return;

            var reservas = ApiClient.ReservacionesLocales;
            var habitaciones = CatalogoLocal.Habitaciones;
            int totalHab = Math.Max(1, habitaciones.Count);
            int ocupadas = habitaciones.Count(h => h.EstadoOperativo == EstadoHabitacion.Ocupada);
            var porLimpiar = habitaciones.Where(h => h.EstadoOperativo == EstadoHabitacion.EnLimpieza).ToList();
            decimal pct = Math.Round((decimal)ocupadas * 100m / totalHab, 1);

            lblOcupacionValor.Text = $"{pct:0.#} %";
            lblLlegadasValor.Text = reservas.Count(r => r.Estado == EstadoReservacion.Confirmada).ToString();
            lblSalidasValor.Text = reservas.Count(r => r.Estado == EstadoReservacion.EnCurso).ToString();
            lblIngresosValor.Text = $"₡{reservas.Sum(r => r.CostoTotal):N2}";
            string detalleLimpieza = porLimpiar.Count > 0
                ? $" ({string.Join(", ", porLimpiar.Select(h => $"Hab. {h.Numero}"))})"
                : string.Empty;
            lblAlertaLimpieza.Text = $"Habitaciones por limpiar: {porLimpiar.Count}{detalleLimpieza}";

            dgvLlegadasHoy.Rows.Clear();
            foreach (var r in reservas)
            {
                string cli = r.Cliente?.NombreCompleto ?? $"Cliente #{r.ClienteId}";
                string hab = r.Habitacion?.Numero ?? r.HabitacionId.ToString();
                dgvLlegadasHoy.Rows.Add(r.Codigo, cli, hab, r.FechaInicio.ToString("dd/MM/yyyy"), r.Estado.ToString());
            }
        }
    }
}
