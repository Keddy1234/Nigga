using System.Windows.Forms.DataVisualization.Charting;

namespace Hotel.WindowsForms
{
    partial class FrmReportesOcupacion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            gbFiltros = new GroupBox();
            label1 = new Label();
            dtpDesde = new DateTimePicker();
            label2 = new Label();
            dtpHasta = new DateTimePicker();
            label3 = new Label();
            cmbTipoReporte = new ComboBox();
            btnGenerar = new Button();
            dgvReporte = new DataGridView();
            colRep1 = new DataGridViewTextBoxColumn();
            colRep2 = new DataGridViewTextBoxColumn();
            colRep3 = new DataGridViewTextBoxColumn();
            colRep4 = new DataGridViewTextBoxColumn();
            colRep5 = new DataGridViewTextBoxColumn();
            chartReporte = new Chart();
            btnExportarExcel = new Button();
            btnExportarPdf = new Button();
            gbFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartReporte).BeginInit();
            SuspendLayout();
            // 
            // gbFiltros
            // 
            gbFiltros.BackColor = Color.FromArgb(224, 204, 170);
            gbFiltros.Controls.Add(btnGenerar);
            gbFiltros.Controls.Add(cmbTipoReporte);
            gbFiltros.Controls.Add(label3);
            gbFiltros.Controls.Add(dtpHasta);
            gbFiltros.Controls.Add(label2);
            gbFiltros.Controls.Add(dtpDesde);
            gbFiltros.Controls.Add(label1);
            gbFiltros.Location = new Point(20, 16);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Size = new Size(880, 68);
            gbFiltros.TabIndex = 0;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 30);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 0;
            label1.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(70, 26);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(115, 21);
            dtpDesde.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(204, 30);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 2;
            label2.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(252, 26);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(115, 21);
            dtpHasta.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(388, 30);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 4;
            label3.Text = "Reporte:";
            // 
            // cmbTipoReporte
            // 
            cmbTipoReporte.BackColor = Color.FromArgb(250, 242, 226);
            cmbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoReporte.Items.AddRange(new object[]
            {
                "Ocupación por período",
                "Reservaciones por fecha y estado",
                "Ingresos por hospedaje y pagos",
                "Pedidos de restaurante",
                "Platillos más solicitados"
            });
            cmbTipoReporte.Location = new Point(448, 26);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(270, 23);
            cmbTipoReporte.TabIndex = 5;
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = Color.FromArgb(92, 58, 33);
            btnGenerar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.ForeColor = Color.FromArgb(245, 232, 208);
            btnGenerar.Location = new Point(740, 23);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(118, 29);
            btnGenerar.TabIndex = 6;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = false;
            // 
            // dgvReporte
            // 
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvReporte.ColumnHeadersHeight = 28;
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvReporte.Columns.AddRange(new DataGridViewColumn[] { colRep1, colRep2, colRep3, colRep4, colRep5 });
            dgvReporte.GridColor = Color.FromArgb(140, 105, 70);
            dgvReporte.Location = new Point(20, 98);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.ReadOnly = true;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.Size = new Size(490, 370);
            dgvReporte.TabIndex = 1;
            // 
            // colRep1
            // 
            colRep1.HeaderText = "Fecha";
            colRep1.Name = "colRep1";
            colRep1.ReadOnly = true;
            // 
            // colRep2
            // 
            colRep2.HeaderText = "Ocupadas";
            colRep2.Name = "colRep2";
            colRep2.ReadOnly = true;
            // 
            // colRep3
            // 
            colRep3.HeaderText = "Disponibles";
            colRep3.Name = "colRep3";
            colRep3.ReadOnly = true;
            // 
            // colRep4
            // 
            colRep4.HeaderText = "Mantenimiento";
            colRep4.Name = "colRep4";
            colRep4.ReadOnly = true;
            // 
            // colRep5
            // 
            colRep5.HeaderText = "Detalle";
            colRep5.Name = "colRep5";
            colRep5.ReadOnly = true;
            colRep5.Visible = false;
            // 
            // chartReporte
            // 
            chartReporte.BackColor = Color.FromArgb(245, 232, 208);
            chartReporte.BorderlineColor = Color.FromArgb(92, 58, 33);
            chartReporte.BorderlineDashStyle = ChartDashStyle.Solid;
            chartArea1.BackColor = Color.FromArgb(250, 242, 226);
            chartArea1.Name = "ChartArea1";
            chartReporte.ChartAreas.Add(chartArea1);
            chartReporte.Location = new Point(526, 98);
            chartReporte.Name = "chartReporte";
            series1.ChartArea = "ChartArea1";
            series1.Color = Color.FromArgb(92, 58, 33);
            series1.Name = "Datos";
            chartReporte.Series.Add(series1);
            chartReporte.Size = new Size(374, 370);
            chartReporte.TabIndex = 2;
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.BackColor = Color.FromArgb(58, 102, 52);
            btnExportarExcel.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnExportarExcel.FlatStyle = FlatStyle.Flat;
            btnExportarExcel.ForeColor = Color.FromArgb(245, 232, 208);
            btnExportarExcel.Location = new Point(628, 482);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(130, 32);
            btnExportarExcel.TabIndex = 3;
            btnExportarExcel.Text = "Exportar Excel";
            btnExportarExcel.UseVisualStyleBackColor = false;
            // 
            // btnExportarPdf
            // 
            btnExportarPdf.BackColor = Color.FromArgb(138, 44, 36);
            btnExportarPdf.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnExportarPdf.FlatStyle = FlatStyle.Flat;
            btnExportarPdf.ForeColor = Color.FromArgb(245, 232, 208);
            btnExportarPdf.Location = new Point(770, 482);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(130, 32);
            btnExportarPdf.TabIndex = 4;
            btnExportarPdf.Text = "Exportar PDF";
            btnExportarPdf.UseVisualStyleBackColor = false;
            // 
            // FrmReportesOcupacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(920, 530);
            Controls.Add(btnExportarPdf);
            Controls.Add(btnExportarExcel);
            Controls.Add(chartReporte);
            Controls.Add(dgvReporte);
            Controls.Add(gbFiltros);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmReportesOcupacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes";
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartReporte).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbFiltros;
        private Label label1;
        private DateTimePicker dtpDesde;
        private Label label2;
        private DateTimePicker dtpHasta;
        private Label label3;
        private ComboBox cmbTipoReporte;
        private Button btnGenerar;
        private DataGridView dgvReporte;
        private DataGridViewTextBoxColumn colRep1;
        private DataGridViewTextBoxColumn colRep2;
        private DataGridViewTextBoxColumn colRep3;
        private DataGridViewTextBoxColumn colRep4;
        private DataGridViewTextBoxColumn colRep5;
        private Chart chartReporte;
        private Button btnExportarExcel;
        private Button btnExportarPdf;
    }
}
