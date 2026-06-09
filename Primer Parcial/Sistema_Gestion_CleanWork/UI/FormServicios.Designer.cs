
namespace UI
{
    partial class FormServicios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtcodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbAbono = new System.Windows.Forms.ComboBox();
            this.txtPrecioBase = new System.Windows.Forms.TextBox();
            this.lblPrecioBase = new System.Windows.Forms.Label();
            this.lblAbono = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtTipo = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.lblQuimico = new System.Windows.Forms.Label();
            this.cbQuimico = new System.Windows.Forms.ComboBox();
            this.lblMaximaAltura = new System.Windows.Forms.Label();
            this.txtMaximaAltura = new System.Windows.Forms.TextBox();
            this.lblCuadrilla = new System.Windows.Forms.Label();
            this.cbCuadrilla = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnModificar);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Controls.Add(this.cbCuadrilla);
            this.groupBox1.Controls.Add(this.lblCuadrilla);
            this.groupBox1.Controls.Add(this.txtMaximaAltura);
            this.groupBox1.Controls.Add(this.lblMaximaAltura);
            this.groupBox1.Controls.Add(this.cbQuimico);
            this.groupBox1.Controls.Add(this.lblQuimico);
            this.groupBox1.Controls.Add(this.cbTipo);
            this.groupBox1.Controls.Add(this.txtTipo);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.txtcodigo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbAbono);
            this.groupBox1.Controls.Add(this.txtPrecioBase);
            this.groupBox1.Controls.Add(this.lblPrecioBase);
            this.groupBox1.Controls.Add(this.lblAbono);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 182);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servicios";
            // 
            // txtcodigo
            // 
            this.txtcodigo.Enabled = false;
            this.txtcodigo.Location = new System.Drawing.Point(94, 17);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.ReadOnly = true;
            this.txtcodigo.Size = new System.Drawing.Size(121, 20);
            this.txtcodigo.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Código";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(541, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbAbono
            // 
            this.cbAbono.FormattingEnabled = true;
            this.cbAbono.Location = new System.Drawing.Point(94, 84);
            this.cbAbono.Name = "cbAbono";
            this.cbAbono.Size = new System.Drawing.Size(121, 21);
            this.cbAbono.TabIndex = 6;
            // 
            // txtPrecioBase
            // 
            this.txtPrecioBase.Location = new System.Drawing.Point(94, 116);
            this.txtPrecioBase.Name = "txtPrecioBase";
            this.txtPrecioBase.Size = new System.Drawing.Size(121, 20);
            this.txtPrecioBase.TabIndex = 5;
            // 
            // lblPrecioBase
            // 
            this.lblPrecioBase.AutoSize = true;
            this.lblPrecioBase.Location = new System.Drawing.Point(15, 123);
            this.lblPrecioBase.Name = "lblPrecioBase";
            this.lblPrecioBase.Size = new System.Drawing.Size(64, 13);
            this.lblPrecioBase.TabIndex = 4;
            this.lblPrecioBase.Text = "Precio Base";
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.Location = new System.Drawing.Point(15, 92);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(38, 13);
            this.lblAbono.TabIndex = 2;
            this.lblAbono.Text = "Abono";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(94, 54);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(121, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(15, 57);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 202);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(775, 236);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(622, 20);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtTipo
            // 
            this.txtTipo.AutoSize = true;
            this.txtTipo.Location = new System.Drawing.Point(18, 151);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(28, 13);
            this.txtTipo.TabIndex = 11;
            this.txtTipo.Text = "Tipo";
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Limpieza de Alfombras",
            "Limpieza de Vidrios en Altura"});
            this.cbTipo.Location = new System.Drawing.Point(94, 151);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(121, 21);
            this.cbTipo.TabIndex = 12;
            this.cbTipo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblQuimico
            // 
            this.lblQuimico.AutoSize = true;
            this.lblQuimico.Location = new System.Drawing.Point(243, 159);
            this.lblQuimico.Name = "lblQuimico";
            this.lblQuimico.Size = new System.Drawing.Size(45, 13);
            this.lblQuimico.TabIndex = 13;
            this.lblQuimico.Text = "Quimico";
            // 
            // cbQuimico
            // 
            this.cbQuimico.FormattingEnabled = true;
            this.cbQuimico.Items.AddRange(new object[] {
            "Estándar",
            "Hipoalergénico",
            "Premium"});
            this.cbQuimico.Location = new System.Drawing.Point(325, 151);
            this.cbQuimico.Name = "cbQuimico";
            this.cbQuimico.Size = new System.Drawing.Size(121, 21);
            this.cbQuimico.TabIndex = 14;
            // 
            // lblMaximaAltura
            // 
            this.lblMaximaAltura.AutoSize = true;
            this.lblMaximaAltura.Location = new System.Drawing.Point(246, 158);
            this.lblMaximaAltura.Name = "lblMaximaAltura";
            this.lblMaximaAltura.Size = new System.Drawing.Size(73, 13);
            this.lblMaximaAltura.TabIndex = 15;
            this.lblMaximaAltura.Text = "Maxima Altura";
            // 
            // txtMaximaAltura
            // 
            this.txtMaximaAltura.Location = new System.Drawing.Point(325, 151);
            this.txtMaximaAltura.Name = "txtMaximaAltura";
            this.txtMaximaAltura.Size = new System.Drawing.Size(121, 20);
            this.txtMaximaAltura.TabIndex = 16;
            // 
            // lblCuadrilla
            // 
            this.lblCuadrilla.AutoSize = true;
            this.lblCuadrilla.Location = new System.Drawing.Point(246, 20);
            this.lblCuadrilla.Name = "lblCuadrilla";
            this.lblCuadrilla.Size = new System.Drawing.Size(47, 13);
            this.lblCuadrilla.TabIndex = 17;
            this.lblCuadrilla.Text = "Cuadrilla";
            // 
            // cbCuadrilla
            // 
            this.cbCuadrilla.FormattingEnabled = true;
            this.cbCuadrilla.Location = new System.Drawing.Point(325, 17);
            this.cbCuadrilla.Name = "cbCuadrilla";
            this.cbCuadrilla.Size = new System.Drawing.Size(121, 21);
            this.cbCuadrilla.TabIndex = 18;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(622, 87);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 19;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(541, 87);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 20;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // FormServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormServicios";
            this.Text = "FormServicios";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPrecioBase;
        private System.Windows.Forms.Label lblPrecioBase;
        private System.Windows.Forms.Label lblAbono;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ComboBox cbAbono;
        private System.Windows.Forms.TextBox txtcodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox txtMaximaAltura;
        private System.Windows.Forms.Label lblMaximaAltura;
        private System.Windows.Forms.ComboBox cbQuimico;
        private System.Windows.Forms.Label lblQuimico;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label txtTipo;
        private System.Windows.Forms.ComboBox cbCuadrilla;
        private System.Windows.Forms.Label lblCuadrilla;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
    }
}