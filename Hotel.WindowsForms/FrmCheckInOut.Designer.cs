namespace Hotel.WindowsForms
{
    partial class FrmCheckInOut
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
            label1 = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            lblPendientes = new Label();
            cmbPendientesCheckIn = new ComboBox();
            gbReservacion = new GroupBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblCodigo = new Label();
            lblCliente = new Label();
            lblHabitacion = new Label();
            lblEntrada = new Label();
            lblSalida = new Label();
            lblEstado = new Label();
            gbCheckIn = new GroupBox();
            label8 = new Label();
            txtAcompanantes = new TextBox();
            label9 = new Label();
            txtLlave = new TextBox();
            btnCheckIn = new Button();
            gbCuenta = new GroupBox();
            dgvConsumos = new DataGridView();
            colFecha = new DataGridViewTextBoxColumn();
            colConcepto = new DataGridViewTextBoxColumn();
            colMonto = new DataGridViewTextBoxColumn();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            lblHospedaje = new Label();
            lblConsumos = new Label();
            lblPagado = new Label();
            lblSaldo = new Label();
            gbPago = new GroupBox();
            label14 = new Label();
            cmbMetodoPago = new ComboBox();
            label15 = new Label();
            nudMontoPago = new NumericUpDown();
            label16 = new Label();
            txtReferencia = new TextBox();
            btnRegistrarPago = new Button();
            btnCheckOut = new Button();
            btnFactura = new Button();
            gbReservacion.SuspendLayout();
            gbCheckIn.SuspendLayout();
            gbCuenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConsumos).BeginInit();
            gbPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMontoPago).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 22);
            label1.Name = "label1";
            label1.Size = new Size(165, 15);
            label1.TabIndex = 0;
            label1.Text = "Documento de Identificación:";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(250, 242, 226);
            txtBuscar.Location = new Point(195, 19);
            txtBuscar.MaxLength = 10;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(185, 21);
            txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(92, 58, 33);
            btnBuscar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.ForeColor = Color.FromArgb(245, 232, 208);
            btnBuscar.Location = new Point(390, 16);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(92, 28);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // lblPendientes
            // 
            lblPendientes.AutoSize = true;
            lblPendientes.Font = new Font("Georgia", 9F, FontStyle.Bold);
            lblPendientes.Location = new Point(496, 22);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.Size = new Size(138, 15);
            lblPendientes.TabIndex = 3;
            lblPendientes.Text = "Pendientes Check-In:";
            // 
            // cmbPendientesCheckIn
            // 
            cmbPendientesCheckIn.BackColor = Color.FromArgb(250, 242, 226);
            cmbPendientesCheckIn.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPendientesCheckIn.Location = new Point(642, 18);
            cmbPendientesCheckIn.Name = "cmbPendientesCheckIn";
            cmbPendientesCheckIn.Size = new Size(258, 23);
            cmbPendientesCheckIn.TabIndex = 4;
            // 
            // gbReservacion
            // 
            gbReservacion.BackColor = Color.FromArgb(224, 204, 170);
            gbReservacion.Controls.Add(lblEstado);
            gbReservacion.Controls.Add(lblSalida);
            gbReservacion.Controls.Add(lblEntrada);
            gbReservacion.Controls.Add(lblHabitacion);
            gbReservacion.Controls.Add(lblCliente);
            gbReservacion.Controls.Add(lblCodigo);
            gbReservacion.Controls.Add(label7);
            gbReservacion.Controls.Add(label6);
            gbReservacion.Controls.Add(label5);
            gbReservacion.Controls.Add(label4);
            gbReservacion.Controls.Add(label3);
            gbReservacion.Controls.Add(label2);
            gbReservacion.Location = new Point(20, 56);
            gbReservacion.Name = "gbReservacion";
            gbReservacion.Size = new Size(410, 200);
            gbReservacion.TabIndex = 3;
            gbReservacion.TabStop = false;
            gbReservacion.Text = "Reservación";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 30);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 0;
            label2.Text = "Código:";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(120, 30);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(11, 15);
            lblCodigo.TabIndex = 1;
            lblCodigo.Text = "-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 58);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 2;
            label3.Text = "Cliente:";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(120, 58);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(11, 15);
            lblCliente.TabIndex = 3;
            lblCliente.Text = "-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 86);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 4;
            label4.Text = "Habitación:";
            // 
            // lblHabitacion
            // 
            lblHabitacion.AutoSize = true;
            lblHabitacion.Location = new Point(120, 86);
            lblHabitacion.Name = "lblHabitacion";
            lblHabitacion.Size = new Size(11, 15);
            lblHabitacion.TabIndex = 5;
            lblHabitacion.Text = "-";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 114);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 6;
            label5.Text = "Entrada:";
            // 
            // lblEntrada
            // 
            lblEntrada.AutoSize = true;
            lblEntrada.Location = new Point(120, 114);
            lblEntrada.Name = "lblEntrada";
            lblEntrada.Size = new Size(11, 15);
            lblEntrada.TabIndex = 7;
            lblEntrada.Text = "-";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 142);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 8;
            label6.Text = "Salida:";
            // 
            // lblSalida
            // 
            lblSalida.AutoSize = true;
            lblSalida.Location = new Point(120, 142);
            lblSalida.Name = "lblSalida";
            lblSalida.Size = new Size(11, 15);
            lblSalida.TabIndex = 9;
            lblSalida.Text = "-";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(18, 170);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 10;
            label7.Text = "Estado:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(120, 170);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(11, 15);
            lblEstado.TabIndex = 11;
            lblEstado.Text = "-";
            // 
            // gbCheckIn
            // 
            gbCheckIn.BackColor = Color.FromArgb(224, 204, 170);
            gbCheckIn.Controls.Add(btnCheckIn);
            gbCheckIn.Controls.Add(txtLlave);
            gbCheckIn.Controls.Add(label9);
            gbCheckIn.Controls.Add(txtAcompanantes);
            gbCheckIn.Controls.Add(label8);
            gbCheckIn.Location = new Point(20, 268);
            gbCheckIn.Name = "gbCheckIn";
            gbCheckIn.Size = new Size(410, 200);
            gbCheckIn.TabIndex = 4;
            gbCheckIn.TabStop = false;
            gbCheckIn.Text = "Check-In";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(18, 28);
            label8.Name = "label8";
            label8.Size = new Size(92, 15);
            label8.TabIndex = 0;
            label8.Text = "Acompañantes:";
            // 
            // txtAcompanantes
            // 
            txtAcompanantes.BackColor = Color.FromArgb(250, 242, 226);
            txtAcompanantes.Location = new Point(18, 48);
            txtAcompanantes.Multiline = true;
            txtAcompanantes.Name = "txtAcompanantes";
            txtAcompanantes.Size = new Size(372, 54);
            txtAcompanantes.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(18, 116);
            label9.Name = "label9";
            label9.Size = new Size(99, 15);
            label9.TabIndex = 2;
            label9.Text = "Llave magnética:";
            // 
            // txtLlave
            // 
            txtLlave.BackColor = Color.FromArgb(250, 242, 226);
            txtLlave.Location = new Point(126, 113);
            txtLlave.MaxLength = 8;
            txtLlave.Name = "txtLlave";
            txtLlave.Size = new Size(130, 21);
            txtLlave.TabIndex = 3;
            // 
            // btnCheckIn
            // 
            btnCheckIn.BackColor = Color.FromArgb(58, 102, 52);
            btnCheckIn.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnCheckIn.FlatStyle = FlatStyle.Flat;
            btnCheckIn.ForeColor = Color.FromArgb(245, 232, 208);
            btnCheckIn.Location = new Point(18, 152);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(160, 32);
            btnCheckIn.TabIndex = 4;
            btnCheckIn.Text = "Confirmar Check-In";
            btnCheckIn.UseVisualStyleBackColor = false;
            // 
            // gbCuenta
            // 
            gbCuenta.BackColor = Color.FromArgb(224, 204, 170);
            gbCuenta.Controls.Add(lblSaldo);
            gbCuenta.Controls.Add(label13);
            gbCuenta.Controls.Add(lblPagado);
            gbCuenta.Controls.Add(label12);
            gbCuenta.Controls.Add(lblConsumos);
            gbCuenta.Controls.Add(label11);
            gbCuenta.Controls.Add(lblHospedaje);
            gbCuenta.Controls.Add(label10);
            gbCuenta.Controls.Add(dgvConsumos);
            gbCuenta.Location = new Point(448, 56);
            gbCuenta.Name = "gbCuenta";
            gbCuenta.Size = new Size(452, 250);
            gbCuenta.TabIndex = 5;
            gbCuenta.TabStop = false;
            gbCuenta.Text = "Cuenta y Consumos";
            // 
            // dgvConsumos
            // 
            dgvConsumos.AllowUserToAddRows = false;
            dgvConsumos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConsumos.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvConsumos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvConsumos.Columns.AddRange(new DataGridViewColumn[] { colFecha, colConcepto, colMonto });
            dgvConsumos.GridColor = Color.FromArgb(140, 105, 70);
            dgvConsumos.Location = new Point(16, 26);
            dgvConsumos.Name = "dgvConsumos";
            dgvConsumos.ReadOnly = true;
            dgvConsumos.RowHeadersVisible = false;
            dgvConsumos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsumos.Size = new Size(420, 150);
            dgvConsumos.TabIndex = 0;
            // 
            // colFecha
            // 
            colFecha.HeaderText = "Fecha";
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // colConcepto
            // 
            colConcepto.HeaderText = "Concepto";
            colConcepto.Name = "colConcepto";
            colConcepto.ReadOnly = true;
            // 
            // colMonto
            // 
            colMonto.HeaderText = "Monto";
            colMonto.Name = "colMonto";
            colMonto.ReadOnly = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(16, 188);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 1;
            label10.Text = "Hospedaje:";
            // 
            // lblHospedaje
            // 
            lblHospedaje.AutoSize = true;
            lblHospedaje.Location = new Point(92, 188);
            lblHospedaje.Name = "lblHospedaje";
            lblHospedaje.Size = new Size(31, 15);
            lblHospedaje.TabIndex = 2;
            lblHospedaje.Text = "0.00";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(236, 188);
            label11.Name = "label11";
            label11.Size = new Size(69, 15);
            label11.TabIndex = 3;
            label11.Text = "Consumos:";
            // 
            // lblConsumos
            // 
            lblConsumos.AutoSize = true;
            lblConsumos.Location = new Point(316, 188);
            lblConsumos.Name = "lblConsumos";
            lblConsumos.Size = new Size(31, 15);
            lblConsumos.TabIndex = 4;
            lblConsumos.Text = "0.00";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(16, 216);
            label12.Name = "label12";
            label12.Size = new Size(51, 15);
            label12.TabIndex = 5;
            label12.Text = "Pagado:";
            // 
            // lblPagado
            // 
            lblPagado.AutoSize = true;
            lblPagado.Location = new Point(92, 216);
            lblPagado.Name = "lblPagado";
            lblPagado.Size = new Size(31, 15);
            lblPagado.TabIndex = 6;
            lblPagado.Text = "0.00";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            label13.Location = new Point(236, 216);
            label13.Name = "label13";
            label13.Size = new Size(49, 16);
            label13.TabIndex = 7;
            label13.Text = "Saldo:";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Font = new Font("Georgia", 9.75F, FontStyle.Bold);
            lblSaldo.Location = new Point(316, 216);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(36, 16);
            lblSaldo.TabIndex = 8;
            lblSaldo.Text = "0.00";
            // 
            // gbPago
            // 
            gbPago.BackColor = Color.FromArgb(224, 204, 170);
            gbPago.Controls.Add(btnRegistrarPago);
            gbPago.Controls.Add(txtReferencia);
            gbPago.Controls.Add(label16);
            gbPago.Controls.Add(nudMontoPago);
            gbPago.Controls.Add(label15);
            gbPago.Controls.Add(cmbMetodoPago);
            gbPago.Controls.Add(label14);
            gbPago.Location = new Point(448, 316);
            gbPago.Name = "gbPago";
            gbPago.Size = new Size(452, 152);
            gbPago.TabIndex = 6;
            gbPago.TabStop = false;
            gbPago.Text = "Pago";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(16, 32);
            label14.Name = "label14";
            label14.Size = new Size(51, 15);
            label14.TabIndex = 0;
            label14.Text = "Método:";
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.BackColor = Color.FromArgb(250, 242, 226);
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta", "Transferencia" });
            cmbMetodoPago.Location = new Point(88, 28);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(150, 23);
            cmbMetodoPago.TabIndex = 1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(258, 32);
            label15.Name = "label15";
            label15.Size = new Size(45, 15);
            label15.TabIndex = 2;
            label15.Text = "Monto:";
            // 
            // nudMontoPago
            // 
            nudMontoPago.BackColor = Color.FromArgb(250, 242, 226);
            nudMontoPago.DecimalPlaces = 2;
            nudMontoPago.Location = new Point(312, 29);
            nudMontoPago.Maximum = new decimal(new int[] { 50000000, 0, 0, 0 });
            nudMontoPago.Name = "nudMontoPago";
            nudMontoPago.Size = new Size(124, 21);
            nudMontoPago.TabIndex = 3;
            nudMontoPago.ThousandsSeparator = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(16, 72);
            label16.Name = "label16";
            label16.Size = new Size(68, 15);
            label16.TabIndex = 4;
            label16.Text = "Referencia:";
            // 
            // txtReferencia
            // 
            txtReferencia.BackColor = Color.FromArgb(250, 242, 226);
            txtReferencia.Location = new Point(88, 69);
            txtReferencia.Name = "txtReferencia";
            txtReferencia.Size = new Size(348, 21);
            txtReferencia.TabIndex = 5;
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.BackColor = Color.FromArgb(92, 58, 33);
            btnRegistrarPago.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnRegistrarPago.FlatStyle = FlatStyle.Flat;
            btnRegistrarPago.ForeColor = Color.FromArgb(245, 232, 208);
            btnRegistrarPago.Location = new Point(16, 106);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(140, 32);
            btnRegistrarPago.TabIndex = 6;
            btnRegistrarPago.Text = "Registrar Pago";
            btnRegistrarPago.UseVisualStyleBackColor = false;
            // 
            // btnCheckOut
            // 
            btnCheckOut.BackColor = Color.FromArgb(138, 44, 36);
            btnCheckOut.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnCheckOut.FlatStyle = FlatStyle.Flat;
            btnCheckOut.ForeColor = Color.FromArgb(245, 232, 208);
            btnCheckOut.Location = new Point(448, 484);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(170, 34);
            btnCheckOut.TabIndex = 7;
            btnCheckOut.Text = "Confirmar Check-Out";
            btnCheckOut.UseVisualStyleBackColor = false;
            // 
            // btnFactura
            // 
            btnFactura.BackColor = Color.FromArgb(92, 58, 33);
            btnFactura.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnFactura.FlatStyle = FlatStyle.Flat;
            btnFactura.ForeColor = Color.FromArgb(245, 232, 208);
            btnFactura.Location = new Point(632, 484);
            btnFactura.Name = "btnFactura";
            btnFactura.Size = new Size(150, 34);
            btnFactura.TabIndex = 8;
            btnFactura.Text = "Emitir Factura";
            btnFactura.UseVisualStyleBackColor = false;
            // 
            // FrmCheckInOut
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(920, 536);
            Controls.Add(cmbPendientesCheckIn);
            Controls.Add(lblPendientes);
            Controls.Add(btnFactura);
            Controls.Add(btnCheckOut);
            Controls.Add(gbPago);
            Controls.Add(gbCuenta);
            Controls.Add(gbCheckIn);
            Controls.Add(gbReservacion);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmCheckInOut";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Check-In y Check-Out";
            gbReservacion.ResumeLayout(false);
            gbReservacion.PerformLayout();
            gbCheckIn.ResumeLayout(false);
            gbCheckIn.PerformLayout();
            gbCuenta.ResumeLayout(false);
            gbCuenta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConsumos).EndInit();
            gbPago.ResumeLayout(false);
            gbPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMontoPago).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label lblPendientes;
        private ComboBox cmbPendientesCheckIn;
        private GroupBox gbReservacion;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblCodigo;
        private Label lblCliente;
        private Label lblHabitacion;
        private Label lblEntrada;
        private Label lblSalida;
        private Label lblEstado;
        private GroupBox gbCheckIn;
        private Label label8;
        private TextBox txtAcompanantes;
        private Label label9;
        private TextBox txtLlave;
        private Button btnCheckIn;
        private GroupBox gbCuenta;
        private DataGridView dgvConsumos;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colConcepto;
        private DataGridViewTextBoxColumn colMonto;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label lblHospedaje;
        private Label lblConsumos;
        private Label lblPagado;
        private Label lblSaldo;
        private GroupBox gbPago;
        private Label label14;
        private ComboBox cmbMetodoPago;
        private Label label15;
        private NumericUpDown nudMontoPago;
        private Label label16;
        private TextBox txtReferencia;
        private Button btnRegistrarPago;
        private Button btnCheckOut;
        private Button btnFactura;
    }
}
