
namespace UI
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuadrillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviciosContratadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicioMasContratadoPorCuadrillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicioMenosContratadoPorCuadrillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesConMayoresDescuentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.gestionToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // gestionToolStripMenuItem
            // 
            this.gestionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviciosToolStripMenuItem,
            this.cuadrillaToolStripMenuItem});
            this.gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            this.gestionToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.gestionToolStripMenuItem.Text = "Gestion";
            // 
            // serviciosToolStripMenuItem
            // 
            this.serviciosToolStripMenuItem.Name = "serviciosToolStripMenuItem";
            this.serviciosToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.serviciosToolStripMenuItem.Text = "Servicios";
            this.serviciosToolStripMenuItem.Click += new System.EventHandler(this.serviciosToolStripMenuItem_Click);
            // 
            // cuadrillaToolStripMenuItem
            // 
            this.cuadrillaToolStripMenuItem.Name = "cuadrillaToolStripMenuItem";
            this.cuadrillaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.cuadrillaToolStripMenuItem.Text = "Cuadrilla";
            this.cuadrillaToolStripMenuItem.Click += new System.EventHandler(this.cuadrillaToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviciosContratadosToolStripMenuItem,
            this.servicioMasContratadoPorCuadrillaToolStripMenuItem,
            this.servicioMenosContratadoPorCuadrillaToolStripMenuItem,
            this.clientesConMayoresDescuentosToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.reportesToolStripMenuItem.Text = "Informes";
            // 
            // serviciosContratadosToolStripMenuItem
            // 
            this.serviciosContratadosToolStripMenuItem.Name = "serviciosContratadosToolStripMenuItem";
            this.serviciosContratadosToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.serviciosContratadosToolStripMenuItem.Text = "Servicios Contratados";
            this.serviciosContratadosToolStripMenuItem.Click += new System.EventHandler(this.serviciosContratadosToolStripMenuItem_Click);
            // 
            // servicioMasContratadoPorCuadrillaToolStripMenuItem
            // 
            this.servicioMasContratadoPorCuadrillaToolStripMenuItem.Name = "servicioMasContratadoPorCuadrillaToolStripMenuItem";
            this.servicioMasContratadoPorCuadrillaToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.servicioMasContratadoPorCuadrillaToolStripMenuItem.Text = "Servicio Mas Contratado Por Cuadrilla";
            this.servicioMasContratadoPorCuadrillaToolStripMenuItem.Click += new System.EventHandler(this.servicioMasContratadoPorCuadrillaToolStripMenuItem_Click);
            // 
            // servicioMenosContratadoPorCuadrillaToolStripMenuItem
            // 
            this.servicioMenosContratadoPorCuadrillaToolStripMenuItem.Name = "servicioMenosContratadoPorCuadrillaToolStripMenuItem";
            this.servicioMenosContratadoPorCuadrillaToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.servicioMenosContratadoPorCuadrillaToolStripMenuItem.Text = "Servicio Menos Contratado Por Cuadrilla";
            this.servicioMenosContratadoPorCuadrillaToolStripMenuItem.Click += new System.EventHandler(this.servicioMenosContratadoPorCuadrillaToolStripMenuItem_Click);
            // 
            // clientesConMayoresDescuentosToolStripMenuItem
            // 
            this.clientesConMayoresDescuentosToolStripMenuItem.Name = "clientesConMayoresDescuentosToolStripMenuItem";
            this.clientesConMayoresDescuentosToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.clientesConMayoresDescuentosToolStripMenuItem.Text = "Clientes con Mayores Descuentos";
            this.clientesConMayoresDescuentosToolStripMenuItem.Click += new System.EventHandler(this.clientesConMayoresDescuentosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 561);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuadrillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviciosContratadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicioMasContratadoPorCuadrillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicioMenosContratadoPorCuadrillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesConMayoresDescuentosToolStripMenuItem;
    }
}

