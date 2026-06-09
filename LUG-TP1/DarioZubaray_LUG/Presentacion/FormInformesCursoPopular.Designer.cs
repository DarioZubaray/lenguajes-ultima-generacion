
namespace Presentacion
{
    partial class FormInformesCursoPopular
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
            this.btnActualizar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblCursoEstrella = new System.Windows.Forms.Label();
            this.lblInscriptos = new System.Windows.Forms.Label();
            this.txtCursoEstrella = new System.Windows.Forms.TextBox();
            this.txtInscriptos = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(399, 12);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 62);
            this.btnActualizar.TabIndex = 0;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 91);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(461, 189);
            this.dataGridView1.TabIndex = 1;
            // 
            // lblCursoEstrella
            // 
            this.lblCursoEstrella.AutoSize = true;
            this.lblCursoEstrella.Location = new System.Drawing.Point(13, 13);
            this.lblCursoEstrella.Name = "lblCursoEstrella";
            this.lblCursoEstrella.Size = new System.Drawing.Size(70, 13);
            this.lblCursoEstrella.TabIndex = 2;
            this.lblCursoEstrella.Text = "Curso estrella";
            // 
            // lblInscriptos
            // 
            this.lblInscriptos.AutoSize = true;
            this.lblInscriptos.Location = new System.Drawing.Point(16, 54);
            this.lblInscriptos.Name = "lblInscriptos";
            this.lblInscriptos.Size = new System.Drawing.Size(52, 13);
            this.lblInscriptos.TabIndex = 3;
            this.lblInscriptos.Text = "Inscriptos";
            // 
            // txtCursoEstrella
            // 
            this.txtCursoEstrella.Enabled = false;
            this.txtCursoEstrella.Location = new System.Drawing.Point(90, 12);
            this.txtCursoEstrella.Name = "txtCursoEstrella";
            this.txtCursoEstrella.ReadOnly = true;
            this.txtCursoEstrella.Size = new System.Drawing.Size(295, 20);
            this.txtCursoEstrella.TabIndex = 4;
            // 
            // txtInscriptos
            // 
            this.txtInscriptos.Enabled = false;
            this.txtInscriptos.Location = new System.Drawing.Point(90, 54);
            this.txtInscriptos.Name = "txtInscriptos";
            this.txtInscriptos.ReadOnly = true;
            this.txtInscriptos.Size = new System.Drawing.Size(295, 20);
            this.txtInscriptos.TabIndex = 5;
            // 
            // FormInformesCursoPopular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 296);
            this.Controls.Add(this.txtInscriptos);
            this.Controls.Add(this.txtCursoEstrella);
            this.Controls.Add(this.lblInscriptos);
            this.Controls.Add(this.lblCursoEstrella);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnActualizar);
            this.Name = "FormInformesCursoPopular";
            this.Text = "FormInformes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblCursoEstrella;
        private System.Windows.Forms.Label lblInscriptos;
        private System.Windows.Forms.TextBox txtCursoEstrella;
        private System.Windows.Forms.TextBox txtInscriptos;
    }
}