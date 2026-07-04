using System;
using System.Windows.Forms;

namespace UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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
    }
}
