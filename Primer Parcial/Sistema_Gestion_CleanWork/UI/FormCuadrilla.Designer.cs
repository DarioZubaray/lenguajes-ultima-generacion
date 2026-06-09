
namespace UI
{
    partial class FormCuadrilla
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
            this.lblNombreSupervisor = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCantidadOperarios = new System.Windows.Forms.Label();
            this.lblTurnoTrabajo = new System.Windows.Forms.Label();
            this.txtNombreSupervisor = new System.Windows.Forms.TextBox();
            this.cbTurno = new System.Windows.Forms.ComboBox();
            this.txtCantidadOperarios = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombreSupervisor
            // 
            this.lblNombreSupervisor.AutoSize = true;
            this.lblNombreSupervisor.Location = new System.Drawing.Point(6, 27);
            this.lblNombreSupervisor.Name = "lblNombreSupervisor";
            this.lblNombreSupervisor.Size = new System.Drawing.Size(97, 13);
            this.lblNombreSupervisor.TabIndex = 0;
            this.lblNombreSupervisor.Text = "Nombre Supervisor";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAgregar);
            this.groupBox1.Controls.Add(this.txtCantidadOperarios);
            this.groupBox1.Controls.Add(this.cbTurno);
            this.groupBox1.Controls.Add(this.txtNombreSupervisor);
            this.groupBox1.Controls.Add(this.lblCantidadOperarios);
            this.groupBox1.Controls.Add(this.lblTurnoTrabajo);
            this.groupBox1.Controls.Add(this.lblNombreSupervisor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 126);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // lblCantidadOperarios
            // 
            this.lblCantidadOperarios.AutoSize = true;
            this.lblCantidadOperarios.Location = new System.Drawing.Point(6, 88);
            this.lblCantidadOperarios.Name = "lblCantidadOperarios";
            this.lblCantidadOperarios.Size = new System.Drawing.Size(97, 13);
            this.lblCantidadOperarios.TabIndex = 2;
            this.lblCantidadOperarios.Text = "Cantidad Operarios";
            // 
            // lblTurnoTrabajo
            // 
            this.lblTurnoTrabajo.AutoSize = true;
            this.lblTurnoTrabajo.Location = new System.Drawing.Point(6, 58);
            this.lblTurnoTrabajo.Name = "lblTurnoTrabajo";
            this.lblTurnoTrabajo.Size = new System.Drawing.Size(35, 13);
            this.lblTurnoTrabajo.TabIndex = 1;
            this.lblTurnoTrabajo.Text = "Turno";
            // 
            // txtNombreSupervisor
            // 
            this.txtNombreSupervisor.Location = new System.Drawing.Point(140, 19);
            this.txtNombreSupervisor.Name = "txtNombreSupervisor";
            this.txtNombreSupervisor.Size = new System.Drawing.Size(121, 20);
            this.txtNombreSupervisor.TabIndex = 3;
            // 
            // cbTurno
            // 
            this.cbTurno.FormattingEnabled = true;
            this.cbTurno.Items.AddRange(new object[] {
            "Mañana",
            "Tarde",
            "Noche"});
            this.cbTurno.Location = new System.Drawing.Point(140, 58);
            this.cbTurno.Name = "cbTurno";
            this.cbTurno.Size = new System.Drawing.Size(121, 21);
            this.cbTurno.TabIndex = 4;
            // 
            // txtCantidadOperarios
            // 
            this.txtCantidadOperarios.Location = new System.Drawing.Point(140, 88);
            this.txtCantidadOperarios.Name = "txtCantidadOperarios";
            this.txtCantidadOperarios.Size = new System.Drawing.Size(121, 20);
            this.txtCantidadOperarios.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 145);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(494, 205);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(291, 19);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 6;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // FormCuadrilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 355);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormCuadrilla";
            this.Text = "FormCuadrilla";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNombreSupervisor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCantidadOperarios;
        private System.Windows.Forms.Label lblTurnoTrabajo;
        private System.Windows.Forms.TextBox txtCantidadOperarios;
        private System.Windows.Forms.ComboBox cbTurno;
        private System.Windows.Forms.TextBox txtNombreSupervisor;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAgregar;
    }
}