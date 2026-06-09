using System;
using System.Windows.Forms;

using BLL;

namespace UI
{
    public partial class InformeServcioMasContratado : Form
    {
        BLLServicio _BLLServicio;

        public InformeServcioMasContratado()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            _BLLServicio = new BLLServicio();
        }

        private void btnVerInforme_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _BLLServicio.ListarLimpiezaServicioAlfombrasMasVendidoPorcuadrilla();
        }
    }
}
