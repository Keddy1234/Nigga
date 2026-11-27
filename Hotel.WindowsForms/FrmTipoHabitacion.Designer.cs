namespace Hotel.WindowsForms
{
    partial class FrmTipoHabitacion
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
            gbDatos = new GroupBox();
            cmbNombre = new ComboBox();
            cmbAdultos = new ComboBox();
            cmbNinos = new ComboBox();
            txtPrecioAdulto = new TextBox();
            txtPrecioNino = new TextBox();
            nudPrecioBase = new NumericUpDown();
            txtDescripcion = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblPrecioAdulto = new Label();
            lblPrecioNino = new Label();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            dgvTipos = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colAdultos = new DataGridViewTextBoxColumn();
            colNinos = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecioBase).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTipos).BeginInit();
            SuspendLayout();
            // 
            // gbDatos
            // 
            gbDatos.BackColor = Color.FromArgb(224, 204, 170);
            gbDatos.Controls.Add(txtPrecioNino);
            gbDatos.Controls.Add(lblPrecioNino);
            gbDatos.Controls.Add(txtPrecioAdulto);
            gbDatos.Controls.Add(lblPrecioAdulto);
            gbDatos.Controls.Add(nudPrecioBase);
            gbDatos.Controls.Add(label5);
            gbDatos.Controls.Add(cmbNinos);
            gbDatos.Controls.Add(label4);
            gbDatos.Controls.Add(cmbAdultos);
            gbDatos.Controls.Add(label3);
            gbDatos.Controls.Add(txtDescripcion);
            gbDatos.Controls.Add(label2);
            gbDatos.Controls.Add(cmbNombre);
            gbDatos.Controls.Add(label1);
            gbDatos.Location = new Point(20, 14);
            gbDatos.Name = "gbDatos";
            gbDatos.Size = new Size(760, 142);
            gbDatos.TabIndex = 0;
            gbDatos.TabStop = false;
            gbDatos.Text = "Selección de Habitación, Capacidad y Precios";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 30);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Habitación:";
            // 
            // cmbNombre
            // 
            cmbNombre.BackColor = Color.FromArgb(250, 242, 226);
            cmbNombre.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNombre.Location = new Point(94, 26);
            cmbNombre.Name = "cmbNombre";
            cmbNombre.Size = new Size(190, 23);
            cmbNombre.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(300, 30);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 2;
            label3.Text = "Adultos:";
            // 
            // cmbAdultos
            // 
            cmbAdultos.BackColor = Color.FromArgb(250, 242, 226);
            cmbAdultos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAdultos.Location = new Point(358, 26);
            cmbAdultos.Name = "cmbAdultos";
            cmbAdultos.Size = new Size(64, 23);
            cmbAdultos.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(436, 30);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 4;
            label4.Text = "Niños:";
            // 
            // cmbNinos
            // 
            cmbNinos.BackColor = Color.FromArgb(250, 242, 226);
            cmbNinos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNinos.Location = new Point(484, 26);
            cmbNinos.Name = "cmbNinos";
            cmbNinos.Size = new Size(64, 23);
            cmbNinos.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(552, 30);
            label5.Name = "label5";
            label5.Size = new Size(76, 15);
            label5.TabIndex = 6;
            label5.Text = "Total/Noche:";
            // 
            // nudPrecioBase
            // 
            nudPrecioBase.BackColor = Color.FromArgb(250, 242, 226);
            nudPrecioBase.DecimalPlaces = 2;
            nudPrecioBase.Location = new Point(632, 27);
            nudPrecioBase.Maximum = new decimal(new int[] { 50000000, 0, 0, 0 });
            nudPrecioBase.Name = "nudPrecioBase";
            nudPrecioBase.ReadOnly = true;
            nudPrecioBase.Size = new Size(110, 21);
            nudPrecioBase.TabIndex = 7;
            nudPrecioBase.ThousandsSeparator = true;
            // 
            // lblPrecioAdulto
            // 
            lblPrecioAdulto.AutoSize = true;
            lblPrecioAdulto.Location = new Point(16, 68);
            lblPrecioAdulto.Name = "lblPrecioAdulto";
            lblPrecioAdulto.Size = new Size(84, 15);
            lblPrecioAdulto.TabIndex = 8;
            lblPrecioAdulto.Text = "Precio Adulto:";
            // 
            // txtPrecioAdulto
            // 
            txtPrecioAdulto.BackColor = Color.FromArgb(245, 232, 208);
            txtPrecioAdulto.Location = new Point(106, 65);
            txtPrecioAdulto.Name = "txtPrecioAdulto";
            txtPrecioAdulto.ReadOnly = true;
            txtPrecioAdulto.Size = new Size(210, 21);
            txtPrecioAdulto.TabIndex = 9;
            // 
            // lblPrecioNino
            // 
            lblPrecioNino.AutoSize = true;
            lblPrecioNino.Location = new Point(336, 68);
            lblPrecioNino.Name = "lblPrecioNino";
            lblPrecioNino.Size = new Size(74, 15);
            lblPrecioNino.TabIndex = 10;
            lblPrecioNino.Text = "Precio Niño:";
            // 
            // txtPrecioNino
            // 
            txtPrecioNino.BackColor = Color.FromArgb(245, 232, 208);
            txtPrecioNino.Location = new Point(416, 65);
            txtPrecioNino.Name = "txtPrecioNino";
            txtPrecioNino.ReadOnly = true;
            txtPrecioNino.Size = new Size(210, 21);
            txtPrecioNino.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 106);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 12;
            label2.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(245, 232, 208);
            txtDescripcion.Location = new Point(98, 103);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(644, 21);
            txtDescripcion.TabIndex = 13;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(92, 58, 33);
            btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevo.Location = new Point(20, 166);
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
            btnGuardar.Location = new Point(124, 166);
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
            btnModificar.Location = new Point(228, 166);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(95, 30);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(138, 44, 36);
            btnEliminar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.FromArgb(245, 232, 208);
            btnEliminar.Location = new Point(332, 166);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 30);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // dgvTipos
            // 
            dgvTipos.AllowUserToAddRows = false;
            dgvTipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTipos.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvTipos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvTipos.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colAdultos, colNinos, colPrecio });
            dgvTipos.GridColor = Color.FromArgb(140, 105, 70);
            dgvTipos.Location = new Point(20, 208);
            dgvTipos.Name = "dgvTipos";
            dgvTipos.ReadOnly = true;
            dgvTipos.RowHeadersVisible = false;
            dgvTipos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTipos.Size = new Size(760, 258);
            dgvTipos.TabIndex = 5;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.HeaderText = "Habitación";
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colDescripcion
            // 
            colDescripcion.HeaderText = "Descripción";
            colDescripcion.Name = "colDescripcion";
            colDescripcion.ReadOnly = true;
            // 
            // colAdultos
            // 
            colAdultos.HeaderText = "Adultos";
            colAdultos.Name = "colAdultos";
            colAdultos.ReadOnly = true;
            // 
            // colNinos
            // 
            colNinos.HeaderText = "Niños";
            colNinos.Name = "colNinos";
            colNinos.ReadOnly = true;
            // 
            // colPrecio
            // 
            colPrecio.HeaderText = "Precio / Noche";
            colPrecio.Name = "colPrecio";
            colPrecio.ReadOnly = true;
            // 
            // FrmTipoHabitacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(800, 488);
            Controls.Add(dgvTipos);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNuevo);
            Controls.Add(gbDatos);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmTipoHabitacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tipos de Habitación";
            gbDatos.ResumeLayout(false);
            gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecioBase).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTipos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbDatos;
        private ComboBox cmbNombre;
        private ComboBox cmbAdultos;
        private ComboBox cmbNinos;
        private TextBox txtPrecioAdulto;
        private TextBox txtPrecioNino;
        private NumericUpDown nudPrecioBase;
        private TextBox txtDescripcion;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblPrecioAdulto;
        private Label lblPrecioNino;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvTipos;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colAdultos;
        private DataGridViewTextBoxColumn colNinos;
        private DataGridViewTextBoxColumn colPrecio;
    }
}
