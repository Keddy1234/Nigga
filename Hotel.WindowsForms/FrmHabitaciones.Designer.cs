namespace Hotel.WindowsForms
{
    partial class FrmHabitaciones
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
            tabHabitaciones = new TabControl();
            tabMapa = new TabPage();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            btnConsultarMapa = new Button();
            label1 = new Label();
            label2 = new Label();
            pnlColorDisponible = new Panel();
            pnlColorOcupada = new Panel();
            pnlColorLimpieza = new Panel();
            pnlColorReservada = new Panel();
            pnlColorMantenimiento = new Panel();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            flpMapa = new FlowLayoutPanel();
            tabMantenimiento = new TabPage();
            gbDatos = new GroupBox();
            txtNumero = new TextBox();
            nudPiso = new NumericUpDown();
            cmbTipo = new ComboBox();
            cmbEstado = new ComboBox();
            chkJacuzzi = new CheckBox();
            chkVistaMar = new CheckBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnDesactivar = new Button();
            dgvHabitaciones = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNumero = new DataGridViewTextBoxColumn();
            colPiso = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colJacuzzi = new DataGridViewTextBoxColumn();
            colVistaMar = new DataGridViewTextBoxColumn();
            tabHabitaciones.SuspendLayout();
            tabMapa.SuspendLayout();
            tabMantenimiento.SuspendLayout();
            gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPiso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHabitaciones).BeginInit();
            SuspendLayout();
            // 
            // tabHabitaciones
            // 
            tabHabitaciones.Controls.Add(tabMapa);
            tabHabitaciones.Controls.Add(tabMantenimiento);
            tabHabitaciones.Location = new Point(16, 16);
            tabHabitaciones.Name = "tabHabitaciones";
            tabHabitaciones.SelectedIndex = 0;
            tabHabitaciones.Size = new Size(888, 528);
            tabHabitaciones.TabIndex = 0;
            // 
            // tabMapa
            // 
            tabMapa.BackColor = Color.FromArgb(236, 220, 192);
            tabMapa.Controls.Add(flpMapa);
            tabMapa.Controls.Add(label7);
            tabMapa.Controls.Add(pnlColorMantenimiento);
            tabMapa.Controls.Add(label6);
            tabMapa.Controls.Add(pnlColorReservada);
            tabMapa.Controls.Add(label5);
            tabMapa.Controls.Add(pnlColorLimpieza);
            tabMapa.Controls.Add(label4);
            tabMapa.Controls.Add(pnlColorOcupada);
            tabMapa.Controls.Add(label3);
            tabMapa.Controls.Add(pnlColorDisponible);
            tabMapa.Controls.Add(btnConsultarMapa);
            tabMapa.Controls.Add(dtpHasta);
            tabMapa.Controls.Add(label2);
            tabMapa.Controls.Add(dtpDesde);
            tabMapa.Controls.Add(label1);
            tabMapa.Location = new Point(4, 24);
            tabMapa.Name = "tabMapa";
            tabMapa.Size = new Size(880, 500);
            tabMapa.TabIndex = 0;
            tabMapa.Text = "Mapa de Habitaciones";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 22);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 0;
            label1.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(70, 18);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(115, 21);
            dtpDesde.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(202, 22);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 2;
            label2.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(250, 18);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(115, 21);
            dtpHasta.TabIndex = 3;
            // 
            // btnConsultarMapa
            // 
            btnConsultarMapa.BackColor = Color.FromArgb(92, 58, 33);
            btnConsultarMapa.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnConsultarMapa.FlatStyle = FlatStyle.Flat;
            btnConsultarMapa.ForeColor = Color.FromArgb(245, 232, 208);
            btnConsultarMapa.Location = new Point(382, 15);
            btnConsultarMapa.Name = "btnConsultarMapa";
            btnConsultarMapa.Size = new Size(100, 27);
            btnConsultarMapa.TabIndex = 4;
            btnConsultarMapa.Text = "Consultar";
            btnConsultarMapa.UseVisualStyleBackColor = false;
            // 
            // pnlColorDisponible
            // 
            pnlColorDisponible.BackColor = Color.FromArgb(58, 125, 68);
            pnlColorDisponible.BorderStyle = BorderStyle.FixedSingle;
            pnlColorDisponible.Location = new Point(18, 58);
            pnlColorDisponible.Name = "pnlColorDisponible";
            pnlColorDisponible.Size = new Size(18, 18);
            pnlColorDisponible.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 60);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 6;
            label3.Text = "Disponible";
            // 
            // pnlColorOcupada
            // 
            pnlColorOcupada.BackColor = Color.FromArgb(168, 50, 40);
            pnlColorOcupada.BorderStyle = BorderStyle.FixedSingle;
            pnlColorOcupada.Location = new Point(134, 58);
            pnlColorOcupada.Name = "pnlColorOcupada";
            pnlColorOcupada.Size = new Size(18, 18);
            pnlColorOcupada.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(158, 60);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 8;
            label4.Text = "Ocupada";
            // 
            // pnlColorLimpieza
            // 
            pnlColorLimpieza.BackColor = Color.FromArgb(204, 154, 43);
            pnlColorLimpieza.BorderStyle = BorderStyle.FixedSingle;
            pnlColorLimpieza.Location = new Point(240, 58);
            pnlColorLimpieza.Name = "pnlColorLimpieza";
            pnlColorLimpieza.Size = new Size(18, 18);
            pnlColorLimpieza.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(264, 60);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 10;
            label5.Text = "Limpieza";
            // 
            // pnlColorReservada
            // 
            pnlColorReservada.BackColor = Color.FromArgb(52, 101, 148);
            pnlColorReservada.BorderStyle = BorderStyle.FixedSingle;
            pnlColorReservada.Location = new Point(346, 58);
            pnlColorReservada.Name = "pnlColorReservada";
            pnlColorReservada.Size = new Size(18, 18);
            pnlColorReservada.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(370, 60);
            label6.Name = "label6";
            label6.Size = new Size(65, 15);
            label6.TabIndex = 12;
            label6.Text = "Reservada";
            // 
            // pnlColorMantenimiento
            // 
            pnlColorMantenimiento.BackColor = Color.FromArgb(115, 110, 102);
            pnlColorMantenimiento.BorderStyle = BorderStyle.FixedSingle;
            pnlColorMantenimiento.Location = new Point(458, 58);
            pnlColorMantenimiento.Name = "pnlColorMantenimiento";
            pnlColorMantenimiento.Size = new Size(18, 18);
            pnlColorMantenimiento.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(482, 60);
            label7.Name = "label7";
            label7.Size = new Size(92, 15);
            label7.TabIndex = 14;
            label7.Text = "Mantenimiento";
            // 
            // flpMapa
            // 
            flpMapa.AutoScroll = true;
            flpMapa.BackColor = Color.FromArgb(224, 204, 170);
            flpMapa.BorderStyle = BorderStyle.FixedSingle;
            flpMapa.Location = new Point(18, 90);
            flpMapa.Name = "flpMapa";
            flpMapa.Size = new Size(842, 392);
            flpMapa.TabIndex = 15;
            // 
            // tabMantenimiento
            // 
            tabMantenimiento.BackColor = Color.FromArgb(236, 220, 192);
            tabMantenimiento.Controls.Add(dgvHabitaciones);
            tabMantenimiento.Controls.Add(btnDesactivar);
            tabMantenimiento.Controls.Add(btnModificar);
            tabMantenimiento.Controls.Add(btnGuardar);
            tabMantenimiento.Controls.Add(btnNuevo);
            tabMantenimiento.Controls.Add(gbDatos);
            tabMantenimiento.Location = new Point(4, 24);
            tabMantenimiento.Name = "tabMantenimiento";
            tabMantenimiento.Size = new Size(880, 500);
            tabMantenimiento.TabIndex = 1;
            tabMantenimiento.Text = "Mantenimiento";
            // 
            // gbDatos
            // 
            gbDatos.BackColor = Color.FromArgb(224, 204, 170);
            gbDatos.Controls.Add(chkVistaMar);
            gbDatos.Controls.Add(chkJacuzzi);
            gbDatos.Controls.Add(cmbEstado);
            gbDatos.Controls.Add(label11);
            gbDatos.Controls.Add(cmbTipo);
            gbDatos.Controls.Add(label10);
            gbDatos.Controls.Add(nudPiso);
            gbDatos.Controls.Add(label9);
            gbDatos.Controls.Add(txtNumero);
            gbDatos.Controls.Add(label8);
            gbDatos.Location = new Point(18, 16);
            gbDatos.Name = "gbDatos";
            gbDatos.Size = new Size(842, 118);
            gbDatos.TabIndex = 0;
            gbDatos.TabStop = false;
            gbDatos.Text = "Datos de la Habitación";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(18, 32);
            label8.Name = "label8";
            label8.Size = new Size(57, 15);
            label8.TabIndex = 0;
            label8.Text = "Número:";
            // 
            // txtNumero
            // 
            txtNumero.BackColor = Color.FromArgb(250, 242, 226);
            txtNumero.Location = new Point(84, 29);
            txtNumero.MaxLength = 8;
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(100, 21);
            txtNumero.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(214, 32);
            label9.Name = "label9";
            label9.Size = new Size(34, 15);
            label9.TabIndex = 2;
            label9.Text = "Piso:";
            // 
            // nudPiso
            // 
            nudPiso.BackColor = Color.FromArgb(250, 242, 226);
            nudPiso.Location = new Point(256, 29);
            nudPiso.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPiso.Name = "nudPiso";
            nudPiso.Size = new Size(68, 21);
            nudPiso.TabIndex = 3;
            nudPiso.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(354, 32);
            label10.Name = "label10";
            label10.Size = new Size(34, 15);
            label10.TabIndex = 4;
            label10.Text = "Tipo:";
            // 
            // cmbTipo
            // 
            cmbTipo.BackColor = Color.FromArgb(250, 242, 226);
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Location = new Point(396, 28);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(180, 23);
            cmbTipo.TabIndex = 5;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(18, 74);
            label11.Name = "label11";
            label11.Size = new Size(47, 15);
            label11.TabIndex = 6;
            label11.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.BackColor = Color.FromArgb(250, 242, 226);
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Items.AddRange(new object[] { "Disponible", "Reservada", "Ocupada", "EnLimpieza", "EnMantenimiento", "Bloqueada" });
            cmbEstado.Location = new Point(84, 70);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(160, 23);
            cmbEstado.TabIndex = 7;
            // 
            // chkJacuzzi
            // 
            chkJacuzzi.AutoSize = true;
            chkJacuzzi.Location = new Point(280, 72);
            chkJacuzzi.Name = "chkJacuzzi";
            chkJacuzzi.Size = new Size(99, 19);
            chkJacuzzi.TabIndex = 8;
            chkJacuzzi.Text = "Tiene Jacuzzi";
            // 
            // chkVistaMar
            // 
            chkVistaMar.AutoSize = true;
            chkVistaMar.Location = new Point(406, 72);
            chkVistaMar.Name = "chkVistaMar";
            chkVistaMar.Size = new Size(118, 19);
            chkVistaMar.TabIndex = 9;
            chkVistaMar.Text = "Tiene Vista al Mar";
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(92, 58, 33);
            btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevo.Location = new Point(18, 148);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(95, 30);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(58, 102, 52);
            btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.FromArgb(245, 232, 208);
            btnGuardar.Location = new Point(122, 148);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(92, 58, 33);
            btnModificar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.ForeColor = Color.FromArgb(245, 232, 208);
            btnModificar.Location = new Point(226, 148);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(95, 30);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            // 
            // btnDesactivar
            // 
            btnDesactivar.BackColor = Color.FromArgb(138, 44, 36);
            btnDesactivar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnDesactivar.FlatStyle = FlatStyle.Flat;
            btnDesactivar.ForeColor = Color.FromArgb(245, 232, 208);
            btnDesactivar.Location = new Point(330, 148);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(95, 30);
            btnDesactivar.TabIndex = 4;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseVisualStyleBackColor = false;
            // 
            // dgvHabitaciones
            // 
            dgvHabitaciones.AllowUserToAddRows = false;
            dgvHabitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHabitaciones.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvHabitaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvHabitaciones.Columns.AddRange(new DataGridViewColumn[] { colId, colNumero, colPiso, colTipo, colEstado, colJacuzzi, colVistaMar });
            dgvHabitaciones.GridColor = Color.FromArgb(140, 105, 70);
            dgvHabitaciones.Location = new Point(18, 192);
            dgvHabitaciones.Name = "dgvHabitaciones";
            dgvHabitaciones.ReadOnly = true;
            dgvHabitaciones.RowHeadersVisible = false;
            dgvHabitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHabitaciones.Size = new Size(842, 290);
            dgvHabitaciones.TabIndex = 5;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNumero
            // 
            colNumero.HeaderText = "Número";
            colNumero.Name = "colNumero";
            colNumero.ReadOnly = true;
            // 
            // colPiso
            // 
            colPiso.HeaderText = "Piso";
            colPiso.Name = "colPiso";
            colPiso.ReadOnly = true;
            // 
            // colTipo
            // 
            colTipo.HeaderText = "Tipo";
            colTipo.Name = "colTipo";
            colTipo.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colJacuzzi
            // 
            colJacuzzi.HeaderText = "Jacuzzi";
            colJacuzzi.Name = "colJacuzzi";
            colJacuzzi.ReadOnly = true;
            // 
            // colVistaMar
            // 
            colVistaMar.HeaderText = "Vista al Mar";
            colVistaMar.Name = "colVistaMar";
            colVistaMar.ReadOnly = true;
            // 
            // FrmHabitaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(920, 560);
            Controls.Add(tabHabitaciones);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmHabitaciones";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Habitaciones";
            tabHabitaciones.ResumeLayout(false);
            tabMapa.ResumeLayout(false);
            tabMapa.PerformLayout();
            tabMantenimiento.ResumeLayout(false);
            gbDatos.ResumeLayout(false);
            gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPiso).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHabitaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabHabitaciones;
        private TabPage tabMapa;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Button btnConsultarMapa;
        private Label label1;
        private Label label2;
        private Panel pnlColorDisponible;
        private Panel pnlColorOcupada;
        private Panel pnlColorLimpieza;
        private Panel pnlColorReservada;
        private Panel pnlColorMantenimiento;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private FlowLayoutPanel flpMapa;
        private TabPage tabMantenimiento;
        private GroupBox gbDatos;
        private TextBox txtNumero;
        private NumericUpDown nudPiso;
        private ComboBox cmbTipo;
        private ComboBox cmbEstado;
        private CheckBox chkJacuzzi;
        private CheckBox chkVistaMar;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnDesactivar;
        private DataGridView dgvHabitaciones;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNumero;
        private DataGridViewTextBoxColumn colPiso;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colJacuzzi;
        private DataGridViewTextBoxColumn colVistaMar;
    }
}
