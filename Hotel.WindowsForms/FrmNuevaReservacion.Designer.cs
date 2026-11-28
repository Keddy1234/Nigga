namespace Hotel.WindowsForms
{
    partial class FrmNuevaReservacion
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
            tabAsistente = new TabControl();
            tabFechas = new TabPage();
            calFechas = new MonthCalendar();
            cmbTipoHabitacion = new ComboBox();
            cmbHabitacion = new ComboBox();
            cmbAdultos = new ComboBox();
            cmbNinos = new ComboBox();
            lblHabitacionCombo = new Label();
            lblNinos = new Label();
            lblPreciosInfo = new Label();
            txtDescripcionHabitacion = new TextBox();
            nudHuespedes = new NumericUpDown();
            btnBuscarDisponibles = new Button();
            dgvDisponibles = new DataGridView();
            colHabId = new DataGridViewTextBoxColumn();
            colHabNumero = new DataGridViewTextBoxColumn();
            colHabPiso = new DataGridViewTextBoxColumn();
            colHabTipo = new DataGridViewTextBoxColumn();
            colHabCapacidad = new DataGridViewTextBoxColumn();
            colHabPrecio = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            tabCliente = new TabPage();
            gbCliente = new GroupBox();
            cmbTipoDocumento = new ComboBox();
            txtNumeroDocumento = new TextBox();
            btnBuscarCliente = new Button();
            txtNombres = new TextBox();
            txtApellidos = new TextBox();
            txtEmail = new TextBox();
            txtTelefono = new TextBox();
            txtObservaciones = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            tabCotizacion = new TabPage();
            gbCotizacion = new GroupBox();
            gbPlanPago = new GroupBox();
            lblMonedaReserva = new Label();
            cmbMonedaReserva = new ComboBox();
            rbContado = new RadioButton();
            rbTasaCero = new RadioButton();
            lblPlazoTasaCero = new Label();
            cmbPlazoTasaCero = new ComboBox();
            lblDetallePlanPago = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            lblNoches = new Label();
            lblTarifaBase = new Label();
            lblRecargo = new Label();
            lblDescuento = new Label();
            lblSubtotal = new Label();
            lblIva = new Label();
            lblTotal = new Label();
            lblAnticipo = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            btnConfirmar = new Button();
            tabAsistente.SuspendLayout();
            tabFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHuespedes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).BeginInit();
            tabCliente.SuspendLayout();
            gbCliente.SuspendLayout();
            tabCotizacion.SuspendLayout();
            gbCotizacion.SuspendLayout();
            gbPlanPago.SuspendLayout();
            SuspendLayout();
            // 
            // tabAsistente
            // 
            tabAsistente.Controls.Add(tabFechas);
            tabAsistente.Controls.Add(tabCliente);
            tabAsistente.Controls.Add(tabCotizacion);
            tabAsistente.Location = new Point(16, 16);
            tabAsistente.Name = "tabAsistente";
            tabAsistente.SelectedIndex = 0;
            tabAsistente.Size = new Size(848, 456);
            tabAsistente.TabIndex = 0;
            // 
            // tabFechas
            // 
            tabFechas.BackColor = Color.FromArgb(236, 220, 192);
            tabFechas.Controls.Add(txtDescripcionHabitacion);
            tabFechas.Controls.Add(lblPreciosInfo);
            tabFechas.Controls.Add(cmbHabitacion);
            tabFechas.Controls.Add(lblHabitacionCombo);
            tabFechas.Controls.Add(cmbNinos);
            tabFechas.Controls.Add(lblNinos);
            tabFechas.Controls.Add(cmbAdultos);
            tabFechas.Controls.Add(dgvDisponibles);
            tabFechas.Controls.Add(btnBuscarDisponibles);
            tabFechas.Controls.Add(nudHuespedes);
            tabFechas.Controls.Add(label2);
            tabFechas.Controls.Add(cmbTipoHabitacion);
            tabFechas.Controls.Add(label1);
            tabFechas.Controls.Add(calFechas);
            tabFechas.Location = new Point(4, 24);
            tabFechas.Name = "tabFechas";
            tabFechas.Size = new Size(840, 428);
            tabFechas.TabIndex = 0;
            tabFechas.Text = "1. Fechas y Habitación";
            // 
            // calFechas
            // 
            calFechas.BackColor = Color.FromArgb(250, 242, 226);
            calFechas.Location = new Point(18, 22);
            calFechas.MaxSelectionCount = 30;
            calFechas.Name = "calFechas";
            calFechas.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(268, 20);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 1;
            label1.Text = "Tipo:";
            // 
            // cmbTipoHabitacion
            // 
            cmbTipoHabitacion.BackColor = Color.FromArgb(250, 242, 226);
            cmbTipoHabitacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoHabitacion.Location = new Point(308, 16);
            cmbTipoHabitacion.Name = "cmbTipoHabitacion";
            cmbTipoHabitacion.Size = new Size(145, 23);
            cmbTipoHabitacion.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(464, 20);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 3;
            label2.Text = "Adultos:";
            // 
            // cmbAdultos
            // 
            cmbAdultos.BackColor = Color.FromArgb(250, 242, 226);
            cmbAdultos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAdultos.Location = new Point(520, 16);
            cmbAdultos.Name = "cmbAdultos";
            cmbAdultos.Size = new Size(52, 23);
            cmbAdultos.TabIndex = 4;
            // 
            // lblNinos
            // 
            lblNinos.AutoSize = true;
            lblNinos.Location = new Point(580, 20);
            lblNinos.Name = "lblNinos";
            lblNinos.Size = new Size(42, 15);
            lblNinos.TabIndex = 5;
            lblNinos.Text = "Niños:";
            // 
            // cmbNinos
            // 
            cmbNinos.BackColor = Color.FromArgb(250, 242, 226);
            cmbNinos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNinos.Location = new Point(626, 16);
            cmbNinos.Name = "cmbNinos";
            cmbNinos.Size = new Size(52, 23);
            cmbNinos.TabIndex = 6;
            // 
            // nudHuespedes
            // 
            nudHuespedes.BackColor = Color.FromArgb(250, 242, 226);
            nudHuespedes.Location = new Point(572, 21);
            nudHuespedes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudHuespedes.Name = "nudHuespedes";
            nudHuespedes.Size = new Size(60, 21);
            nudHuespedes.TabIndex = 4;
            nudHuespedes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudHuespedes.Visible = false;
            // 
            // btnBuscarDisponibles
            // 
            btnBuscarDisponibles.BackColor = Color.FromArgb(92, 58, 33);
            btnBuscarDisponibles.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnBuscarDisponibles.FlatStyle = FlatStyle.Flat;
            btnBuscarDisponibles.ForeColor = Color.FromArgb(245, 232, 208);
            btnBuscarDisponibles.Location = new Point(688, 14);
            btnBuscarDisponibles.Name = "btnBuscarDisponibles";
            btnBuscarDisponibles.Size = new Size(130, 27);
            btnBuscarDisponibles.TabIndex = 7;
            btnBuscarDisponibles.Text = "Buscar";
            btnBuscarDisponibles.UseVisualStyleBackColor = false;
            // 
            // lblHabitacionCombo
            // 
            lblHabitacionCombo.AutoSize = true;
            lblHabitacionCombo.Location = new Point(268, 52);
            lblHabitacionCombo.Name = "lblHabitacionCombo";
            lblHabitacionCombo.Size = new Size(71, 15);
            lblHabitacionCombo.TabIndex = 8;
            lblHabitacionCombo.Text = "Habitación:";
            // 
            // cmbHabitacion
            // 
            cmbHabitacion.BackColor = Color.FromArgb(250, 242, 226);
            cmbHabitacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHabitacion.Location = new Point(344, 48);
            cmbHabitacion.Name = "cmbHabitacion";
            cmbHabitacion.Size = new Size(215, 23);
            cmbHabitacion.TabIndex = 9;
            // 
            // lblPreciosInfo
            // 
            lblPreciosInfo.AutoSize = true;
            lblPreciosInfo.Font = new Font("Georgia", 8.5F, FontStyle.Bold);
            lblPreciosInfo.ForeColor = Color.FromArgb(92, 58, 33);
            lblPreciosInfo.Location = new Point(568, 52);
            lblPreciosInfo.Name = "lblPreciosInfo";
            lblPreciosInfo.Size = new Size(240, 15);
            lblPreciosInfo.TabIndex = 10;
            lblPreciosInfo.Text = "Adulto: ₡75,000.00 | Niño: ₡30,000.00";
            // 
            // txtDescripcionHabitacion
            // 
            txtDescripcionHabitacion.BackColor = Color.FromArgb(245, 232, 208);
            txtDescripcionHabitacion.Location = new Point(268, 78);
            txtDescripcionHabitacion.Name = "txtDescripcionHabitacion";
            txtDescripcionHabitacion.ReadOnly = true;
            txtDescripcionHabitacion.Size = new Size(550, 21);
            txtDescripcionHabitacion.TabIndex = 11;
            // 
            // dgvDisponibles
            // 
            dgvDisponibles.AllowUserToAddRows = false;
            dgvDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDisponibles.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvDisponibles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvDisponibles.Columns.AddRange(new DataGridViewColumn[] { colHabId, colHabNumero, colHabPiso, colHabTipo, colHabCapacidad, colHabPrecio });
            dgvDisponibles.GridColor = Color.FromArgb(140, 105, 70);
            dgvDisponibles.Location = new Point(268, 106);
            dgvDisponibles.Name = "dgvDisponibles";
            dgvDisponibles.ReadOnly = true;
            dgvDisponibles.RowHeadersVisible = false;
            dgvDisponibles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDisponibles.Size = new Size(550, 300);
            dgvDisponibles.TabIndex = 12;
            // 
            // colHabId
            // 
            colHabId.HeaderText = "Id";
            colHabId.Name = "colHabId";
            colHabId.ReadOnly = true;
            // 
            // colHabNumero
            // 
            colHabNumero.HeaderText = "Número";
            colHabNumero.Name = "colHabNumero";
            colHabNumero.ReadOnly = true;
            // 
            // colHabPiso
            // 
            colHabPiso.HeaderText = "Piso";
            colHabPiso.Name = "colHabPiso";
            colHabPiso.ReadOnly = true;
            // 
            // colHabTipo
            // 
            colHabTipo.HeaderText = "Tipo";
            colHabTipo.Name = "colHabTipo";
            colHabTipo.ReadOnly = true;
            // 
            // colHabCapacidad
            // 
            colHabCapacidad.HeaderText = "Capacidad";
            colHabCapacidad.Name = "colHabCapacidad";
            colHabCapacidad.ReadOnly = true;
            // 
            // colHabPrecio
            // 
            colHabPrecio.HeaderText = "Precio / Noche";
            colHabPrecio.Name = "colHabPrecio";
            colHabPrecio.ReadOnly = true;
            // 
            // tabCliente
            // 
            tabCliente.BackColor = Color.FromArgb(236, 220, 192);
            tabCliente.Controls.Add(gbCliente);
            tabCliente.Location = new Point(4, 24);
            tabCliente.Name = "tabCliente";
            tabCliente.Size = new Size(840, 428);
            tabCliente.TabIndex = 1;
            tabCliente.Text = "2. Cliente";
            // 
            // gbCliente
            // 
            gbCliente.BackColor = Color.FromArgb(224, 204, 170);
            gbCliente.Controls.Add(txtObservaciones);
            gbCliente.Controls.Add(label9);
            gbCliente.Controls.Add(txtTelefono);
            gbCliente.Controls.Add(label8);
            gbCliente.Controls.Add(txtEmail);
            gbCliente.Controls.Add(label7);
            gbCliente.Controls.Add(txtApellidos);
            gbCliente.Controls.Add(label6);
            gbCliente.Controls.Add(txtNombres);
            gbCliente.Controls.Add(label5);
            gbCliente.Controls.Add(btnBuscarCliente);
            gbCliente.Controls.Add(txtNumeroDocumento);
            gbCliente.Controls.Add(label4);
            gbCliente.Controls.Add(cmbTipoDocumento);
            gbCliente.Controls.Add(label3);
            gbCliente.Location = new Point(24, 20);
            gbCliente.Name = "gbCliente";
            gbCliente.Size = new Size(790, 384);
            gbCliente.TabIndex = 0;
            gbCliente.TabStop = false;
            gbCliente.Text = "Datos del Cliente";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 36);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 0;
            label3.Text = "Tipo Documento:";
            // 
            // cmbTipoDocumento
            // 
            cmbTipoDocumento.BackColor = Color.FromArgb(250, 242, 226);
            cmbTipoDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoDocumento.Items.AddRange(new object[] { "DNI", "Pasaporte", "Cedula" });
            cmbTipoDocumento.Location = new Point(150, 32);
            cmbTipoDocumento.Name = "cmbTipoDocumento";
            cmbTipoDocumento.Size = new Size(140, 23);
            cmbTipoDocumento.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(316, 36);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 2;
            label4.Text = "Documento:";
            // 
            // txtNumeroDocumento
            // 
            txtNumeroDocumento.BackColor = Color.FromArgb(250, 242, 226);
            txtNumeroDocumento.Location = new Point(400, 33);
            txtNumeroDocumento.MaxLength = 10;
            txtNumeroDocumento.Name = "txtNumeroDocumento";
            txtNumeroDocumento.Size = new Size(170, 21);
            txtNumeroDocumento.TabIndex = 3;
            // 
            // btnBuscarCliente
            // 
            btnBuscarCliente.BackColor = Color.FromArgb(92, 58, 33);
            btnBuscarCliente.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnBuscarCliente.FlatStyle = FlatStyle.Flat;
            btnBuscarCliente.ForeColor = Color.FromArgb(245, 232, 208);
            btnBuscarCliente.Location = new Point(584, 30);
            btnBuscarCliente.Name = "btnBuscarCliente";
            btnBuscarCliente.Size = new Size(95, 27);
            btnBuscarCliente.TabIndex = 4;
            btnBuscarCliente.Text = "Buscar";
            btnBuscarCliente.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 84);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 5;
            label5.Text = "Nombres:";
            // 
            // txtNombres
            // 
            txtNombres.BackColor = Color.FromArgb(250, 242, 226);
            txtNombres.Location = new Point(150, 81);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(230, 21);
            txtNombres.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(404, 84);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 7;
            label6.Text = "Apellidos:";
            // 
            // txtApellidos
            // 
            txtApellidos.BackColor = Color.FromArgb(250, 242, 226);
            txtApellidos.Location = new Point(476, 81);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(250, 21);
            txtApellidos.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 132);
            label7.Name = "label7";
            label7.Size = new Size(41, 15);
            label7.TabIndex = 9;
            label7.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(250, 242, 226);
            txtEmail.Location = new Point(150, 129);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(230, 21);
            txtEmail.TabIndex = 10;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(404, 132);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 11;
            label8.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(250, 242, 226);
            txtTelefono.Location = new Point(476, 129);
            txtTelefono.MaxLength = 8;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(160, 21);
            txtTelefono.TabIndex = 12;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(24, 182);
            label9.Name = "label9";
            label9.Size = new Size(93, 15);
            label9.TabIndex = 13;
            label9.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            txtObservaciones.BackColor = Color.FromArgb(250, 242, 226);
            txtObservaciones.Location = new Point(150, 180);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(576, 110);
            txtObservaciones.TabIndex = 14;
            // 
            // tabCotizacion
            // 
            tabCotizacion.BackColor = Color.FromArgb(236, 220, 192);
            tabCotizacion.Controls.Add(gbPlanPago);
            tabCotizacion.Controls.Add(gbCotizacion);
            tabCotizacion.Location = new Point(4, 24);
            tabCotizacion.Name = "tabCotizacion";
            tabCotizacion.Size = new Size(840, 428);
            tabCotizacion.TabIndex = 2;
            tabCotizacion.Text = "3. Cotización y Pago";
            // 
            // gbCotizacion
            // 
            gbCotizacion.BackColor = Color.FromArgb(224, 204, 170);
            gbCotizacion.Controls.Add(lblAnticipo);
            gbCotizacion.Controls.Add(label17);
            gbCotizacion.Controls.Add(lblTotal);
            gbCotizacion.Controls.Add(label16);
            gbCotizacion.Controls.Add(lblIva);
            gbCotizacion.Controls.Add(label15);
            gbCotizacion.Controls.Add(lblSubtotal);
            gbCotizacion.Controls.Add(label14);
            gbCotizacion.Controls.Add(lblDescuento);
            gbCotizacion.Controls.Add(label13);
            gbCotizacion.Controls.Add(lblRecargo);
            gbCotizacion.Controls.Add(label12);
            gbCotizacion.Controls.Add(lblTarifaBase);
            gbCotizacion.Controls.Add(label11);
            gbCotizacion.Controls.Add(lblNoches);
            gbCotizacion.Controls.Add(label10);
            gbCotizacion.Location = new Point(20, 20);
            gbCotizacion.Name = "gbCotizacion";
            gbCotizacion.Size = new Size(430, 340);
            gbCotizacion.TabIndex = 0;
            gbCotizacion.TabStop = false;
            gbCotizacion.Text = "Resumen de Costos";
            // 
            // gbPlanPago
            // 
            gbPlanPago.BackColor = Color.FromArgb(224, 204, 170);
            gbPlanPago.Controls.Add(lblDetallePlanPago);
            gbPlanPago.Controls.Add(cmbPlazoTasaCero);
            gbPlanPago.Controls.Add(lblPlazoTasaCero);
            gbPlanPago.Controls.Add(rbTasaCero);
            gbPlanPago.Controls.Add(rbContado);
            gbPlanPago.Controls.Add(cmbMonedaReserva);
            gbPlanPago.Controls.Add(lblMonedaReserva);
            gbPlanPago.Location = new Point(468, 20);
            gbPlanPago.Name = "gbPlanPago";
            gbPlanPago.Size = new Size(348, 340);
            gbPlanPago.TabIndex = 1;
            gbPlanPago.TabStop = false;
            gbPlanPago.Text = "Modalidad de Pago";
            // 
            // lblMonedaReserva
            // 
            lblMonedaReserva.AutoSize = true;
            lblMonedaReserva.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            lblMonedaReserva.Location = new Point(22, 26);
            lblMonedaReserva.Name = "lblMonedaReserva";
            lblMonedaReserva.Size = new Size(64, 16);
            lblMonedaReserva.TabIndex = 0;
            lblMonedaReserva.Text = "Moneda:";
            // 
            // cmbMonedaReserva
            // 
            cmbMonedaReserva.BackColor = Color.FromArgb(250, 242, 226);
            cmbMonedaReserva.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMonedaReserva.Items.AddRange(new object[]
            {
                "Colones (₡ CRC)",
                "Dólares ($ USD)"
            });
            cmbMonedaReserva.Location = new Point(108, 22);
            cmbMonedaReserva.Name = "cmbMonedaReserva";
            cmbMonedaReserva.Size = new Size(214, 23);
            cmbMonedaReserva.TabIndex = 1;
            // 
            // rbContado
            // 
            rbContado.AutoSize = true;
            rbContado.Checked = true;
            rbContado.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            rbContado.Location = new Point(22, 58);
            rbContado.Name = "rbContado";
            rbContado.Size = new Size(192, 20);
            rbContado.TabIndex = 2;
            rbContado.TabStop = true;
            rbContado.Text = "Pago de Contado (100%)";
            // 
            // rbTasaCero
            // 
            rbTasaCero.AutoSize = true;
            rbTasaCero.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            rbTasaCero.Location = new Point(22, 88);
            rbTasaCero.Name = "rbTasaCero";
            rbTasaCero.Size = new Size(235, 20);
            rbTasaCero.TabIndex = 3;
            rbTasaCero.Text = "A Pagos (Tasa Cero - 0% Interés)";
            // 
            // lblPlazoTasaCero
            // 
            lblPlazoTasaCero.AutoSize = true;
            lblPlazoTasaCero.Location = new Point(22, 122);
            lblPlazoTasaCero.Name = "lblPlazoTasaCero";
            lblPlazoTasaCero.Size = new Size(125, 15);
            lblPlazoTasaCero.TabIndex = 4;
            lblPlazoTasaCero.Text = "Plazo de Tasa Cero:";
            // 
            // cmbPlazoTasaCero
            // 
            cmbPlazoTasaCero.BackColor = Color.FromArgb(250, 242, 226);
            cmbPlazoTasaCero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlazoTasaCero.Enabled = false;
            cmbPlazoTasaCero.Items.AddRange(new object[]
            {
                "3 Meses (Tasa Cero)",
                "6 Meses (Tasa Cero)",
                "12 Meses (Tasa Cero)"
            });
            cmbPlazoTasaCero.Location = new Point(22, 144);
            cmbPlazoTasaCero.Name = "cmbPlazoTasaCero";
            cmbPlazoTasaCero.Size = new Size(300, 23);
            cmbPlazoTasaCero.TabIndex = 5;
            // 
            // lblDetallePlanPago
            // 
            lblDetallePlanPago.BackColor = Color.FromArgb(245, 232, 208);
            lblDetallePlanPago.BorderStyle = BorderStyle.FixedSingle;
            lblDetallePlanPago.Font = new Font("Georgia", 9.5F, FontStyle.Bold);
            lblDetallePlanPago.ForeColor = Color.FromArgb(58, 102, 52);
            lblDetallePlanPago.Location = new Point(22, 182);
            lblDetallePlanPago.Name = "lblDetallePlanPago";
            lblDetallePlanPago.Padding = new Padding(10);
            lblDetallePlanPago.Size = new Size(300, 134);
            lblDetallePlanPago.TabIndex = 6;
            lblDetallePlanPago.Text = "Modalidad seleccionada:\r\nPago de Contado";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(28, 38);
            label10.Name = "label10";
            label10.Size = new Size(51, 15);
            label10.TabIndex = 0;
            label10.Text = "Noches:";
            // 
            // lblNoches
            // 
            lblNoches.AutoSize = true;
            lblNoches.Location = new Point(240, 38);
            lblNoches.Name = "lblNoches";
            lblNoches.Size = new Size(13, 15);
            lblNoches.TabIndex = 1;
            lblNoches.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(28, 72);
            label11.Name = "label11";
            label11.Size = new Size(71, 15);
            label11.TabIndex = 2;
            label11.Text = "Tarifa base:";
            // 
            // lblTarifaBase
            // 
            lblTarifaBase.AutoSize = true;
            lblTarifaBase.Location = new Point(240, 72);
            lblTarifaBase.Name = "lblTarifaBase";
            lblTarifaBase.Size = new Size(31, 15);
            lblTarifaBase.TabIndex = 3;
            lblTarifaBase.Text = "0.00";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(28, 106);
            label12.Name = "label12";
            label12.Size = new Size(146, 15);
            label12.TabIndex = 4;
            label12.Text = "Recargo personas extra:";
            // 
            // lblRecargo
            // 
            lblRecargo.AutoSize = true;
            lblRecargo.Location = new Point(240, 106);
            lblRecargo.Name = "lblRecargo";
            lblRecargo.Size = new Size(31, 15);
            lblRecargo.TabIndex = 5;
            lblRecargo.Text = "0.00";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(28, 140);
            label13.Name = "label13";
            label13.Size = new Size(116, 15);
            label13.TabIndex = 6;
            label13.Text = "Descuento cliente:";
            // 
            // lblDescuento
            // 
            lblDescuento.AutoSize = true;
            lblDescuento.Location = new Point(240, 140);
            lblDescuento.Name = "lblDescuento";
            lblDescuento.Size = new Size(31, 15);
            lblDescuento.TabIndex = 7;
            lblDescuento.Text = "0.00";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(28, 174);
            label14.Name = "label14";
            label14.Size = new Size(57, 15);
            label14.TabIndex = 8;
            label14.Text = "Subtotal:";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(240, 174);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(31, 15);
            lblSubtotal.TabIndex = 9;
            lblSubtotal.Text = "0.00";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(28, 208);
            label15.Name = "label15";
            label15.Size = new Size(68, 15);
            label15.TabIndex = 10;
            label15.Text = "IVA (16%):";
            // 
            // lblIva
            // 
            lblIva.AutoSize = true;
            lblIva.Location = new Point(240, 208);
            lblIva.Name = "lblIva";
            lblIva.Size = new Size(31, 15);
            lblIva.TabIndex = 11;
            lblIva.Text = "0.00";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Georgia", 10.5F, FontStyle.Bold);
            label16.Location = new Point(28, 248);
            label16.Name = "label16";
            label16.Size = new Size(49, 17);
            label16.TabIndex = 12;
            label16.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Georgia", 10.5F, FontStyle.Bold);
            lblTotal.Location = new Point(240, 248);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(38, 17);
            lblTotal.TabIndex = 13;
            lblTotal.Text = "0.00";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(28, 286);
            label17.Name = "label17";
            label17.Size = new Size(146, 15);
            label17.TabIndex = 14;
            label17.Text = "Anticipo mínimo (30%):";
            // 
            // lblAnticipo
            // 
            lblAnticipo.AutoSize = true;
            lblAnticipo.Location = new Point(240, 286);
            lblAnticipo.Name = "lblAnticipo";
            lblAnticipo.Size = new Size(31, 15);
            lblAnticipo.TabIndex = 15;
            lblAnticipo.Text = "0.00";
            // 
            // btnAnterior
            // 
            btnAnterior.BackColor = Color.FromArgb(92, 58, 33);
            btnAnterior.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnAnterior.FlatStyle = FlatStyle.Flat;
            btnAnterior.ForeColor = Color.FromArgb(245, 232, 208);
            btnAnterior.Location = new Point(16, 484);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(105, 32);
            btnAnterior.TabIndex = 1;
            btnAnterior.Text = "Anterior";
            btnAnterior.UseVisualStyleBackColor = false;
            // 
            // btnSiguiente
            // 
            btnSiguiente.BackColor = Color.FromArgb(92, 58, 33);
            btnSiguiente.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.ForeColor = Color.FromArgb(245, 232, 208);
            btnSiguiente.Location = new Point(620, 484);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(105, 32);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = false;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.FromArgb(58, 102, 52);
            btnConfirmar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.ForeColor = Color.FromArgb(245, 232, 208);
            btnConfirmar.Location = new Point(734, 484);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(130, 32);
            btnConfirmar.TabIndex = 3;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            // 
            // FrmNuevaReservacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(880, 532);
            Controls.Add(btnConfirmar);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(tabAsistente);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmNuevaReservacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nueva Reservación";
            tabAsistente.ResumeLayout(false);
            tabFechas.ResumeLayout(false);
            tabFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHuespedes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).EndInit();
            tabCliente.ResumeLayout(false);
            gbCliente.ResumeLayout(false);
            gbCliente.PerformLayout();
            tabCotizacion.ResumeLayout(false);
            gbCotizacion.ResumeLayout(false);
            gbCotizacion.PerformLayout();
            gbPlanPago.ResumeLayout(false);
            gbPlanPago.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabAsistente;
        private TabPage tabFechas;
        private MonthCalendar calFechas;
        private ComboBox cmbTipoHabitacion;
        private ComboBox cmbHabitacion;
        private ComboBox cmbAdultos;
        private ComboBox cmbNinos;
        private Label lblHabitacionCombo;
        private Label lblNinos;
        private Label lblPreciosInfo;
        private TextBox txtDescripcionHabitacion;
        private NumericUpDown nudHuespedes;
        private Button btnBuscarDisponibles;
        private DataGridView dgvDisponibles;
        private DataGridViewTextBoxColumn colHabId;
        private DataGridViewTextBoxColumn colHabNumero;
        private DataGridViewTextBoxColumn colHabPiso;
        private DataGridViewTextBoxColumn colHabTipo;
        private DataGridViewTextBoxColumn colHabCapacidad;
        private DataGridViewTextBoxColumn colHabPrecio;
        private Label label1;
        private Label label2;
        private TabPage tabCliente;
        private GroupBox gbCliente;
        private ComboBox cmbTipoDocumento;
        private TextBox txtNumeroDocumento;
        private Button btnBuscarCliente;
        private TextBox txtNombres;
        private TextBox txtApellidos;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private TextBox txtObservaciones;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TabPage tabCotizacion;
        private GroupBox gbCotizacion;
        private GroupBox gbPlanPago;
        private Label lblMonedaReserva;
        private ComboBox cmbMonedaReserva;
        private RadioButton rbContado;
        private RadioButton rbTasaCero;
        private Label lblPlazoTasaCero;
        private ComboBox cmbPlazoTasaCero;
        private Label lblDetallePlanPago;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label lblNoches;
        private Label lblTarifaBase;
        private Label lblRecargo;
        private Label lblDescuento;
        private Label lblSubtotal;
        private Label lblIva;
        private Label lblTotal;
        private Label lblAnticipo;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Button btnConfirmar;
    }
}
