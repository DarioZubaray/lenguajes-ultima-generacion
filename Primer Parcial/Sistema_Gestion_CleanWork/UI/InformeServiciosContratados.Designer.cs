
namespace UI
{
    partial class InformeServiciosContratados
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
            this.dataGridViewClientes = new System.Windows.Forms.DataGridView();
            this.btnLimpiezaAlfombras = new System.Windows.Forms.Button();
            this.btnVidriosAltura = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewServicios = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServicios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewClientes);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clientes";
            // 
            // dataGridViewClientes
            // 
            this.dataGridViewClientes.AllowUserToAddRows = false;
            this.dataGridViewClientes.AllowUserToDeleteRows = false;
            this.dataGridViewClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClientes.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewClientes.Name = "dataGridViewClientes";
            this.dataGridViewClientes.ReadOnly = true;
            this.dataGridViewClientes.Size = new System.Drawing.Size(668, 150);
            this.dataGridViewClientes.TabIndex = 0;
            // 
            // btnLimpiezaAlfombras
            // 
            this.btnLimpiezaAlfombras.Location = new System.Drawing.Point(134, 193);
            this.btnLimpiezaAlfombras.Name = "btnLimpiezaAlfombras";
            this.btnLimpiezaAlfombras.Size = new System.Drawing.Size(175, 23);
            this.btnLimpiezaAlfombras.TabIndex = 1;
            this.btnLimpiezaAlfombras.Text = "Ver: Limpieza Alfombras";
            this.btnLimpiezaAlfombras.UseVisualStyleBackColor = true;
            this.btnLimpiezaAlfombras.Click += new System.EventHandler(this.btnLimpiezaAlfombras_Click);
            // 
            // btnVidriosAltura
            // 
            this.btnVidriosAltura.Location = new System.Drawing.Point(315, 193);
            this.btnVidriosAltura.Name = "btnVidriosAltura";
            this.btnVidriosAltura.Size = new System.Drawing.Size(200, 23);
            this.btnVidriosAltura.TabIndex = 2;
            this.btnVidriosAltura.Text = "Ver: Limpieza Vidrios Altura";
            this.btnVidriosAltura.UseVisualStyleBackColor = true;
            this.btnVidriosAltura.Click += new System.EventHandler(this.btnVidriosAltura_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewServicios);
            this.groupBox2.Location = new System.Drawing.Point(19, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(674, 216);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Servicios";
            // 
            // dataGridViewServicios
            // 
            this.dataGridViewServicios.AllowUserToAddRows = false;
            this.dataGridViewServicios.AllowUserToDeleteRows = false;
            this.dataGridViewServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewServicios.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewServicios.Name = "dataGridViewServicios";
            this.dataGridViewServicios.ReadOnly = true;
            this.dataGridViewServicios.Size = new System.Drawing.Size(661, 190);
            this.dataGridViewServicios.TabIndex = 0;
            // 
            // InformeServiciosContratados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnVidriosAltura);
            this.Controls.Add(this.btnLimpiezaAlfombras);
            this.Controls.Add(this.groupBox1);
            this.Name = "InformeServiciosContratados";
            this.Text = "InformeServiciosContratados";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServicios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewClientes;
        private System.Windows.Forms.Button btnLimpiezaAlfombras;
        private System.Windows.Forms.Button btnVidriosAltura;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewServicios;
    }
}