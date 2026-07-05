using System;
using System.Windows.Forms;

using Persistencia;

namespace UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            // Generando archivos de pruebas
            new ProcesadorXML();
            new DispositivosXML();
            new ClientesXML();
            new ContratacionXML();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ClientesForm();
            form.MdiParent = this;
            form.Show();
        }

        private void dispositivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DispositivosForm();
            form.MdiParent = this;
            form.Show();
        }

        private void contratacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ContratacionForm();
            form.MdiParent = this;
            form.Show();
        }

        private void dispositivoMásVendidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformeDispositivoMasVendido form = new InformeDispositivoMasVendido();
            form.MdiParent = this;
            form.Show();
        }

        private void montoRecaudadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformeMontoRecaudado form = new InformeMontoRecaudado();
            form.MdiParent = this;
            form.Show();
        }
    }
}
