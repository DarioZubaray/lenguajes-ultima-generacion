using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class IInformeMayorDescuento : Form
    {
        BLLCliente _BLLCliente;

        public IInformeMayorDescuento()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            _BLLCliente = new BLLCliente();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _BLLCliente.ListarClientesMayorDescuentos();
        }
    }
}
