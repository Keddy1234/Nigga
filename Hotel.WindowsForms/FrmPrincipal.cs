using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            Load += async (s, e) => await CargarDashboardAsync();

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
