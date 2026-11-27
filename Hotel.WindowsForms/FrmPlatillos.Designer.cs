namespace Hotel.WindowsForms
{
    partial class FrmPlatillos
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
            tabCatalogo = new TabControl();
            tabPlatillos = new TabPage();
            gbPlatillo = new GroupBox();
            cmbNombrePlatillo = new ComboBox();
            cmbCategoria = new ComboBox();
            txtDescripcionPlatillo = new TextBox();
            nudPrecio = new NumericUpDown();
            nudTiempo = new NumericUpDown();
            chkDisponible = new CheckBox();
            chkAgregarRefresco = new CheckBox();
            cmbRefrescoPlatillo = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnNuevoPlatillo = new Button();
            btnGuardarPlatillo = new Button();
            btnModificarPlatillo = new Button();
            btnDesactivarPlatillo = new Button();
            dgvPlatillos = new DataGridView();
            colPlatId = new DataGridViewTextBoxColumn();
            colPlatCategoria = new DataGridViewTextBoxColumn();
            colPlatNombre = new DataGridViewTextBoxColumn();
            colPlatPrecio = new DataGridViewTextBoxColumn();
            colPlatTiempo = new DataGridViewTextBoxColumn();
            colPlatDisponible = new DataGridViewTextBoxColumn();
            tabCategorias = new TabPage();
            gbCategoria = new GroupBox();
            txtNombreCategoria = new TextBox();
            txtDescripcionCategoria = new TextBox();
            chkCategoriaActiva = new CheckBox();
            label6 = new Label();
            label7 = new Label();
            btnNuevaCategoria = new Button();
            btnGuardarCategoria = new Button();
            btnModificarCategoria = new Button();
            dgvCategorias = new DataGridView();
            colCatId = new DataGridViewTextBoxColumn();
            colCatNombre = new DataGridViewTextBoxColumn();
            colCatDescripcion = new DataGridViewTextBoxColumn();
            colCatActivo = new DataGridViewTextBoxColumn();
            tabCatalogo.SuspendLayout();
            tabPlatillos.SuspendLayout();
            gbPlatillo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTiempo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPlatillos).BeginInit();
            tabCategorias.SuspendLayout();
            gbCategoria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // tabCatalogo
            // 
            tabCatalogo.Controls.Add(tabPlatillos);
            tabCatalogo.Controls.Add(tabCategorias);
            tabCatalogo.Location = new Point(16, 16);
            tabCatalogo.Name = "tabCatalogo";
            tabCatalogo.SelectedIndex = 0;
            tabCatalogo.Size = new Size(828, 498);
            tabCatalogo.TabIndex = 0;
            // 
            // tabPlatillos
            // 
            tabPlatillos.BackColor = Color.FromArgb(236, 220, 192);
            tabPlatillos.Controls.Add(dgvPlatillos);
            tabPlatillos.Controls.Add(btnDesactivarPlatillo);
            tabPlatillos.Controls.Add(btnModificarPlatillo);
            tabPlatillos.Controls.Add(btnGuardarPlatillo);
            tabPlatillos.Controls.Add(btnNuevoPlatillo);
            tabPlatillos.Controls.Add(gbPlatillo);
            tabPlatillos.Location = new Point(4, 24);
            tabPlatillos.Name = "tabPlatillos";
            tabPlatillos.Size = new Size(820, 470);
            tabPlatillos.TabIndex = 0;
            tabPlatillos.Text = "Platillos";
            // 
            // gbPlatillo
            // 
            gbPlatillo.BackColor = Color.FromArgb(224, 204, 170);
            gbPlatillo.Controls.Add(cmbRefrescoPlatillo);
            gbPlatillo.Controls.Add(chkAgregarRefresco);
            gbPlatillo.Controls.Add(chkDisponible);
            gbPlatillo.Controls.Add(nudTiempo);
            gbPlatillo.Controls.Add(label5);
            gbPlatillo.Controls.Add(nudPrecio);
            gbPlatillo.Controls.Add(label4);
            gbPlatillo.Controls.Add(txtDescripcionPlatillo);
            gbPlatillo.Controls.Add(label3);
            gbPlatillo.Controls.Add(cmbCategoria);
            gbPlatillo.Controls.Add(label2);
            gbPlatillo.Controls.Add(cmbNombrePlatillo);
            gbPlatillo.Controls.Add(label1);
            gbPlatillo.Location = new Point(18, 12);
            gbPlatillo.Name = "gbPlatillo";
            gbPlatillo.Size = new Size(784, 134);
            gbPlatillo.TabIndex = 0;
            gbPlatillo.TabStop = false;
            gbPlatillo.Text = "Catálogo de Comidas y Bebidas (Precios Fijos y Agregar Nuevos)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 30);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Platillo:";
            // 
            // cmbNombrePlatillo
            // 
            cmbNombrePlatillo.BackColor = Color.FromArgb(250, 242, 226);
            cmbNombrePlatillo.DropDownStyle = ComboBoxStyle.DropDown;
            cmbNombrePlatillo.Location = new Point(78, 26);
            cmbNombrePlatillo.Name = "cmbNombrePlatillo";
            cmbNombrePlatillo.Size = new Size(225, 23);
            cmbNombrePlatillo.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(316, 30);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 2;
            label2.Text = "Categoría:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.BackColor = Color.FromArgb(250, 242, 226);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(388, 26);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(165, 23);
            cmbCategoria.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(564, 30);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 4;
            label4.Text = "Precio ₡:";
            // 
            // nudPrecio
            // 
            nudPrecio.BackColor = Color.FromArgb(250, 242, 226);
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Location = new Point(622, 27);
            nudPrecio.Maximum = new decimal(new int[] { 5000000, 0, 0, 0 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(115, 21);
            nudPrecio.TabIndex = 5;
            nudPrecio.ThousandsSeparator = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 66);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 6;
            label3.Text = "Descripción:";
            // 
            // txtDescripcionPlatillo
            // 
            txtDescripcionPlatillo.BackColor = Color.FromArgb(250, 242, 226);
            txtDescripcionPlatillo.Location = new Point(100, 63);
            txtDescripcionPlatillo.Name = "txtDescripcionPlatillo";
            txtDescripcionPlatillo.Size = new Size(340, 21);
            txtDescripcionPlatillo.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(454, 66);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 8;
            label5.Text = "Minutos prep.:";
            // 
            // nudTiempo
            // 
            nudTiempo.BackColor = Color.FromArgb(250, 242, 226);
            nudTiempo.Location = new Point(548, 63);
            nudTiempo.Maximum = new decimal(new int[] { 240, 0, 0, 0 });
            nudTiempo.Name = "nudTiempo";
            nudTiempo.Size = new Size(64, 21);
            nudTiempo.TabIndex = 9;
            nudTiempo.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // chkDisponible
            // 
            chkDisponible.AutoSize = true;
            chkDisponible.Checked = true;
            chkDisponible.CheckState = CheckState.Checked;
            chkDisponible.Location = new Point(628, 65);
            chkDisponible.Name = "chkDisponible";
            chkDisponible.Size = new Size(87, 19);
            chkDisponible.TabIndex = 10;
            chkDisponible.Text = "Disponible";
            // 
            // chkAgregarRefresco
            // 
            chkAgregarRefresco.AutoSize = true;
            chkAgregarRefresco.Font = new Font("Georgia", 9F, FontStyle.Bold);
            chkAgregarRefresco.Location = new Point(18, 100);
            chkAgregarRefresco.Name = "chkAgregarRefresco";
            chkAgregarRefresco.Size = new Size(148, 19);
            chkAgregarRefresco.TabIndex = 11;
            chkAgregarRefresco.Text = "¿Agregar refresco?";
            chkAgregarRefresco.Visible = false;
            // 
            // cmbRefrescoPlatillo
            // 
            cmbRefrescoPlatillo.BackColor = Color.FromArgb(250, 242, 226);
            cmbRefrescoPlatillo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRefrescoPlatillo.Enabled = false;
            cmbRefrescoPlatillo.Location = new Point(176, 97);
            cmbRefrescoPlatillo.Name = "cmbRefrescoPlatillo";
            cmbRefrescoPlatillo.Size = new Size(320, 23);
            cmbRefrescoPlatillo.TabIndex = 12;
            cmbRefrescoPlatillo.Visible = false;
            // 
            // btnNuevoPlatillo
            // 
            btnNuevoPlatillo.BackColor = Color.FromArgb(92, 58, 33);
            btnNuevoPlatillo.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnNuevoPlatillo.FlatStyle = FlatStyle.Flat;
            btnNuevoPlatillo.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevoPlatillo.Location = new Point(18, 150);
            btnNuevoPlatillo.Name = "btnNuevoPlatillo";
            btnNuevoPlatillo.Size = new Size(95, 30);
            btnNuevoPlatillo.TabIndex = 1;
            btnNuevoPlatillo.Text = "Nuevo";
            btnNuevoPlatillo.UseVisualStyleBackColor = false;
            // 
            // btnGuardarPlatillo
            // 
            btnGuardarPlatillo.BackColor = Color.FromArgb(58, 102, 52);
            btnGuardarPlatillo.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnGuardarPlatillo.FlatStyle = FlatStyle.Flat;
            btnGuardarPlatillo.ForeColor = Color.FromArgb(245, 232, 208);
            btnGuardarPlatillo.Location = new Point(122, 150);
            btnGuardarPlatillo.Name = "btnGuardarPlatillo";
            btnGuardarPlatillo.Size = new Size(95, 30);
            btnGuardarPlatillo.TabIndex = 2;
            btnGuardarPlatillo.Text = "Guardar";
            btnGuardarPlatillo.UseVisualStyleBackColor = false;
            // 
            // btnModificarPlatillo
            // 
            btnModificarPlatillo.BackColor = Color.FromArgb(92, 58, 33);
            btnModificarPlatillo.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnModificarPlatillo.FlatStyle = FlatStyle.Flat;
            btnModificarPlatillo.ForeColor = Color.FromArgb(245, 232, 208);
            btnModificarPlatillo.Location = new Point(226, 150);
            btnModificarPlatillo.Name = "btnModificarPlatillo";
            btnModificarPlatillo.Size = new Size(95, 30);
            btnModificarPlatillo.TabIndex = 3;
            btnModificarPlatillo.Text = "Modificar";
            btnModificarPlatillo.UseVisualStyleBackColor = false;
            // 
            // btnDesactivarPlatillo
            // 
            btnDesactivarPlatillo.BackColor = Color.FromArgb(138, 44, 36);
            btnDesactivarPlatillo.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnDesactivarPlatillo.FlatStyle = FlatStyle.Flat;
            btnDesactivarPlatillo.ForeColor = Color.FromArgb(245, 232, 208);
            btnDesactivarPlatillo.Location = new Point(330, 150);
            btnDesactivarPlatillo.Name = "btnDesactivarPlatillo";
            btnDesactivarPlatillo.Size = new Size(95, 30);
            btnDesactivarPlatillo.TabIndex = 4;
            btnDesactivarPlatillo.Text = "Eliminar";
            btnDesactivarPlatillo.UseVisualStyleBackColor = false;
            // 
            // dgvPlatillos
            // 
            dgvPlatillos.AllowUserToAddRows = false;
            dgvPlatillos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPlatillos.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvPlatillos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvPlatillos.Columns.AddRange(new DataGridViewColumn[] { colPlatId, colPlatCategoria, colPlatNombre, colPlatPrecio, colPlatTiempo, colPlatDisponible });
            dgvPlatillos.GridColor = Color.FromArgb(140, 105, 70);
            dgvPlatillos.Location = new Point(18, 194);
            dgvPlatillos.Name = "dgvPlatillos";
            dgvPlatillos.ReadOnly = true;
            dgvPlatillos.RowHeadersVisible = false;
            dgvPlatillos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlatillos.Size = new Size(784, 258);
            dgvPlatillos.TabIndex = 5;
            // 
            // colPlatId
            // 
            colPlatId.HeaderText = "Id";
            colPlatId.Name = "colPlatId";
            colPlatId.ReadOnly = true;
            // 
            // colPlatCategoria
            // 
            colPlatCategoria.HeaderText = "Categoría";
            colPlatCategoria.Name = "colPlatCategoria";
            colPlatCategoria.ReadOnly = true;
            // 
            // colPlatNombre
            // 
            colPlatNombre.HeaderText = "Nombre";
            colPlatNombre.Name = "colPlatNombre";
            colPlatNombre.ReadOnly = true;
            // 
            // colPlatPrecio
            // 
            colPlatPrecio.HeaderText = "Precio Fijo";
            colPlatPrecio.Name = "colPlatPrecio";
            colPlatPrecio.ReadOnly = true;
            // 
            // colPlatTiempo
            // 
            colPlatTiempo.HeaderText = "Minutos";
            colPlatTiempo.Name = "colPlatTiempo";
            colPlatTiempo.ReadOnly = true;
            // 
            // colPlatDisponible
            // 
            colPlatDisponible.HeaderText = "Disponible";
            colPlatDisponible.Name = "colPlatDisponible";
            colPlatDisponible.ReadOnly = true;
            // 
            // tabCategorias
            // 
            tabCategorias.BackColor = Color.FromArgb(236, 220, 192);
            tabCategorias.Controls.Add(dgvCategorias);
            tabCategorias.Controls.Add(btnModificarCategoria);
            tabCategorias.Controls.Add(btnGuardarCategoria);
            tabCategorias.Controls.Add(btnNuevaCategoria);
            tabCategorias.Controls.Add(gbCategoria);
            tabCategorias.Location = new Point(4, 24);
            tabCategorias.Name = "tabCategorias";
            tabCategorias.Size = new Size(820, 470);
            tabCategorias.TabIndex = 1;
            tabCategorias.Text = "Categorías";
            // 
            // gbCategoria
            // 
            gbCategoria.BackColor = Color.FromArgb(224, 204, 170);
            gbCategoria.Controls.Add(chkCategoriaActiva);
            gbCategoria.Controls.Add(txtDescripcionCategoria);
            gbCategoria.Controls.Add(label7);
            gbCategoria.Controls.Add(txtNombreCategoria);
            gbCategoria.Controls.Add(label6);
            gbCategoria.Location = new Point(18, 16);
            gbCategoria.Name = "gbCategoria";
            gbCategoria.Size = new Size(784, 110);
            gbCategoria.TabIndex = 0;
            gbCategoria.TabStop = false;
            gbCategoria.Text = "Categoría";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 32);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 0;
            label6.Text = "Nombre:";
            // 
            // txtNombreCategoria
            // 
            txtNombreCategoria.BackColor = Color.FromArgb(250, 242, 226);
            txtNombreCategoria.Location = new Point(100, 29);
            txtNombreCategoria.Name = "txtNombreCategoria";
            txtNombreCategoria.Size = new Size(220, 21);
            txtNombreCategoria.TabIndex = 1;
            // 
            // chkCategoriaActiva
            // 
            chkCategoriaActiva.AutoSize = true;
            chkCategoriaActiva.Checked = true;
            chkCategoriaActiva.CheckState = CheckState.Checked;
            chkCategoriaActiva.Location = new Point(352, 31);
            chkCategoriaActiva.Name = "chkCategoriaActiva";
            chkCategoriaActiva.Size = new Size(62, 19);
            chkCategoriaActiva.TabIndex = 2;
            chkCategoriaActiva.Text = "Activa";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(18, 70);
            label7.Name = "label7";
            label7.Size = new Size(76, 15);
            label7.TabIndex = 3;
            label7.Text = "Descripción:";
            // 
            // txtDescripcionCategoria
            // 
            txtDescripcionCategoria.BackColor = Color.FromArgb(250, 242, 226);
            txtDescripcionCategoria.Location = new Point(100, 67);
            txtDescripcionCategoria.Name = "txtDescripcionCategoria";
            txtDescripcionCategoria.Size = new Size(650, 21);
            txtDescripcionCategoria.TabIndex = 4;
            // 
            // btnNuevaCategoria
            // 
            btnNuevaCategoria.BackColor = Color.FromArgb(92, 58, 33);
            btnNuevaCategoria.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnNuevaCategoria.FlatStyle = FlatStyle.Flat;
            btnNuevaCategoria.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevaCategoria.Location = new Point(18, 138);
            btnNuevaCategoria.Name = "btnNuevaCategoria";
            btnNuevaCategoria.Size = new Size(95, 30);
            btnNuevaCategoria.TabIndex = 1;
            btnNuevaCategoria.Text = "Nuevo";
            btnNuevaCategoria.UseVisualStyleBackColor = false;
            // 
            // btnGuardarCategoria
            // 
            btnGuardarCategoria.BackColor = Color.FromArgb(58, 102, 52);
            btnGuardarCategoria.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnGuardarCategoria.FlatStyle = FlatStyle.Flat;
            btnGuardarCategoria.ForeColor = Color.FromArgb(245, 232, 208);
            btnGuardarCategoria.Location = new Point(122, 138);
            btnGuardarCategoria.Name = "btnGuardarCategoria";
            btnGuardarCategoria.Size = new Size(95, 30);
            btnGuardarCategoria.TabIndex = 2;
            btnGuardarCategoria.Text = "Guardar";
            btnGuardarCategoria.UseVisualStyleBackColor = false;
            // 
            // btnModificarCategoria
            // 
            btnModificarCategoria.BackColor = Color.FromArgb(92, 58, 33);
            btnModificarCategoria.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnModificarCategoria.FlatStyle = FlatStyle.Flat;
            btnModificarCategoria.ForeColor = Color.FromArgb(245, 232, 208);
            btnModificarCategoria.Location = new Point(226, 138);
            btnModificarCategoria.Name = "btnModificarCategoria";
            btnModificarCategoria.Size = new Size(95, 30);
            btnModificarCategoria.TabIndex = 3;
            btnModificarCategoria.Text = "Modificar";
            btnModificarCategoria.UseVisualStyleBackColor = false;
            // 
            // dgvCategorias
            // 
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategorias.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvCategorias.Columns.AddRange(new DataGridViewColumn[] { colCatId, colCatNombre, colCatDescripcion, colCatActivo });
            dgvCategorias.GridColor = Color.FromArgb(140, 105, 70);
            dgvCategorias.Location = new Point(18, 182);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.ReadOnly = true;
            dgvCategorias.RowHeadersVisible = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.Size = new Size(784, 270);
            dgvCategorias.TabIndex = 4;
            // 
            // colCatId
            // 
            colCatId.HeaderText = "Id";
            colCatId.Name = "colCatId";
            colCatId.ReadOnly = true;
            // 
            // colCatNombre
            // 
            colCatNombre.HeaderText = "Nombre";
            colCatNombre.Name = "colCatNombre";
            colCatNombre.ReadOnly = true;
            // 
            // colCatDescripcion
            // 
            colCatDescripcion.HeaderText = "Descripción";
            colCatDescripcion.Name = "colCatDescripcion";
            colCatDescripcion.ReadOnly = true;
            // 
            // colCatActivo
            // 
            colCatActivo.HeaderText = "Activo";
            colCatActivo.Name = "colCatActivo";
            colCatActivo.ReadOnly = true;
            // 
            // FrmPlatillos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(860, 530);
            Controls.Add(tabCatalogo);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmPlatillos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Catálogo de Platillos";
            tabCatalogo.ResumeLayout(false);
            tabPlatillos.ResumeLayout(false);
            gbPlatillo.ResumeLayout(false);
            gbPlatillo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTiempo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPlatillos).EndInit();
            tabCategorias.ResumeLayout(false);
            gbCategoria.ResumeLayout(false);
            gbCategoria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabCatalogo;
        private TabPage tabPlatillos;
        private GroupBox gbPlatillo;
        private ComboBox cmbNombrePlatillo;
        private ComboBox cmbCategoria;
        private TextBox txtDescripcionPlatillo;
        private NumericUpDown nudPrecio;
        private NumericUpDown nudTiempo;
        private CheckBox chkDisponible;
        private CheckBox chkAgregarRefresco;
        private ComboBox cmbRefrescoPlatillo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnNuevoPlatillo;
        private Button btnGuardarPlatillo;
        private Button btnModificarPlatillo;
        private Button btnDesactivarPlatillo;
        private DataGridView dgvPlatillos;
        private DataGridViewTextBoxColumn colPlatId;
        private DataGridViewTextBoxColumn colPlatCategoria;
        private DataGridViewTextBoxColumn colPlatNombre;
        private DataGridViewTextBoxColumn colPlatPrecio;
        private DataGridViewTextBoxColumn colPlatTiempo;
        private DataGridViewTextBoxColumn colPlatDisponible;
        private TabPage tabCategorias;
        private GroupBox gbCategoria;
        private TextBox txtNombreCategoria;
        private TextBox txtDescripcionCategoria;
        private CheckBox chkCategoriaActiva;
        private Label label6;
        private Label label7;
        private Button btnNuevaCategoria;
        private Button btnGuardarCategoria;
        private Button btnModificarCategoria;
        private DataGridView dgvCategorias;
        private DataGridViewTextBoxColumn colCatId;
        private DataGridViewTextBoxColumn colCatNombre;
        private DataGridViewTextBoxColumn colCatDescripcion;
        private DataGridViewTextBoxColumn colCatActivo;
    }
}
