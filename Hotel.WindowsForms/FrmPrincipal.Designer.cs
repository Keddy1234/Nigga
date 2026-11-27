namespace Hotel.WindowsForms
{
    partial class FrmPrincipal
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
            pnlMenu = new Panel();
            lblLogo = new Label();
            btnHabitaciones = new Button();
            btnTiposHabitacion = new Button();
            btnNuevaReservacion = new Button();
            btnCheckInOut = new Button();
            btnPlatillos = new Button();
            btnRestaurante = new Button();
            btnReportes = new Button();
            pnlContenido = new Panel();
            pnlKpiOcupacion = new Panel();
            lblOcupacionValor = new Label();
            label1 = new Label();
            pnlKpiLlegadas = new Panel();
            lblLlegadasValor = new Label();
            label2 = new Label();
            pnlKpiSalidas = new Panel();
            lblSalidasValor = new Label();
            label3 = new Label();
            pnlKpiIngresos = new Panel();
            lblIngresosValor = new Label();
            label4 = new Label();
            pnlAlertaLimpieza = new Panel();
            lblAlertaLimpieza = new Label();
            gbLlegadasHoy = new GroupBox();
            dgvLlegadasHoy = new DataGridView();
            colCodigo = new DataGridViewTextBoxColumn();
            colCliente = new DataGridViewTextBoxColumn();
            colHabitacion = new DataGridViewTextBoxColumn();
            colEntrada = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlMenu.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlKpiOcupacion.SuspendLayout();
            pnlKpiLlegadas.SuspendLayout();
            pnlKpiSalidas.SuspendLayout();
            pnlKpiIngresos.SuspendLayout();
            pnlAlertaLimpieza.SuspendLayout();
            gbLlegadasHoy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLlegadasHoy).BeginInit();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(46, 31, 20);
            pnlMenu.Controls.Add(btnReportes);
            pnlMenu.Controls.Add(btnRestaurante);
            pnlMenu.Controls.Add(btnPlatillos);
            pnlMenu.Controls.Add(btnCheckInOut);
            pnlMenu.Controls.Add(btnNuevaReservacion);
            pnlMenu.Controls.Add(btnTiposHabitacion);
            pnlMenu.Controls.Add(btnHabitaciones);
            pnlMenu.Controls.Add(lblLogo);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(210, 600);
            pnlMenu.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.BackColor = Color.FromArgb(35, 22, 14);
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Georgia", 13F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(232, 198, 128);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(210, 64);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "POSADA DEL ROBLE";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnHabitaciones
            // 
            btnHabitaciones.BackColor = Color.FromArgb(92, 58, 33);
            btnHabitaciones.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnHabitaciones.FlatStyle = FlatStyle.Flat;
            btnHabitaciones.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnHabitaciones.ForeColor = Color.FromArgb(245, 232, 208);
            btnHabitaciones.Location = new Point(14, 84);
            btnHabitaciones.Name = "btnHabitaciones";
            btnHabitaciones.Size = new Size(182, 40);
            btnHabitaciones.TabIndex = 1;
            btnHabitaciones.Text = "Habitaciones";
            btnHabitaciones.UseVisualStyleBackColor = false;
            // 
            // btnTiposHabitacion
            // 
            btnTiposHabitacion.BackColor = Color.FromArgb(92, 58, 33);
            btnTiposHabitacion.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnTiposHabitacion.FlatStyle = FlatStyle.Flat;
            btnTiposHabitacion.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnTiposHabitacion.ForeColor = Color.FromArgb(245, 232, 208);
            btnTiposHabitacion.Location = new Point(14, 134);
            btnTiposHabitacion.Name = "btnTiposHabitacion";
            btnTiposHabitacion.Size = new Size(182, 40);
            btnTiposHabitacion.TabIndex = 2;
            btnTiposHabitacion.Text = "Tipos de Habitación";
            btnTiposHabitacion.UseVisualStyleBackColor = false;
            // 
            // btnNuevaReservacion
            // 
            btnNuevaReservacion.BackColor = Color.FromArgb(92, 58, 33);
            btnNuevaReservacion.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnNuevaReservacion.FlatStyle = FlatStyle.Flat;
            btnNuevaReservacion.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnNuevaReservacion.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevaReservacion.Location = new Point(14, 184);
            btnNuevaReservacion.Name = "btnNuevaReservacion";
            btnNuevaReservacion.Size = new Size(182, 40);
            btnNuevaReservacion.TabIndex = 3;
            btnNuevaReservacion.Text = "Nueva Reservación";
            btnNuevaReservacion.UseVisualStyleBackColor = false;
            // 
            // btnCheckInOut
            // 
            btnCheckInOut.BackColor = Color.FromArgb(92, 58, 33);
            btnCheckInOut.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnCheckInOut.FlatStyle = FlatStyle.Flat;
            btnCheckInOut.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnCheckInOut.ForeColor = Color.FromArgb(245, 232, 208);
            btnCheckInOut.Location = new Point(14, 234);
            btnCheckInOut.Name = "btnCheckInOut";
            btnCheckInOut.Size = new Size(182, 40);
            btnCheckInOut.TabIndex = 4;
            btnCheckInOut.Text = "Check-In / Check-Out";
            btnCheckInOut.UseVisualStyleBackColor = false;
            // 
            // btnPlatillos
            // 
            btnPlatillos.BackColor = Color.FromArgb(92, 58, 33);
            btnPlatillos.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnPlatillos.FlatStyle = FlatStyle.Flat;
            btnPlatillos.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnPlatillos.ForeColor = Color.FromArgb(245, 232, 208);
            btnPlatillos.Location = new Point(14, 284);
            btnPlatillos.Name = "btnPlatillos";
            btnPlatillos.Size = new Size(182, 40);
            btnPlatillos.TabIndex = 5;
            btnPlatillos.Text = "Platillos";
            btnPlatillos.UseVisualStyleBackColor = false;
            // 
            // btnRestaurante
            // 
            btnRestaurante.BackColor = Color.FromArgb(92, 58, 33);
            btnRestaurante.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnRestaurante.FlatStyle = FlatStyle.Flat;
            btnRestaurante.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnRestaurante.ForeColor = Color.FromArgb(245, 232, 208);
            btnRestaurante.Location = new Point(14, 334);
            btnRestaurante.Name = "btnRestaurante";
            btnRestaurante.Size = new Size(182, 40);
            btnRestaurante.TabIndex = 6;
            btnRestaurante.Text = "Restaurante";
            btnRestaurante.UseVisualStyleBackColor = false;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(92, 58, 33);
            btnReportes.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnReportes.ForeColor = Color.FromArgb(245, 232, 208);
            btnReportes.Location = new Point(14, 384);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(182, 40);
            btnReportes.TabIndex = 7;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(236, 220, 192);
            pnlContenido.Controls.Add(gbLlegadasHoy);
            pnlContenido.Controls.Add(pnlAlertaLimpieza);
            pnlContenido.Controls.Add(pnlKpiIngresos);
            pnlContenido.Controls.Add(pnlKpiSalidas);
            pnlContenido.Controls.Add(pnlKpiLlegadas);
            pnlContenido.Controls.Add(pnlKpiOcupacion);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(210, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(810, 600);
            pnlContenido.TabIndex = 1;
            // 
            // pnlKpiOcupacion
            // 
            pnlKpiOcupacion.BackColor = Color.FromArgb(74, 48, 32);
            pnlKpiOcupacion.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiOcupacion.Controls.Add(lblOcupacionValor);
            pnlKpiOcupacion.Controls.Add(label1);
            pnlKpiOcupacion.Location = new Point(24, 22);
            pnlKpiOcupacion.Name = "pnlKpiOcupacion";
            pnlKpiOcupacion.Size = new Size(180, 92);
            pnlKpiOcupacion.TabIndex = 0;
            // 
            // lblOcupacionValor
            // 
            lblOcupacionValor.AutoSize = true;
            lblOcupacionValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblOcupacionValor.ForeColor = Color.FromArgb(232, 198, 128);
            lblOcupacionValor.Location = new Point(14, 42);
            lblOcupacionValor.Name = "lblOcupacionValor";
            lblOcupacionValor.Size = new Size(64, 29);
            lblOcupacionValor.TabIndex = 1;
            lblOcupacionValor.Text = "0 %";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(245, 232, 208);
            label1.Location = new Point(14, 14);
            label1.Name = "label1";
            label1.Size = new Size(126, 16);
            label1.TabIndex = 0;
            label1.Text = "Ocupación del día";
            // 
            // pnlKpiLlegadas
            // 
            pnlKpiLlegadas.BackColor = Color.FromArgb(62, 92, 50);
            pnlKpiLlegadas.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiLlegadas.Controls.Add(lblLlegadasValor);
            pnlKpiLlegadas.Controls.Add(label2);
            pnlKpiLlegadas.Location = new Point(218, 22);
            pnlKpiLlegadas.Name = "pnlKpiLlegadas";
            pnlKpiLlegadas.Size = new Size(180, 92);
            pnlKpiLlegadas.TabIndex = 1;
            // 
            // lblLlegadasValor
            // 
            lblLlegadasValor.AutoSize = true;
            lblLlegadasValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblLlegadasValor.ForeColor = Color.FromArgb(245, 232, 208);
            lblLlegadasValor.Location = new Point(14, 42);
            lblLlegadasValor.Name = "lblLlegadasValor";
            lblLlegadasValor.Size = new Size(29, 29);
            lblLlegadasValor.TabIndex = 1;
            lblLlegadasValor.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(245, 232, 208);
            label2.Location = new Point(14, 14);
            label2.Name = "label2";
            label2.Size = new Size(141, 16);
            label2.TabIndex = 0;
            label2.Text = "Llegadas pendientes";
            // 
            // pnlKpiSalidas
            // 
            pnlKpiSalidas.BackColor = Color.FromArgb(145, 92, 36);
            pnlKpiSalidas.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiSalidas.Controls.Add(lblSalidasValor);
            pnlKpiSalidas.Controls.Add(label3);
            pnlKpiSalidas.Location = new Point(412, 22);
            pnlKpiSalidas.Name = "pnlKpiSalidas";
            pnlKpiSalidas.Size = new Size(180, 92);
            pnlKpiSalidas.TabIndex = 2;
            // 
            // lblSalidasValor
            // 
            lblSalidasValor.AutoSize = true;
            lblSalidasValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblSalidasValor.ForeColor = Color.FromArgb(245, 232, 208);
            lblSalidasValor.Location = new Point(14, 42);
            lblSalidasValor.Name = "lblSalidasValor";
            lblSalidasValor.Size = new Size(29, 29);
            lblSalidasValor.TabIndex = 1;
            lblSalidasValor.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(245, 232, 208);
            label3.Location = new Point(14, 14);
            label3.Name = "label3";
            label3.Size = new Size(144, 16);
            label3.TabIndex = 0;
            label3.Text = "Salidas programadas";
            // 
            // pnlKpiIngresos
            // 
            pnlKpiIngresos.BackColor = Color.FromArgb(118, 44, 36);
            pnlKpiIngresos.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiIngresos.Controls.Add(lblIngresosValor);
            pnlKpiIngresos.Controls.Add(label4);
            pnlKpiIngresos.Location = new Point(606, 22);
            pnlKpiIngresos.Name = "pnlKpiIngresos";
            pnlKpiIngresos.Size = new Size(180, 92);
            pnlKpiIngresos.TabIndex = 3;
            // 
            // lblIngresosValor
            // 
            lblIngresosValor.AutoSize = true;
            lblIngresosValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblIngresosValor.ForeColor = Color.FromArgb(232, 198, 128);
            lblIngresosValor.Location = new Point(14, 42);
            lblIngresosValor.Name = "lblIngresosValor";
            lblIngresosValor.Size = new Size(65, 29);
            lblIngresosValor.TabIndex = 1;
            lblIngresosValor.Text = "0.00";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(245, 232, 208);
            label4.Location = new Point(14, 14);
            label4.Name = "label4";
            label4.Size = new Size(64, 16);
            label4.TabIndex = 0;
            label4.Text = "Ingresos";
            // 
            // pnlAlertaLimpieza
            // 
            pnlAlertaLimpieza.BackColor = Color.FromArgb(196, 154, 69);
            pnlAlertaLimpieza.BorderStyle = BorderStyle.FixedSingle;
            pnlAlertaLimpieza.Controls.Add(lblAlertaLimpieza);
            pnlAlertaLimpieza.Location = new Point(24, 130);
            pnlAlertaLimpieza.Name = "pnlAlertaLimpieza";
            pnlAlertaLimpieza.Size = new Size(762, 42);
            pnlAlertaLimpieza.TabIndex = 4;
            // 
            // lblAlertaLimpieza
            // 
            lblAlertaLimpieza.AutoSize = true;
            lblAlertaLimpieza.Font = new Font("Georgia", 10F, FontStyle.Bold);
            lblAlertaLimpieza.ForeColor = Color.FromArgb(46, 31, 20);
            lblAlertaLimpieza.Location = new Point(14, 11);
            lblAlertaLimpieza.Name = "lblAlertaLimpieza";
            lblAlertaLimpieza.Size = new Size(203, 17);
            lblAlertaLimpieza.TabIndex = 0;
            lblAlertaLimpieza.Text = "Habitaciones por limpiar: 0";
            // 
            // gbLlegadasHoy
            // 
            gbLlegadasHoy.BackColor = Color.FromArgb(224, 204, 170);
            gbLlegadasHoy.Controls.Add(dgvLlegadasHoy);
            gbLlegadasHoy.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            gbLlegadasHoy.ForeColor = Color.FromArgb(46, 31, 20);
            gbLlegadasHoy.Location = new Point(24, 188);
            gbLlegadasHoy.Name = "gbLlegadasHoy";
            gbLlegadasHoy.Size = new Size(762, 388);
            gbLlegadasHoy.TabIndex = 5;
            gbLlegadasHoy.TabStop = false;
            gbLlegadasHoy.Text = "Reservaciones del día";
            // 
            // dgvLlegadasHoy
            // 
            dgvLlegadasHoy.AllowUserToAddRows = false;
            dgvLlegadasHoy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLlegadasHoy.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvLlegadasHoy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvLlegadasHoy.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colCliente, colHabitacion, colEntrada, colEstado });
            dgvLlegadasHoy.GridColor = Color.FromArgb(140, 105, 70);
            dgvLlegadasHoy.Location = new Point(16, 28);
            dgvLlegadasHoy.Name = "dgvLlegadasHoy";
            dgvLlegadasHoy.ReadOnly = true;
            dgvLlegadasHoy.RowHeadersVisible = false;
            dgvLlegadasHoy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLlegadasHoy.Size = new Size(730, 344);
            dgvLlegadasHoy.TabIndex = 0;
            // 
            // colCodigo
            // 
            colCodigo.HeaderText = "Código";
            colCodigo.Name = "colCodigo";
            colCodigo.ReadOnly = true;
            // 
            // colCliente
            // 
            colCliente.HeaderText = "Cliente";
            colCliente.Name = "colCliente";
            colCliente.ReadOnly = true;
            // 
            // colHabitacion
            // 
            colHabitacion.HeaderText = "Habitación";
            colHabitacion.Name = "colHabitacion";
            colHabitacion.ReadOnly = true;
            // 
            // colEntrada
            // 
            colEntrada.HeaderText = "Entrada";
            colEntrada.Name = "colEntrada";
            colEntrada.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(1020, 600);
            Controls.Add(pnlContenido);
            Controls.Add(pnlMenu);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Posada del Roble - Sistema Hotelero";
            pnlMenu.ResumeLayout(false);
            pnlContenido.ResumeLayout(false);
            pnlKpiOcupacion.ResumeLayout(false);
            pnlKpiOcupacion.PerformLayout();
            pnlKpiLlegadas.ResumeLayout(false);
            pnlKpiLlegadas.PerformLayout();
            pnlKpiSalidas.ResumeLayout(false);
            pnlKpiSalidas.PerformLayout();
            pnlKpiIngresos.ResumeLayout(false);
            pnlKpiIngresos.PerformLayout();
            pnlAlertaLimpieza.ResumeLayout(false);
            pnlAlertaLimpieza.PerformLayout();
            gbLlegadasHoy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLlegadasHoy).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Label lblLogo;
        private Button btnHabitaciones;
        private Button btnTiposHabitacion;
        private Button btnNuevaReservacion;
        private Button btnCheckInOut;
        private Button btnPlatillos;
        private Button btnRestaurante;
        private Button btnReportes;
        private Panel pnlContenido;
        private Panel pnlKpiOcupacion;
        private Label lblOcupacionValor;
        private Label label1;
        private Panel pnlKpiLlegadas;
        private Label lblLlegadasValor;
        private Label label2;
        private Panel pnlKpiSalidas;
        private Label lblSalidasValor;
        private Label label3;
        private Panel pnlKpiIngresos;
        private Label lblIngresosValor;
        private Label label4;
        private Panel pnlAlertaLimpieza;
        private Label lblAlertaLimpieza;
        private GroupBox gbLlegadasHoy;
        private DataGridView dgvLlegadasHoy;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colCliente;
        private DataGridViewTextBoxColumn colHabitacion;
        private DataGridViewTextBoxColumn colEntrada;
        private DataGridViewTextBoxColumn colEstado;
    }
}
