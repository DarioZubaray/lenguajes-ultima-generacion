using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class InformeServiciosContratados : Form
    {
        List<BECliente> _BECLientes;
        List<BEServicio> _BEServicios;

        BLLCliente _BLLCliente;
        BLLServicioLimpiezaAlfombras _BLLServicioLimpiezaAlfombras;
        BLLServicioLimpiezaVidriosAltura _BLLServicioLimpiezaAlturas;

        public InformeServiciosContratados()
        {
            InitializeComponent();
            dataGridViewClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClientes.MultiSelect = false;
            dataGridViewClientes.ReadOnly = true;
            dataGridViewServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewServicios.MultiSelect = false;
            dataGridViewServicios.ReadOnly = true;

            _BLLCliente = new BLLCliente();
            _BLLServicioLimpiezaAlfombras = new BLLServicioLimpiezaAlfombras();
            _BLLServicioLimpiezaAlturas = new BLLServicioLimpiezaVidriosAltura();
            CargarGrillaCliente();
        }

        private void CargarGrillaCliente()
        {
            _BECLientes = _BLLCliente.ListarTodo();
            dataGridViewClientes.DataSource = _BECLientes;
        }

        private void btnLimpiezaAlfombras_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridViewClientes.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    var codigo = Convert.ToInt32(fila.Cells["Codigo"].Value);
                    dataGridViewServicios.DataSource = _BLLServicioLimpiezaAlfombras.ListarPorCodigoCliente(codigo);
                }
            }
        }

        private void btnVidriosAltura_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridViewClientes.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    var codigo = Convert.ToInt32(fila.Cells["Codigo"].Value);
                    dataGridViewServicios.DataSource = _BLLServicioLimpiezaAlturas.ListarPorCodigoCliente(codigo);
                }
            }
        }
    }
}
