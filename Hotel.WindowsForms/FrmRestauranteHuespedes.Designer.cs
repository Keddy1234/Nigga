namespace Hotel.WindowsForms
{
    partial class FrmRestauranteHuespedes
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
            tabRestaurante = new TabControl();
            tabNuevoPedido = new TabPage();
            label1 = new Label();
            cmbHabitacion = new ComboBox();
            rbRoomService = new RadioButton();
            rbMesa = new RadioButton();
            cmbMesa = new ComboBox();
            label2 = new Label();
            dtpHoraDeseada = new DateTimePicker();
            label3 = new Label();
            cmbCategoriaFiltro = new ComboBox();
            lblPlatilloCombo = new Label();
            cmbPlatillo = new ComboBox();
            lblPrecioPlatillo = new Label();
            txtPrecioPlatillo = new TextBox();
            chkRefresco = new CheckBox();
            cmbRefresco = new ComboBox();
            flpMenu = new FlowLayoutPanel();
            label4 = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            btnQuitar = new Button();
            btnNuevoPlatilloMenu = new Button();
            dgvDetalle = new DataGridView();
            colPlatillo = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colSubtotal = new DataGridViewTextBoxColumn();
            label5 = new Label();
            txtInstrucciones = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            lblSubtotal = new Label();
            lblRecargo = new Label();
            lblTotal = new Label();
            btnCargarHabitacion = new Button();
            tabPedidos = new TabPage();
            label9 = new Label();
            dtpFechaFiltro = new DateTimePicker();
            label10 = new Label();
            cmbEstadoFiltro = new ComboBox();
            btnBuscarPedidos = new Button();
            dgvPedidos = new DataGridView();
            colPedId = new DataGridViewTextBoxColumn();
            colPedHabitacion = new DataGridViewTextBoxColumn();
            colPedCliente = new DataGridViewTextBoxColumn();
            colPedFecha = new DataGridViewTextBoxColumn();
            colPedEstado = new DataGridViewTextBoxColumn();
            colPedTotal = new DataGridViewTextBoxColumn();
            cmbNuevoEstado = new ComboBox();
            btnCambiarEstado = new Button();
            btnCancelarPedido = new Button();
            tabRestaurante.SuspendLayout();
            tabNuevoPedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            tabPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            SuspendLayout();
            // 
            // tabRestaurante
            // 
            tabRestaurante.Controls.Add(tabNuevoPedido);
            tabRestaurante.Controls.Add(tabPedidos);
            tabRestaurante.Location = new Point(16, 16);
            tabRestaurante.Name = "tabRestaurante";
            tabRestaurante.SelectedIndex = 0;
            tabRestaurante.Size = new Size(908, 528);
            tabRestaurante.TabIndex = 0;
            // 
            // tabNuevoPedido
            // 
            tabNuevoPedido.BackColor = Color.FromArgb(236, 220, 192);
            tabNuevoPedido.Controls.Add(btnNuevoPlatilloMenu);
            tabNuevoPedido.Controls.Add(cmbRefresco);
            tabNuevoPedido.Controls.Add(chkRefresco);
            tabNuevoPedido.Controls.Add(txtPrecioPlatillo);
            tabNuevoPedido.Controls.Add(lblPrecioPlatillo);
            tabNuevoPedido.Controls.Add(cmbPlatillo);
            tabNuevoPedido.Controls.Add(lblPlatilloCombo);
            tabNuevoPedido.Controls.Add(btnCargarHabitacion);
            tabNuevoPedido.Controls.Add(lblTotal);
            tabNuevoPedido.Controls.Add(label8);
            tabNuevoPedido.Controls.Add(lblRecargo);
            tabNuevoPedido.Controls.Add(label7);
            tabNuevoPedido.Controls.Add(lblSubtotal);
            tabNuevoPedido.Controls.Add(label6);
            tabNuevoPedido.Controls.Add(txtInstrucciones);
            tabNuevoPedido.Controls.Add(label5);
            tabNuevoPedido.Controls.Add(dgvDetalle);
            tabNuevoPedido.Controls.Add(btnQuitar);
            tabNuevoPedido.Controls.Add(btnAgregar);
            tabNuevoPedido.Controls.Add(nudCantidad);
            tabNuevoPedido.Controls.Add(label4);
            tabNuevoPedido.Controls.Add(flpMenu);
            tabNuevoPedido.Controls.Add(cmbCategoriaFiltro);
            tabNuevoPedido.Controls.Add(label3);
            tabNuevoPedido.Controls.Add(dtpHoraDeseada);
            tabNuevoPedido.Controls.Add(label2);
            tabNuevoPedido.Controls.Add(cmbMesa);
            tabNuevoPedido.Controls.Add(rbMesa);
            tabNuevoPedido.Controls.Add(rbRoomService);
            tabNuevoPedido.Controls.Add(cmbHabitacion);
            tabNuevoPedido.Controls.Add(label1);
            tabNuevoPedido.Location = new Point(4, 24);
            tabNuevoPedido.Name = "tabNuevoPedido";
            tabNuevoPedido.Size = new Size(900, 500);
            tabNuevoPedido.TabIndex = 0;
            tabNuevoPedido.Text = "Nuevo Pedido";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 22);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Habitación:";
            // 
            // cmbHabitacion
            // 
            cmbHabitacion.BackColor = Color.FromArgb(250, 242, 226);
            cmbHabitacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHabitacion.Location = new Point(94, 18);
            cmbHabitacion.Name = "cmbHabitacion";
            cmbHabitacion.Size = new Size(190, 23);
            cmbHabitacion.TabIndex = 1;
            // 
            // rbRoomService
            // 
            rbRoomService.AutoSize = true;
            rbRoomService.Checked = true;
            rbRoomService.Location = new Point(296, 20);
            rbRoomService.Name = "rbRoomService";
            rbRoomService.Size = new Size(106, 19);
            rbRoomService.TabIndex = 2;
            rbRoomService.TabStop = true;
            rbRoomService.Text = "Room Service";
            // 
            // rbMesa
            // 
            rbMesa.AutoSize = true;
            rbMesa.Location = new Point(410, 20);
            rbMesa.Name = "rbMesa";
            rbMesa.Size = new Size(60, 19);
            rbMesa.TabIndex = 3;
            rbMesa.Text = "Mesa:";
            // 
            // cmbMesa
            // 
            cmbMesa.BackColor = Color.FromArgb(250, 242, 226);
            cmbMesa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMesa.Enabled = false;
            cmbMesa.Location = new Point(474, 18);
            cmbMesa.Name = "cmbMesa";
            cmbMesa.Size = new Size(112, 23);
            cmbMesa.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(612, 22);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 5;
            label2.Text = "Fecha y hora:";
            // 
            // dtpHoraDeseada
            // 
            dtpHoraDeseada.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpHoraDeseada.Format = DateTimePickerFormat.Custom;
            dtpHoraDeseada.Location = new Point(706, 18);
            dtpHoraDeseada.Name = "dtpHoraDeseada";
            dtpHoraDeseada.Size = new Size(172, 21);
            dtpHoraDeseada.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 56);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 6;
            label3.Text = "Tiempo:";
            // 
            // cmbCategoriaFiltro
            // 
            cmbCategoriaFiltro.BackColor = Color.FromArgb(250, 242, 226);
            cmbCategoriaFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoriaFiltro.Location = new Point(78, 52);
            cmbCategoriaFiltro.Name = "cmbCategoriaFiltro";
            cmbCategoriaFiltro.Size = new Size(155, 23);
            cmbCategoriaFiltro.TabIndex = 7;
            // 
            // lblPlatilloCombo
            // 
            lblPlatilloCombo.AutoSize = true;
            lblPlatilloCombo.Location = new Point(246, 56);
            lblPlatilloCombo.Name = "lblPlatilloCombo";
            lblPlatilloCombo.Size = new Size(56, 15);
            lblPlatilloCombo.TabIndex = 8;
            lblPlatilloCombo.Text = "Comida:";
            // 
            // cmbPlatillo
            // 
            cmbPlatillo.BackColor = Color.FromArgb(250, 242, 226);
            cmbPlatillo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatillo.Location = new Point(308, 52);
            cmbPlatillo.Name = "cmbPlatillo";
            cmbPlatillo.Size = new Size(274, 23);
            cmbPlatillo.TabIndex = 9;
            // 
            // lblPrecioPlatillo
            // 
            lblPrecioPlatillo.AutoSize = true;
            lblPrecioPlatillo.Location = new Point(594, 56);
            lblPrecioPlatillo.Name = "lblPrecioPlatillo";
            lblPrecioPlatillo.Size = new Size(44, 15);
            lblPrecioPlatillo.TabIndex = 10;
            lblPrecioPlatillo.Text = "Precio:";
            // 
            // txtPrecioPlatillo
            // 
            txtPrecioPlatillo.BackColor = Color.FromArgb(245, 232, 208);
            txtPrecioPlatillo.Font = new Font("Georgia", 9F, FontStyle.Bold);
            txtPrecioPlatillo.Location = new Point(644, 53);
            txtPrecioPlatillo.Name = "txtPrecioPlatillo";
            txtPrecioPlatillo.ReadOnly = true;
            txtPrecioPlatillo.Size = new Size(105, 21);
            txtPrecioPlatillo.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(760, 56);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 12;
            label4.Text = "Cant.:";
            // 
            // nudCantidad
            // 
            nudCantidad.BackColor = Color.FromArgb(250, 242, 226);
            nudCantidad.Location = new Point(806, 53);
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(72, 21);
            nudCantidad.TabIndex = 13;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chkRefresco
            // 
            chkRefresco.AutoSize = true;
            chkRefresco.Font = new Font("Georgia", 9F, FontStyle.Bold);
            chkRefresco.Location = new Point(18, 90);
            chkRefresco.Name = "chkRefresco";
            chkRefresco.Size = new Size(155, 19);
            chkRefresco.TabIndex = 14;
            chkRefresco.Text = "Bebidas (Opcional):";
            // 
            // cmbRefresco
            // 
            cmbRefresco.BackColor = Color.FromArgb(250, 242, 226);
            cmbRefresco.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRefresco.Enabled = false;
            cmbRefresco.Location = new Point(176, 86);
            cmbRefresco.Name = "cmbRefresco";
            cmbRefresco.Size = new Size(306, 23);
            cmbRefresco.TabIndex = 15;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(92, 58, 33);
            btnAgregar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.FromArgb(245, 232, 208);
            btnAgregar.Location = new Point(494, 84);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(138, 28);
            btnAgregar.TabIndex = 16;
            btnAgregar.Text = "Agregar Platillo";
            btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnQuitar
            // 
            btnQuitar.BackColor = Color.FromArgb(138, 44, 36);
            btnQuitar.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.ForeColor = Color.FromArgb(245, 232, 208);
            btnQuitar.Location = new Point(640, 84);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(88, 28);
            btnQuitar.TabIndex = 17;
            btnQuitar.Text = "Quitar";
            btnQuitar.UseVisualStyleBackColor = false;
            // 
            // btnNuevoPlatilloMenu
            // 
            btnNuevoPlatilloMenu.BackColor = Color.FromArgb(58, 102, 52);
            btnNuevoPlatilloMenu.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnNuevoPlatilloMenu.FlatStyle = FlatStyle.Flat;
            btnNuevoPlatilloMenu.ForeColor = Color.FromArgb(245, 232, 208);
            btnNuevoPlatilloMenu.Location = new Point(736, 84);
            btnNuevoPlatilloMenu.Name = "btnNuevoPlatilloMenu";
            btnNuevoPlatilloMenu.Size = new Size(142, 28);
            btnNuevoPlatilloMenu.TabIndex = 18;
            btnNuevoPlatilloMenu.Text = "+ Añadir al Menú";
            btnNuevoPlatilloMenu.UseVisualStyleBackColor = false;
            // 
            // flpMenu
            // 
            flpMenu.AutoScroll = true;
            flpMenu.BackColor = Color.FromArgb(224, 204, 170);
            flpMenu.BorderStyle = BorderStyle.FixedSingle;
            flpMenu.Location = new Point(18, 122);
            flpMenu.Name = "flpMenu";
            flpMenu.Size = new Size(460, 358);
            flpMenu.TabIndex = 18;
            // 
            // dgvDetalle
            // 
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { colPlatillo, colCantidad, colPrecio, colSubtotal });
            dgvDetalle.GridColor = Color.FromArgb(140, 105, 70);
            dgvDetalle.Location = new Point(494, 122);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(384, 165);
            dgvDetalle.TabIndex = 19;
            // 
            // colPlatillo
            // 
            colPlatillo.HeaderText = "Platillo";
            colPlatillo.Name = "colPlatillo";
            colPlatillo.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.HeaderText = "Cant.";
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // colPrecio
            // 
            colPrecio.HeaderText = "Precio";
            colPrecio.Name = "colPrecio";
            colPrecio.ReadOnly = true;
            // 
            // colSubtotal
            // 
            colSubtotal.HeaderText = "Subtotal";
            colSubtotal.Name = "colSubtotal";
            colSubtotal.ReadOnly = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(494, 294);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 14;
            label5.Text = "Instrucciones:";
            // 
            // txtInstrucciones
            // 
            txtInstrucciones.BackColor = Color.FromArgb(250, 242, 226);
            txtInstrucciones.Location = new Point(494, 314);
            txtInstrucciones.Multiline = true;
            txtInstrucciones.Name = "txtInstrucciones";
            txtInstrucciones.Size = new Size(384, 52);
            txtInstrucciones.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(494, 380);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 16;
            label6.Text = "Subtotal:";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(620, 380);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(31, 15);
            lblSubtotal.TabIndex = 17;
            lblSubtotal.Text = "0.00";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(494, 404);
            label7.Name = "label7";
            label7.Size = new Size(103, 15);
            label7.TabIndex = 18;
            label7.Text = "Recargo servicio:";
            // 
            // lblRecargo
            // 
            lblRecargo.AutoSize = true;
            lblRecargo.Location = new Point(620, 404);
            lblRecargo.Name = "lblRecargo";
            lblRecargo.Size = new Size(31, 15);
            lblRecargo.TabIndex = 19;
            lblRecargo.Text = "0.00";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Georgia", 10F, FontStyle.Bold);
            label8.Location = new Point(494, 430);
            label8.Name = "label8";
            label8.Size = new Size(47, 17);
            label8.TabIndex = 20;
            label8.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Georgia", 10F, FontStyle.Bold);
            lblTotal.Location = new Point(620, 430);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(37, 17);
            lblTotal.TabIndex = 21;
            lblTotal.Text = "0.00";
            // 
            // btnCargarHabitacion
            // 
            btnCargarHabitacion.BackColor = Color.FromArgb(58, 102, 52);
            btnCargarHabitacion.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnCargarHabitacion.FlatStyle = FlatStyle.Flat;
            btnCargarHabitacion.ForeColor = Color.FromArgb(245, 232, 208);
            btnCargarHabitacion.Location = new Point(698, 444);
            btnCargarHabitacion.Name = "btnCargarHabitacion";
            btnCargarHabitacion.Size = new Size(180, 36);
            btnCargarHabitacion.TabIndex = 22;
            btnCargarHabitacion.Text = "Cargar a Habitación";
            btnCargarHabitacion.UseVisualStyleBackColor = false;
            // 
            // tabPedidos
            // 
            tabPedidos.BackColor = Color.FromArgb(236, 220, 192);
            tabPedidos.Controls.Add(btnCancelarPedido);
            tabPedidos.Controls.Add(btnCambiarEstado);
            tabPedidos.Controls.Add(cmbNuevoEstado);
            tabPedidos.Controls.Add(dgvPedidos);
            tabPedidos.Controls.Add(btnBuscarPedidos);
            tabPedidos.Controls.Add(cmbEstadoFiltro);
            tabPedidos.Controls.Add(label10);
            tabPedidos.Controls.Add(dtpFechaFiltro);
            tabPedidos.Controls.Add(label9);
            tabPedidos.Location = new Point(4, 24);
            tabPedidos.Name = "tabPedidos";
            tabPedidos.Size = new Size(900, 500);
            tabPedidos.TabIndex = 1;
            tabPedidos.Text = "Comandas";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(18, 22);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 0;
            label9.Text = "Fecha:";
            // 
            // dtpFechaFiltro
            // 
            dtpFechaFiltro.Format = DateTimePickerFormat.Short;
            dtpFechaFiltro.Location = new Point(68, 18);
            dtpFechaFiltro.Name = "dtpFechaFiltro";
            dtpFechaFiltro.Size = new Size(115, 21);
            dtpFechaFiltro.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(204, 22);
            label10.Name = "label10";
            label10.Size = new Size(47, 15);
            label10.TabIndex = 2;
            label10.Text = "Estado:";
            // 
            // cmbEstadoFiltro
            // 
            cmbEstadoFiltro.BackColor = Color.FromArgb(250, 242, 226);
            cmbEstadoFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoFiltro.Items.AddRange(new object[] { "Todos", "Recibido", "EnCocina", "EnCamino", "Entregado", "Cancelado" });
            cmbEstadoFiltro.Location = new Point(258, 18);
            cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            cmbEstadoFiltro.Size = new Size(140, 23);
            cmbEstadoFiltro.TabIndex = 3;
            // 
            // btnBuscarPedidos
            // 
            btnBuscarPedidos.BackColor = Color.FromArgb(92, 58, 33);
            btnBuscarPedidos.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnBuscarPedidos.FlatStyle = FlatStyle.Flat;
            btnBuscarPedidos.ForeColor = Color.FromArgb(245, 232, 208);
            btnBuscarPedidos.Location = new Point(414, 15);
            btnBuscarPedidos.Name = "btnBuscarPedidos";
            btnBuscarPedidos.Size = new Size(95, 28);
            btnBuscarPedidos.TabIndex = 4;
            btnBuscarPedidos.Text = "Buscar";
            btnBuscarPedidos.UseVisualStyleBackColor = false;
            // 
            // dgvPedidos
            // 
            dgvPedidos.AllowUserToAddRows = false;
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPedidos.BackgroundColor = Color.FromArgb(245, 232, 208);
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvPedidos.Columns.AddRange(new DataGridViewColumn[] { colPedId, colPedHabitacion, colPedCliente, colPedFecha, colPedEstado, colPedTotal });
            dgvPedidos.GridColor = Color.FromArgb(140, 105, 70);
            dgvPedidos.Location = new Point(18, 56);
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.ReadOnly = true;
            dgvPedidos.RowHeadersVisible = false;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(862, 378);
            dgvPedidos.TabIndex = 5;
            // 
            // colPedId
            // 
            colPedId.HeaderText = "Id";
            colPedId.Name = "colPedId";
            colPedId.ReadOnly = true;
            // 
            // colPedHabitacion
            // 
            colPedHabitacion.HeaderText = "Habitación";
            colPedHabitacion.Name = "colPedHabitacion";
            colPedHabitacion.ReadOnly = true;
            // 
            // colPedCliente
            // 
            colPedCliente.HeaderText = "Cliente";
            colPedCliente.Name = "colPedCliente";
            colPedCliente.ReadOnly = true;
            // 
            // colPedFecha
            // 
            colPedFecha.HeaderText = "Fecha";
            colPedFecha.Name = "colPedFecha";
            colPedFecha.ReadOnly = true;
            // 
            // colPedEstado
            // 
            colPedEstado.HeaderText = "Estado";
            colPedEstado.Name = "colPedEstado";
            colPedEstado.ReadOnly = true;
            // 
            // colPedTotal
            // 
            colPedTotal.HeaderText = "Total";
            colPedTotal.Name = "colPedTotal";
            colPedTotal.ReadOnly = true;
            // 
            // cmbNuevoEstado
            // 
            cmbNuevoEstado.BackColor = Color.FromArgb(250, 242, 226);
            cmbNuevoEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNuevoEstado.Items.AddRange(new object[] { "EnCocina", "EnCamino", "Entregado" });
            cmbNuevoEstado.Location = new Point(18, 452);
            cmbNuevoEstado.Name = "cmbNuevoEstado";
            cmbNuevoEstado.Size = new Size(140, 23);
            cmbNuevoEstado.TabIndex = 6;
            // 
            // btnCambiarEstado
            // 
            btnCambiarEstado.BackColor = Color.FromArgb(92, 58, 33);
            btnCambiarEstado.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnCambiarEstado.FlatStyle = FlatStyle.Flat;
            btnCambiarEstado.ForeColor = Color.FromArgb(245, 232, 208);
            btnCambiarEstado.Location = new Point(170, 449);
            btnCambiarEstado.Name = "btnCambiarEstado";
            btnCambiarEstado.Size = new Size(135, 30);
            btnCambiarEstado.TabIndex = 7;
            btnCambiarEstado.Text = "Cambiar Estado";
            btnCambiarEstado.UseVisualStyleBackColor = false;
            // 
            // btnCancelarPedido
            // 
            btnCancelarPedido.BackColor = Color.FromArgb(138, 44, 36);
            btnCancelarPedido.FlatAppearance.BorderColor = Color.FromArgb(196, 154, 69);
            btnCancelarPedido.FlatStyle = FlatStyle.Flat;
            btnCancelarPedido.ForeColor = Color.FromArgb(245, 232, 208);
            btnCancelarPedido.Location = new Point(318, 449);
            btnCancelarPedido.Name = "btnCancelarPedido";
            btnCancelarPedido.Size = new Size(135, 30);
            btnCancelarPedido.TabIndex = 8;
            btnCancelarPedido.Text = "Cancelar Pedido";
            btnCancelarPedido.UseVisualStyleBackColor = false;
            // 
            // FrmRestauranteHuespedes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 220, 192);
            ClientSize = new Size(940, 560);
            Controls.Add(tabRestaurante);
            Font = new Font("Georgia", 9F);
            ForeColor = Color.FromArgb(46, 31, 20);
            Name = "FrmRestauranteHuespedes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Restaurante";
            tabRestaurante.ResumeLayout(false);
            tabNuevoPedido.ResumeLayout(false);
            tabNuevoPedido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            tabPedidos.ResumeLayout(false);
            tabPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabRestaurante;
        private TabPage tabNuevoPedido;
        private Label label1;
        private ComboBox cmbHabitacion;
        private RadioButton rbRoomService;
        private RadioButton rbMesa;
        private ComboBox cmbMesa;
        private Label label2;
        private DateTimePicker dtpHoraDeseada;
        private Label label3;
        private ComboBox cmbCategoriaFiltro;
        private Label lblPlatilloCombo;
        private ComboBox cmbPlatillo;
        private Label lblPrecioPlatillo;
        private TextBox txtPrecioPlatillo;
        private CheckBox chkRefresco;
        private ComboBox cmbRefresco;
        private FlowLayoutPanel flpMenu;
        private Label label4;
        private NumericUpDown nudCantidad;
        private Button btnAgregar;
        private Button btnQuitar;
        private Button btnNuevoPlatilloMenu;
        private DataGridView dgvDetalle;
        private DataGridViewTextBoxColumn colPlatillo;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colSubtotal;
        private Label label5;
        private TextBox txtInstrucciones;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label lblSubtotal;
        private Label lblRecargo;
        private Label lblTotal;
        private Button btnCargarHabitacion;
        private TabPage tabPedidos;
        private Label label9;
        private DateTimePicker dtpFechaFiltro;
        private Label label10;
        private ComboBox cmbEstadoFiltro;
        private Button btnBuscarPedidos;
        private DataGridView dgvPedidos;
        private DataGridViewTextBoxColumn colPedId;
        private DataGridViewTextBoxColumn colPedHabitacion;
        private DataGridViewTextBoxColumn colPedCliente;
        private DataGridViewTextBoxColumn colPedFecha;
        private DataGridViewTextBoxColumn colPedEstado;
        private DataGridViewTextBoxColumn colPedTotal;
        private ComboBox cmbNuevoEstado;
        private Button btnCambiarEstado;
        private Button btnCancelarPedido;
    }
}
