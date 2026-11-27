using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.WindowsForms.Servicios;

namespace Hotel.WindowsForms;

public partial class FrmReportesOcupacion : Form
{
    public FrmReportesOcupacion()
    {
        InitializeComponent();
        dtpDesde.Value = DateTime.Today.AddDays(-7);
        dtpHasta.Value = DateTime.Today.AddDays(7);
        if (cmbTipoReporte.Items.Count > 0) cmbTipoReporte.SelectedIndex = 0;

        Load += async (_, _) => await GenerarReporteAsync();
        btnGenerar.Click += async (_, _) => await GenerarReporteAsync();
        btnExportarExcel.Click += (_, _) => ExportarExcel();
        btnExportarPdf.Click += (_, _) => ExportarPdf();
    }

    private async Task GenerarReporteAsync()
    {
        if (dtpHasta.Value.Date < dtpDesde.Value.Date)
        {
            MessageBox.Show("La fecha final no puede ser menor a la fecha inicial.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string tipo = cmbTipoReporte.SelectedItem?.ToString() ?? "Ocupación por período";
        string desdeIso = dtpDesde.Value.Date.ToString("yyyy-MM-dd");
        string hastaIso = dtpHasta.Value.Date.ToString("yyyy-MM-dd");

        var serie = chartReporte.Series["Datos"];
        serie.Points.Clear();
        serie.IsValueShownAsLabel = true;

        switch (tipo)
        {
            case "Ocupación por período":
                await CargarReporteOcupacionAsync(desdeIso, hastaIso, serie);
                break;
            case "Reservaciones por fecha y estado":
                await CargarReporteReservacionesAsync(desdeIso, hastaIso, serie);
                break;
            case "Ingresos por hospedaje y pagos":
                await CargarReporteIngresosAsync(desdeIso, hastaIso, serie);
                break;
            case "Pedidos de restaurante":
                await CargarReportePedidosAsync(desdeIso, serie);
                break;
            case "Platillos más solicitados":
                await CargarReportePlatillosTopAsync(desdeIso, hastaIso, serie);
                break;
        }
    }

    private async Task CargarReporteOcupacionAsync(string desdeIso, string hastaIso, Series serie)
    {
        List<OcupacionDiaDto> datos;
        try
        {
            datos = await ApiClient.GetAsync<List<OcupacionDiaDto>>($"api/reportes/ocupacion?desde={desdeIso}&hasta={hastaIso}") ?? new();
        }
        catch
        {
            int totalHab = CatalogoLocal.Habitaciones.Count;
            int ocupadasBase = CatalogoLocal.Habitaciones.Count(h => h.EstadoOperativo == Dominio.Enums.EstadoHabitacion.Ocupada);
            int mantBase = CatalogoLocal.Habitaciones.Count(h => h.FueraDeServicio);

            datos = Enumerable.Range(0, 7).Select(i =>
            {
                int ocup = Math.Clamp(ocupadasBase + (i % 5), 1, totalHab - mantBase);
                return new OcupacionDiaDto
                {
                    Fecha = dtpDesde.Value.Date.AddDays(i),
                    Ocupadas = ocup,
                    Disponibles = Math.Max(0, totalHab - ocup - mantBase),
                    FueraDeServicio = mantBase
                };
            }).ToList();
        }

        ConfigurarColumnas("Fecha", "Ocupadas", "Disponibles", "Mantenimiento");
        serie.ChartType = SeriesChartType.Column;

        foreach (var d in datos)
        {
            dgvReporte.Rows.Add(d.Fecha.ToString("dd/MM/yyyy"), d.Ocupadas, d.Disponibles, d.FueraDeServicio);
            serie.Points.AddXY(d.Fecha.ToString("dd/MM"), d.Ocupadas);
        }
    }

    private async Task CargarReporteReservacionesAsync(string desdeIso, string hastaIso, Series serie)
    {
        List<Reservacion> datos;
        try
        {
            datos = await ApiClient.GetAsync<List<Reservacion>>($"api/reportes/reservaciones?desde={desdeIso}&hasta={hastaIso}") ?? new();
        }
        catch
        {
            datos = ApiClient.ReservacionesLocales.ToList();
        }

        ConfigurarColumnas("Código", "Entrada", "Salida", "Estado", "Total");
        serie.ChartType = SeriesChartType.Pie;

        foreach (var r in datos)
        {
            dgvReporte.Rows.Add(
                r.Codigo,
                r.FechaInicio.ToString("dd/MM/yyyy"),
                r.FechaFin.ToString("dd/MM/yyyy"),
                r.Estado.ToString(),
                r.CostoTotal.ToString("C2"));
        }

        foreach (var grupo in datos.GroupBy(x => x.Estado.ToString()))
        {
            serie.Points.AddXY(grupo.Key, grupo.Count());
        }
    }

    private async Task CargarReporteIngresosAsync(string desdeIso, string hastaIso, Series serie)
    {
        List<IngresoPeriodoDto> datos;
        try
        {
            datos = await ApiClient.GetAsync<List<IngresoPeriodoDto>>($"api/reportes/ingresos?desde={desdeIso}&hasta={hastaIso}") ?? new();
        }
        catch
        {
            datos = new List<IngresoPeriodoDto>
            {
                new IngresoPeriodoDto { Anio = DateTime.Today.Year, Mes = DateTime.Today.Month, MetodoPago = "Tarjeta", CantidadPagos = 8, Total = 2450000m },
                new IngresoPeriodoDto { Anio = DateTime.Today.Year, Mes = DateTime.Today.Month, MetodoPago = "Efectivo", CantidadPagos = 5, Total = 840000m },
                new IngresoPeriodoDto { Anio = DateTime.Today.Year, Mes = DateTime.Today.Month, MetodoPago = "Transferencia", CantidadPagos = 4, Total = 1320000m }
            };
        }

        ConfigurarColumnas("Período", "Método de Pago", "Cant. Pagos", "Total Recaudado");
        serie.ChartType = SeriesChartType.Column;

        foreach (var i in datos)
        {
            dgvReporte.Rows.Add($"{i.Mes:00}/{i.Anio}", i.MetodoPago, i.CantidadPagos, i.Total.ToString("C2"));
            serie.Points.AddXY(i.MetodoPago, i.Total);
        }
    }

    private async Task CargarReportePedidosAsync(string desdeIso, Series serie)
    {
        List<PedidoRestaurante> datos;
        try
        {
            datos = await ApiClient.GetAsync<List<PedidoRestaurante>>($"api/reportes/pedidos?fecha={desdeIso}") ?? new();
        }
        catch
        {
            datos = ApiClient.PedidosLocales.ToList();
        }

        ConfigurarColumnas("Pedido #", "Fecha", "Modalidad", "Estado", "Total");
        serie.ChartType = SeriesChartType.Column;

        foreach (var p in datos)
        {
            dgvReporte.Rows.Add(
                p.Id,
                p.FechaPedido.ToString("dd/MM/yyyy HH:mm"),
                p.EsRoomService ? "Room Service" : "Mesa",
                p.EstadoPedido.ToString(),
                p.Total.ToString("C2"));
        }

        foreach (var g in datos.GroupBy(x => x.EstadoPedido.ToString()))
        {
            serie.Points.AddXY(g.Key, g.Sum(x => x.Total));
        }
    }

    private async Task CargarReportePlatillosTopAsync(string desdeIso, string hastaIso, Series serie)
    {
        List<PlatilloSolicitadoDto> datos;
        try
        {
            datos = await ApiClient.GetAsync<List<PlatilloSolicitadoDto>>($"api/reportes/platillos-mas-solicitados?top=5&desde={desdeIso}&hasta={hastaIso}") ?? new();
        }
        catch
        {
            datos = new List<PlatilloSolicitadoDto>
            {
                new PlatilloSolicitadoDto { PlatilloId = 2, Nombre = "Asado del Roble", CantidadVendida = 24, Total = 636000m },
                new PlatilloSolicitadoDto { PlatilloId = 1, Nombre = "Desayuno del Viajero", CantidadVendida = 19, Total = 237500m },
                new PlatilloSolicitadoDto { PlatilloId = 3, Nombre = "Crema de Hongos", CantidadVendida = 14, Total = 165200m },
                new PlatilloSolicitadoDto { PlatilloId = 5, Nombre = "Tarta de Manzana", CantidadVendida = 11, Total = 97900m },
                new PlatilloSolicitadoDto { PlatilloId = 4, Nombre = "Sidra Especiada", CantidadVendida = 9, Total = 58500m }
            };
        }

        ConfigurarColumnas("Platillo", "Unidades Vendidas", "Ingreso Generado");
        serie.ChartType = SeriesChartType.Bar;

        foreach (var p in datos)
        {
            dgvReporte.Rows.Add(p.Nombre, p.CantidadVendida, p.Total.ToString("C2"));
            serie.Points.AddXY(p.Nombre, p.CantidadVendida);
        }
    }

    private void ConfigurarColumnas(params string[] encabezados)
    {
        dgvReporte.Rows.Clear();
        var columnas = new[] { colRep1, colRep2, colRep3, colRep4, colRep5 };
        for (int i = 0; i < columnas.Length; i++)
        {
            if (i < encabezados.Length)
            {
                columnas[i].HeaderText = encabezados[i];
                columnas[i].Visible = true;
            }
            else
            {
                columnas[i].Visible = false;
            }
        }
    }

    private void ExportarExcel()
    {
        if (dgvReporte.Rows.Count == 0)
        {
            MessageBox.Show("Genere un reporte antes de exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var sfd = new SaveFileDialog
        {
            Filter = "Archivo CSV de Excel (*.csv)|*.csv",
            FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        var colsVisibles = dgvReporte.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
        var sb = new StringBuilder();
        var headers = colsVisibles.Select(c => $"\"{c.HeaderText}\"");
        sb.AppendLine(string.Join(",", headers));

        foreach (DataGridViewRow fila in dgvReporte.Rows)
        {
            if (fila.IsNewRow) continue;
            var celdas = colsVisibles.Select(c => $"\"{fila.Cells[c.Index].Value}\"");
            sb.AppendLine(string.Join(",", celdas));
        }

        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
        MessageBox.Show($"Reporte exportado para Excel en:\n{sfd.FileName}", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ExportarPdf()
    {
        if (dgvReporte.Rows.Count == 0)
        {
            MessageBox.Show("Genere un reporte antes de exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var sfd = new SaveFileDialog
        {
            Filter = "Documento PDF (*.pdf)|*.pdf",
            FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        var colsVisibles = dgvReporte.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
        var lineas = new List<string>
        {
            "POSADA DEL ROBLE - SISTEMA DE GESTION HOTELERA",
            $"Reporte: {cmbTipoReporte.SelectedItem}",
            $"Periodo: {dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}",
            new string('-', 75)
        };

        var encabezados = colsVisibles.Select(c => c.HeaderText);
        lineas.Add(string.Join(" | ", encabezados));
        lineas.Add(new string('-', 75));

        foreach (DataGridViewRow fila in dgvReporte.Rows)
        {
            if (fila.IsNewRow) continue;
            var valores = colsVisibles.Select(c => fila.Cells[c.Index].Value?.ToString() ?? "");
            lineas.Add(string.Join(" | ", valores));
        }

        GuardarPdfSimple(sfd.FileName, lineas);
        MessageBox.Show($"Documento PDF generado en:\n{sfd.FileName}", "Posada del Roble", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void GuardarPdfSimple(string rutaArchivo, List<string> lineas)
    {
        var contenidoStream = new StringBuilder();
        contenidoStream.AppendLine("BT");
        contenidoStream.AppendLine("/F1 10 Tf");
        int y = 760;

        foreach (var linea in lineas.Take(48))
        {
            string limpia = QuitarAcentos(linea).Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
            contenidoStream.AppendLine($"1 0 0 1 40 {y} Tm ({limpia}) Tj");
            y -= 14;
        }
        contenidoStream.AppendLine("ET");

        byte[] streamBytes = Encoding.ASCII.GetBytes(contenidoStream.ToString());
        var pdf = new StringBuilder();
        pdf.AppendLine("%PDF-1.4");
        pdf.AppendLine("1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj");
        pdf.AppendLine("2 0 obj << /Type /Pages /Kids [3 0 R] /Count 1 >> endobj");
        pdf.AppendLine("3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >> endobj");
        pdf.AppendLine($"4 0 obj << /Length {streamBytes.Length} >> stream");
        pdf.Append(contenidoStream);
        pdf.AppendLine("endstream endobj");
        pdf.AppendLine("5 0 obj << /Type /Font /Subtype /Type1 /BaseFont /Courier >> endobj");
        pdf.AppendLine("trailer << /Root 1 0 R >>");
        pdf.AppendLine("%%EOF");

        File.WriteAllText(rutaArchivo, pdf.ToString(), Encoding.ASCII);
    }

    private static string QuitarAcentos(string texto)
    {
        return texto
            .Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u')
            .Replace('Á', 'A').Replace('É', 'E').Replace('Í', 'I').Replace('Ó', 'O').Replace('Ú', 'U')
            .Replace('ñ', 'n').Replace('Ñ', 'N');
    }
}
