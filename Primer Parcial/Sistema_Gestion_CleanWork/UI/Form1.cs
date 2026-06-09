using System;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormServicios form = new FormServicios
            {
                MdiParent = this
            };
            form.Show();
        }

        private void cuadrillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCuadrilla form = new FormCuadrilla
            {
                MdiParent = this
            };
            form.Show();
        }

        private void serviciosContratadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformeServiciosContratados form = new InformeServiciosContratados
            {
                MdiParent = this
            };
            form.Show();
        }

        private void servicioMasContratadoPorCuadrillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformeServcioMasContratado form = new InformeServcioMasContratado
            {
                MdiParent = this
            };
            form.Show();
        }

        private void servicioMenosContratadoPorCuadrillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformeServicioMenosContratado form = new InformeServicioMenosContratado
            {
                MdiParent = this
            };
            form.Show();
        }

        private void clientesConMayoresDescuentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IInformeMayorDescuento form = new IInformeMayorDescuento
            {
                MdiParent = this
            };
            form.Show();
        }
    }
}
