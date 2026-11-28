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
            pnlPaisajeSidebar = new Panel();
            btnReportes = new Button();
            btnRestaurante = new Button();
            btnPlatillos = new Button();
            btnCheckInOut = new Button();
            btnNuevaReservacion = new Button();
            btnTiposHabitacion = new Button();
            btnHabitaciones = new Button();
            pnlLogo = new Panel();
            lblSubLogo = new Label();
            pnlLogoLinea = new Panel();
            lblLogo = new Label();
            pnlContenido = new Panel();
            pnlCuerpoDashboard = new Panel();
            gbLlegadasHoy = new GroupBox();
            dgvLlegadasHoy = new DataGridView();
            colCodigo = new DataGridViewTextBoxColumn();
            colCliente = new DataGridViewTextBoxColumn();
            colHabitacion = new DataGridViewTextBoxColumn();
            colEntrada = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlEspaciador2 = new Panel();
            pnlAlertaLimpieza = new Panel();
            btnVerMapaRapido = new Button();
            lblAlertaLimpieza = new Label();
            pnlEspaciador1 = new Panel();
            tlpKpis = new TableLayoutPanel();
            pnlKpiOcupacion = new Panel();
            lblSubOcupacion = new Label();
            lblOcupacionValor = new Label();
            label1 = new Label();
            pnlKpiLlegadas = new Panel();
            lblSubLlegadas = new Label();
            lblLlegadasValor = new Label();
            label2 = new Label();
            pnlKpiSalidas = new Panel();
            lblSubSalidas = new Label();
            lblSalidasValor = new Label();
            label3 = new Label();
            pnlKpiIngresos = new Panel();
            lblSubIngresos = new Label();
            lblIngresosValor = new Label();
            label4 = new Label();
            pnlHeroContenedor = new Panel();
            btnActualizarDashboard = new Button();
            pnlMenu.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlCuerpoDashboard.SuspendLayout();
            gbLlegadasHoy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLlegadasHoy).BeginInit();
            pnlAlertaLimpieza.SuspendLayout();
            tlpKpis.SuspendLayout();
            pnlKpiOcupacion.SuspendLayout();
            pnlKpiLlegadas.SuspendLayout();
            pnlKpiSalidas.SuspendLayout();
            pnlKpiIngresos.SuspendLayout();
            pnlHeroContenedor.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(38, 25, 16);
            pnlMenu.Controls.Add(pnlPaisajeSidebar);
            pnlMenu.Controls.Add(btnReportes);
            pnlMenu.Controls.Add(btnRestaurante);
            pnlMenu.Controls.Add(btnPlatillos);
            pnlMenu.Controls.Add(btnCheckInOut);
            pnlMenu.Controls.Add(btnNuevaReservacion);
            pnlMenu.Controls.Add(btnTiposHabitacion);
            pnlMenu.Controls.Add(btnHabitaciones);
            pnlMenu.Controls.Add(pnlLogo);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(236, 720);
            pnlMenu.TabIndex = 0;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = Color.FromArgb(28, 18, 11);
            pnlLogo.Controls.Add(lblSubLogo);
            pnlLogo.Controls.Add(pnlLogoLinea);
            pnlLogo.Controls.Add(lblLogo);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(236, 82);
            pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.BackColor = Color.FromArgb(28, 18, 11);
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Georgia", 13.5F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(232, 198, 128);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(236, 46);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "POSADA DEL ROBLE";
            lblLogo.TextAlign = ContentAlignment.BottomCenter;
            // 
            // lblSubLogo
            // 
            lblSubLogo.BackColor = Color.FromArgb(28, 18, 11);
            lblSubLogo.Dock = DockStyle.Fill;
            lblSubLogo.Font = new Font("Georgia", 8F, FontStyle.Bold);
            lblSubLogo.ForeColor = Color.FromArgb(201, 174, 132);
            lblSubLogo.Location = new Point(0, 46);
            lblSubLogo.Name = "lblSubLogo";
            lblSubLogo.Padding = new Padding(0, 3, 0, 0);
            lblSubLogo.Size = new Size(236, 34);
            lblSubLogo.TabIndex = 1;
            lblSubLogo.Text = "ADMINISTRACIÓN";
            lblSubLogo.TextAlign = ContentAlignment.TopCenter;
            // 
            // pnlLogoLinea
            // 
            pnlLogoLinea.BackColor = Color.FromArgb(196, 154, 69);
            pnlLogoLinea.Dock = DockStyle.Bottom;
            pnlLogoLinea.Location = new Point(0, 80);
            pnlLogoLinea.Name = "pnlLogoLinea";
            pnlLogoLinea.Size = new Size(236, 2);
            pnlLogoLinea.TabIndex = 2;
            // 
            // btnHabitaciones
            // 
            btnHabitaciones.BackColor = Color.FromArgb(76, 48, 28);
            btnHabitaciones.Cursor = Cursors.Hand;
            btnHabitaciones.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnHabitaciones.FlatStyle = FlatStyle.Flat;
            btnHabitaciones.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnHabitaciones.ForeColor = Color.FromArgb(245, 232, 208);
            btnHabitaciones.Location = new Point(16, 98);
            btnHabitaciones.Name = "btnHabitaciones";
            btnHabitaciones.Padding = new Padding(14, 0, 0, 0);
            btnHabitaciones.Size = new Size(204, 40);
            btnHabitaciones.TabIndex = 1;
            btnHabitaciones.Text = "Habitaciones";
            btnHabitaciones.TextAlign = ContentAlignment.MiddleLeft;
            btnHabitaciones.UseVisualStyleBackColor = false;
            // 
            // btnTiposHabitacion
            // 
            btnTiposHabitacion.BackColor = Color.FromArgb(76, 48, 28);
            btnTiposHabitacion.Cursor = Cursors.Hand;
            btnTiposHabitacion.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnTiposHabitacion.FlatStyle = FlatStyle.Flat;
            btnTiposHabitacion.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnTiposHabitacion.ForeColor = Color.FromArgb(245, 232, 208);
            btnTiposHabitacion.Location = new Point(16, 146);
            btnTiposHabitacion.Name = "btnTiposHabitacion";
            btnTiposHabitacion.Padding = new Padding(14, 0, 0, 0);
            btnTiposHabitacion.Size = new Size(204, 40);
            btnTiposHabitacion.TabIndex = 2;
            btnTiposHabitacion.Text = "Tipos de Habitación";
            btnTiposHabitacion.TextAlign = ContentAlignment.MiddleLeft;
            btnTiposHabitacion.UseVisualStyleBackColor = false;
            // 
            // btnNuevaReservacion
            // 
            btnNuevaReservacion.BackColor = Color.FromArgb(76, 48, 28);
            btnNuevaReservacion.Cursor = Cursors.Hand;
            btnNuevaReservacion.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnNuevaReservacion.FlatStyle = FlatStyle.Flat;
            btnNuevaReservacion.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnNuevaReservacion.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevaReservacion.Location = new Point(16, 194);
            btnNuevaReservacion.Name = "btnNuevaReservacion";
            btnNuevaReservacion.Padding = new Padding(14, 0, 0, 0);
            btnNuevaReservacion.Size = new Size(204, 40);
            btnNuevaReservacion.TabIndex = 3;
            btnNuevaReservacion.Text = "Nueva Reservación";
            btnNuevaReservacion.TextAlign = ContentAlignment.MiddleLeft;
            btnNuevaReservacion.UseVisualStyleBackColor = false;
            // 
            // btnCheckInOut
            // 
            btnCheckInOut.BackColor = Color.FromArgb(76, 48, 28);
            btnCheckInOut.Cursor = Cursors.Hand;
            btnCheckInOut.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnCheckInOut.FlatStyle = FlatStyle.Flat;
            btnCheckInOut.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnCheckInOut.ForeColor = Color.FromArgb(245, 232, 208);
            btnCheckInOut.Location = new Point(16, 242);
            btnCheckInOut.Name = "btnCheckInOut";
            btnCheckInOut.Padding = new Padding(14, 0, 0, 0);
            btnCheckInOut.Size = new Size(204, 40);
            btnCheckInOut.TabIndex = 4;
            btnCheckInOut.Text = "Check-In / Check-Out";
            btnCheckInOut.TextAlign = ContentAlignment.MiddleLeft;
            btnCheckInOut.UseVisualStyleBackColor = false;
            // 
            // btnPlatillos
            // 
            btnPlatillos.BackColor = Color.FromArgb(76, 48, 28);
            btnPlatillos.Cursor = Cursors.Hand;
            btnPlatillos.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnPlatillos.FlatStyle = FlatStyle.Flat;
            btnPlatillos.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnPlatillos.ForeColor = Color.FromArgb(245, 232, 208);
            btnPlatillos.Location = new Point(16, 290);
            btnPlatillos.Name = "btnPlatillos";
            btnPlatillos.Padding = new Padding(14, 0, 0, 0);
            btnPlatillos.Size = new Size(204, 40);
            btnPlatillos.TabIndex = 5;
            btnPlatillos.Text = "Platillos";
            btnPlatillos.TextAlign = ContentAlignment.MiddleLeft;
            btnPlatillos.UseVisualStyleBackColor = false;
            // 
            // btnRestaurante
            // 
            btnRestaurante.BackColor = Color.FromArgb(76, 48, 28);
            btnRestaurante.Cursor = Cursors.Hand;
            btnRestaurante.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnRestaurante.FlatStyle = FlatStyle.Flat;
            btnRestaurante.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnRestaurante.ForeColor = Color.FromArgb(245, 232, 208);
            btnRestaurante.Location = new Point(16, 338);
            btnRestaurante.Name = "btnRestaurante";
            btnRestaurante.Padding = new Padding(14, 0, 0, 0);
            btnRestaurante.Size = new Size(204, 40);
            btnRestaurante.TabIndex = 6;
            btnRestaurante.Text = "Restaurante";
            btnRestaurante.TextAlign = ContentAlignment.MiddleLeft;
            btnRestaurante.UseVisualStyleBackColor = false;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(76, 48, 28);
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderColor = Color.FromArgb(186, 145, 65);
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            btnReportes.ForeColor = Color.FromArgb(245, 232, 208);
            btnReportes.Location = new Point(16, 386);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(14, 0, 0, 0);
            btnReportes.Size = new Size(204, 40);
            btnReportes.TabIndex = 7;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
            // 
            // pnlPaisajeSidebar
            // 
            pnlPaisajeSidebar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnlPaisajeSidebar.BackColor = Color.FromArgb(28, 18, 11);
            pnlPaisajeSidebar.Location = new Point(16, 526);
            pnlPaisajeSidebar.Name = "pnlPaisajeSidebar";
            pnlPaisajeSidebar.Size = new Size(204, 178);
            pnlPaisajeSidebar.TabIndex = 8;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(240, 226, 200);
            pnlContenido.Controls.Add(pnlCuerpoDashboard);
            pnlContenido.Controls.Add(pnlHeroContenedor);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(236, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(944, 720);
            pnlContenido.TabIndex = 1;
            // 
            // pnlHeroContenedor
            // 
            pnlHeroContenedor.BackColor = Color.FromArgb(38, 25, 16);
            pnlHeroContenedor.Controls.Add(btnActualizarDashboard);
            pnlHeroContenedor.Dock = DockStyle.Top;
            pnlHeroContenedor.Location = new Point(0, 0);
            pnlHeroContenedor.Name = "pnlHeroContenedor";
            pnlHeroContenedor.Size = new Size(944, 164);
            pnlHeroContenedor.TabIndex = 0;
            // 
            // btnActualizarDashboard
            // 
            btnActualizarDashboard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnActualizarDashboard.BackColor = Color.FromArgb(58, 102, 52);
            btnActualizarDashboard.Cursor = Cursors.Hand;
            btnActualizarDashboard.FlatAppearance.BorderColor = Color.FromArgb(232, 198, 128);
            btnActualizarDashboard.FlatStyle = FlatStyle.Flat;
            btnActualizarDashboard.Font = new Font("Georgia", 9F, FontStyle.Bold);
            btnActualizarDashboard.ForeColor = Color.FromArgb(250, 242, 226);
            btnActualizarDashboard.Location = new Point(774, 114);
            btnActualizarDashboard.Name = "btnActualizarDashboard";
            btnActualizarDashboard.Size = new Size(148, 32);
            btnActualizarDashboard.TabIndex = 0;
            btnActualizarDashboard.Text = "Actualizar Panel";
            btnActualizarDashboard.UseVisualStyleBackColor = false;
            // 
            // pnlCuerpoDashboard
            // 
            pnlCuerpoDashboard.BackColor = Color.FromArgb(240, 226, 200);
            pnlCuerpoDashboard.Controls.Add(gbLlegadasHoy);
            pnlCuerpoDashboard.Controls.Add(pnlEspaciador2);
            pnlCuerpoDashboard.Controls.Add(pnlAlertaLimpieza);
            pnlCuerpoDashboard.Controls.Add(pnlEspaciador1);
            pnlCuerpoDashboard.Controls.Add(tlpKpis);
            pnlCuerpoDashboard.Dock = DockStyle.Fill;
            pnlCuerpoDashboard.Location = new Point(0, 164);
            pnlCuerpoDashboard.Name = "pnlCuerpoDashboard";
            pnlCuerpoDashboard.Padding = new Padding(22, 16, 22, 18);
            pnlCuerpoDashboard.Size = new Size(944, 556);
            pnlCuerpoDashboard.TabIndex = 1;
            // 
            // tlpKpis
            // 
            tlpKpis.ColumnCount = 4;
            tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpKpis.Controls.Add(pnlKpiOcupacion, 0, 0);
            tlpKpis.Controls.Add(pnlKpiLlegadas, 1, 0);
            tlpKpis.Controls.Add(pnlKpiSalidas, 2, 0);
            tlpKpis.Controls.Add(pnlKpiIngresos, 3, 0);
            tlpKpis.Dock = DockStyle.Top;
            tlpKpis.Location = new Point(22, 16);
            tlpKpis.Name = "tlpKpis";
            tlpKpis.RowCount = 1;
            tlpKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpKpis.Size = new Size(900, 104);
            tlpKpis.TabIndex = 0;
            // 
            // pnlKpiOcupacion
            // 
            pnlKpiOcupacion.BackColor = Color.FromArgb(68, 43, 27);
            pnlKpiOcupacion.Controls.Add(lblSubOcupacion);
            pnlKpiOcupacion.Controls.Add(lblOcupacionValor);
            pnlKpiOcupacion.Controls.Add(label1);
            pnlKpiOcupacion.Dock = DockStyle.Fill;
            pnlKpiOcupacion.Location = new Point(0, 0);
            pnlKpiOcupacion.Margin = new Padding(0, 0, 12, 0);
            pnlKpiOcupacion.Name = "pnlKpiOcupacion";
            pnlKpiOcupacion.Size = new Size(213, 104);
            pnlKpiOcupacion.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Georgia", 9.25F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(245, 232, 208);
            label1.Location = new Point(14, 14);
            label1.Name = "label1";
            label1.Size = new Size(124, 16);
            label1.TabIndex = 0;
            label1.Text = "Ocupación del día";
            // 
            // lblOcupacionValor
            // 
            lblOcupacionValor.AutoSize = true;
            lblOcupacionValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblOcupacionValor.ForeColor = Color.FromArgb(232, 198, 128);
            lblOcupacionValor.Location = new Point(14, 38);
            lblOcupacionValor.Name = "lblOcupacionValor";
            lblOcupacionValor.Size = new Size(56, 29);
            lblOcupacionValor.TabIndex = 1;
            lblOcupacionValor.Text = "0 %";
            // 
            // lblSubOcupacion
            // 
            lblSubOcupacion.AutoSize = true;
            lblSubOcupacion.Font = new Font("Georgia", 8F, FontStyle.Italic);
            lblSubOcupacion.ForeColor = Color.FromArgb(220, 205, 180);
            lblSubOcupacion.Location = new Point(14, 75);
            lblSubOcupacion.Name = "lblSubOcupacion";
            lblSubOcupacion.Size = new Size(118, 14);
            lblSubOcupacion.TabIndex = 2;
            lblSubOcupacion.Text = "Habitaciones en uso";
            // 
            // pnlKpiLlegadas
            // 
            pnlKpiLlegadas.BackColor = Color.FromArgb(52, 82, 43);
            pnlKpiLlegadas.Controls.Add(lblSubLlegadas);
            pnlKpiLlegadas.Controls.Add(lblLlegadasValor);
            pnlKpiLlegadas.Controls.Add(label2);
            pnlKpiLlegadas.Dock = DockStyle.Fill;
            pnlKpiLlegadas.Location = new Point(225, 0);
            pnlKpiLlegadas.Margin = new Padding(0, 0, 12, 0);
            pnlKpiLlegadas.Name = "pnlKpiLlegadas";
            pnlKpiLlegadas.Size = new Size(213, 104);
            pnlKpiLlegadas.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Georgia", 9.25F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(245, 232, 208);
            label2.Location = new Point(14, 14);
            label2.Name = "label2";
            label2.Size = new Size(139, 16);
            label2.TabIndex = 0;
            label2.Text = "Llegadas pendientes";
            // 
            // lblLlegadasValor
            // 
            lblLlegadasValor.AutoSize = true;
            lblLlegadasValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblLlegadasValor.ForeColor = Color.FromArgb(245, 232, 208);
            lblLlegadasValor.Location = new Point(14, 38);
            lblLlegadasValor.Name = "lblLlegadasValor";
            lblLlegadasValor.Size = new Size(29, 29);
            lblLlegadasValor.TabIndex = 1;
            lblLlegadasValor.Text = "0";
            // 
            // lblSubLlegadas
            // 
            lblSubLlegadas.AutoSize = true;
            lblSubLlegadas.Font = new Font("Georgia", 8F, FontStyle.Italic);
            lblSubLlegadas.ForeColor = Color.FromArgb(220, 205, 180);
            lblSubLlegadas.Location = new Point(14, 75);
            lblSubLlegadas.Name = "lblSubLlegadas";
            lblSubLlegadas.Size = new Size(121, 14);
            lblSubLlegadas.TabIndex = 2;
            lblSubLlegadas.Text = "Check-in por atender";
            // 
            // pnlKpiSalidas
            // 
            pnlKpiSalidas.BackColor = Color.FromArgb(135, 84, 32);
            pnlKpiSalidas.Controls.Add(lblSubSalidas);
            pnlKpiSalidas.Controls.Add(lblSalidasValor);
            pnlKpiSalidas.Controls.Add(label3);
            pnlKpiSalidas.Dock = DockStyle.Fill;
            pnlKpiSalidas.Location = new Point(450, 0);
            pnlKpiSalidas.Margin = new Padding(0, 0, 12, 0);
            pnlKpiSalidas.Name = "pnlKpiSalidas";
            pnlKpiSalidas.Size = new Size(213, 104);
            pnlKpiSalidas.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Georgia", 9.25F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(245, 232, 208);
            label3.Location = new Point(14, 14);
            label3.Name = "label3";
            label3.Size = new Size(141, 16);
            label3.TabIndex = 0;
            label3.Text = "Salidas programadas";
            // 
            // lblSalidasValor
            // 
            lblSalidasValor.AutoSize = true;
            lblSalidasValor.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lblSalidasValor.ForeColor = Color.FromArgb(245, 232, 208);
            lblSalidasValor.Location = new Point(14, 38);
            lblSalidasValor.Name = "lblSalidasValor";
            lblSalidasValor.Size = new Size(29, 29);
            lblSalidasValor.TabIndex = 1;
            lblSalidasValor.Text = "0";
            // 
            // lblSubSalidas
            // 
            lblSubSalidas.AutoSize = true;
            lblSubSalidas.Font = new Font("Georgia", 8F, FontStyle.Italic);
            lblSubSalidas.ForeColor = Color.FromArgb(220, 205, 180);
            lblSubSalidas.Location = new Point(14, 75);
            lblSubSalidas.Name = "lblSubSalidas";
            lblSubSalidas.Size = new Size(138, 14);
            lblSubSalidas.TabIndex = 2;
            lblSubSalidas.Text = "Check-out programados";
            // 
            // pnlKpiIngresos
            // 
            pnlKpiIngresos.BackColor = Color.FromArgb(112, 38, 32);
            pnlKpiIngresos.Controls.Add(lblSubIngresos);
            pnlKpiIngresos.Controls.Add(lblIngresosValor);
            pnlKpiIngresos.Controls.Add(label4);
            pnlKpiIngresos.Dock = DockStyle.Fill;
            pnlKpiIngresos.Location = new Point(675, 0);
            pnlKpiIngresos.Margin = new Padding(0);
            pnlKpiIngresos.Name = "pnlKpiIngresos";
            pnlKpiIngresos.Size = new Size(225, 104);
            pnlKpiIngresos.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Georgia", 9.25F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(245, 232, 208);
            label4.Location = new Point(14, 14);
            label4.Name = "label4";
            label4.Size = new Size(63, 16);
            label4.TabIndex = 0;
            label4.Text = "Ingresos";
            // 
            // lblIngresosValor
            // 
            lblIngresosValor.AutoSize = true;
            lblIngresosValor.Font = new Font("Georgia", 15F, FontStyle.Bold);
            lblIngresosValor.ForeColor = Color.FromArgb(232, 198, 128);
            lblIngresosValor.Location = new Point(14, 41);
            lblIngresosValor.Name = "lblIngresosValor";
            lblIngresosValor.Size = new Size(71, 24);
            lblIngresosValor.TabIndex = 1;
            lblIngresosValor.Text = "₡0.00";
            // 
            // lblSubIngresos
            // 
            lblSubIngresos.AutoSize = true;
            lblSubIngresos.Font = new Font("Georgia", 8F, FontStyle.Italic);
            lblSubIngresos.ForeColor = Color.FromArgb(220, 205, 180);
            lblSubIngresos.Location = new Point(14, 75);
            lblSubIngresos.Name = "lblSubIngresos";
            lblSubIngresos.Size = new Size(134, 14);
            lblSubIngresos.TabIndex = 2;
            lblSubIngresos.Text = "Facturación del período";
            // 
            // pnlEspaciador1
            // 
            pnlEspaciador1.BackColor = Color.Transparent;
            pnlEspaciador1.Dock = DockStyle.Top;
            pnlEspaciador1.Location = new Point(22, 120);
            pnlEspaciador1.Name = "pnlEspaciador1";
            pnlEspaciador1.Size = new Size(900, 12);
            pnlEspaciador1.TabIndex = 1;
            // 
            // pnlAlertaLimpieza
            // 
            pnlAlertaLimpieza.BackColor = Color.FromArgb(228, 192, 118);
            pnlAlertaLimpieza.Controls.Add(btnVerMapaRapido);
            pnlAlertaLimpieza.Controls.Add(lblAlertaLimpieza);
            pnlAlertaLimpieza.Dock = DockStyle.Top;
            pnlAlertaLimpieza.Location = new Point(22, 132);
            pnlAlertaLimpieza.Name = "pnlAlertaLimpieza";
            pnlAlertaLimpieza.Size = new Size(900, 44);
            pnlAlertaLimpieza.TabIndex = 2;
            // 
            // lblAlertaLimpieza
            // 
            lblAlertaLimpieza.AutoSize = true;
            lblAlertaLimpieza.Font = new Font("Georgia", 10F, FontStyle.Bold);
            lblAlertaLimpieza.ForeColor = Color.FromArgb(46, 31, 20);
            lblAlertaLimpieza.Location = new Point(16, 13);
            lblAlertaLimpieza.Name = "lblAlertaLimpieza";
            lblAlertaLimpieza.Size = new Size(203, 17);
            lblAlertaLimpieza.TabIndex = 0;
            lblAlertaLimpieza.Text = "Habitaciones por limpiar: 0";
            // 
            // btnVerMapaRapido
            // 
            btnVerMapaRapido.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerMapaRapido.BackColor = Color.FromArgb(76, 48, 28);
            btnVerMapaRapido.Cursor = Cursors.Hand;
            btnVerMapaRapido.FlatAppearance.BorderColor = Color.FromArgb(140, 82, 32);
            btnVerMapaRapido.FlatStyle = FlatStyle.Flat;
            btnVerMapaRapido.Font = new Font("Georgia", 8.75F, FontStyle.Bold);
            btnVerMapaRapido.ForeColor = Color.FromArgb(245, 232, 208);
            btnVerMapaRapido.Location = new Point(780, 8);
            btnVerMapaRapido.Name = "btnVerMapaRapido";
            btnVerMapaRapido.Size = new Size(108, 28);
            btnVerMapaRapido.TabIndex = 1;
            btnVerMapaRapido.Text = "Ver Mapa";
            btnVerMapaRapido.UseVisualStyleBackColor = false;
            // 
            // pnlEspaciador2
            // 
            pnlEspaciador2.BackColor = Color.Transparent;
            pnlEspaciador2.Dock = DockStyle.Top;
            pnlEspaciador2.Location = new Point(22, 176);
            pnlEspaciador2.Name = "pnlEspaciador2";
            pnlEspaciador2.Size = new Size(900, 12);
            pnlEspaciador2.TabIndex = 3;
            // 
            // gbLlegadasHoy
            // 
            gbLlegadasHoy.BackColor = Color.FromArgb(230, 212, 180);
            gbLlegadasHoy.Controls.Add(dgvLlegadasHoy);
            gbLlegadasHoy.Dock = DockStyle.Fill;
            gbLlegadasHoy.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            gbLlegadasHoy.ForeColor = Color.FromArgb(46, 31, 20);
            gbLlegadasHoy.Location = new Point(22, 188);
            gbLlegadasHoy.Name = "gbLlegadasHoy";
            gbLlegadasHoy.Padding = new Padding(14, 12, 14, 14);
            gbLlegadasHoy.Size = new Size(900, 350);
            gbLlegadasHoy.TabIndex = 4;
            gbLlegadasHoy.TabStop = false;
            gbLlegadasHoy.Text = "Reservaciones recientes y llegadas del día";
            // 
            // dgvLlegadasHoy
            // 
            dgvLlegadasHoy.AllowUserToAddRows = false;
            dgvLlegadasHoy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLlegadasHoy.BackgroundColor = Color.FromArgb(248, 238, 218);
            dgvLlegadasHoy.ColumnHeadersHeight = 34;
            dgvLlegadasHoy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvLlegadasHoy.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colCliente, colHabitacion, colEntrada, colEstado });
            dgvLlegadasHoy.Dock = DockStyle.Fill;
            dgvLlegadasHoy.GridColor = Color.FromArgb(196, 170, 134);
            dgvLlegadasHoy.Location = new Point(14, 29);
            dgvLlegadasHoy.Name = "dgvLlegadasHoy";
            dgvLlegadasHoy.ReadOnly = true;
            dgvLlegadasHoy.RowHeadersVisible = false;
            dgvLlegadasHoy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLlegadasHoy.Size = new Size(872, 307);
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
            BackColor = Color.FromArgb(240, 226, 200);
            ClientSize = new Size(1180, 720);
            Controls.Add(pnlContenido);
            Controls.Add(pnlMenu);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            MinimumSize = new Size(1080, 680);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Posada del Roble — Panel de Administración Hotelera";
            pnlMenu.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlContenido.ResumeLayout(false);
            pnlCuerpoDashboard.ResumeLayout(false);
            gbLlegadasHoy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLlegadasHoy).EndInit();
            pnlAlertaLimpieza.ResumeLayout(false);
            pnlAlertaLimpieza.PerformLayout();
            tlpKpis.ResumeLayout(false);
            pnlKpiOcupacion.ResumeLayout(false);
            pnlKpiOcupacion.PerformLayout();
            pnlKpiLlegadas.ResumeLayout(false);
            pnlKpiLlegadas.PerformLayout();
            pnlKpiSalidas.ResumeLayout(false);
            pnlKpiSalidas.PerformLayout();
            pnlKpiIngresos.ResumeLayout(false);
            pnlKpiIngresos.PerformLayout();
            pnlHeroContenedor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Panel pnlLogo;
        private Label lblLogo;
        private Label lblSubLogo;
        private Panel pnlLogoLinea;
        private Button btnHabitaciones;
        private Button btnTiposHabitacion;
        private Button btnNuevaReservacion;
        private Button btnCheckInOut;
        private Button btnPlatillos;
        private Button btnRestaurante;
        private Button btnReportes;
        private Panel pnlPaisajeSidebar;
        private Panel pnlContenido;
        private Panel pnlHeroContenedor;
        private Button btnActualizarDashboard;
        private Panel pnlCuerpoDashboard;
        private TableLayoutPanel tlpKpis;
        private Panel pnlKpiOcupacion;
        private Label lblOcupacionValor;
        private Label lblSubOcupacion;
        private Label label1;
        private Panel pnlKpiLlegadas;
        private Label lblLlegadasValor;
        private Label lblSubLlegadas;
        private Label label2;
        private Panel pnlKpiSalidas;
        private Label lblSalidasValor;
        private Label lblSubSalidas;
        private Label label3;
        private Panel pnlKpiIngresos;
        private Label lblIngresosValor;
        private Label lblSubIngresos;
        private Label label4;
        private Panel pnlEspaciador1;
        private Panel pnlAlertaLimpieza;
        private Label lblAlertaLimpieza;
        private Button btnVerMapaRapido;
        private Panel pnlEspaciador2;
        private GroupBox gbLlegadasHoy;
        private DataGridView dgvLlegadasHoy;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colCliente;
        private DataGridViewTextBoxColumn colHabitacion;
        private DataGridViewTextBoxColumn colEntrada;
        private DataGridViewTextBoxColumn colEstado;
    }
}
