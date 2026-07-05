using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class ContratacionForm : Form
    {
        private ClientesBLL _clienteBLL;
        private DispositivosBLL _dispositivoBLL;
        private ContratacionBLL _contratacionBLL;

        private List<ContratacionView> _listaContratacion;

        public ContratacionForm()
        {
            InitializeComponent();

            this._clienteBLL = new ClientesBLL();
            this._dispositivoBLL = new DispositivosBLL();
            this._contratacionBLL = new ContratacionBLL();

            this._listaContratacion = new List<ContratacionView>();

            cbClientes.DataSource = _clienteBLL.ListarSinContrataciones();
            cbDispositivo.DataSource = _dispositivoBLL.ListarTodo();
            InicializarGrilla();
            CargarGrilla();
        }

        #region Metodos Auxiliares
        private void InicializarGrilla()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }


        private void CargarGrilla()
        {
            try
            {
                this._listaContratacion = _contratacionBLL.ListarTodo();
                dataGridView1.DataSource = this._listaContratacion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnComprar_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = (Cliente)cbClientes.SelectedItem;
                var dispositivo = (Dispositivo)cbDispositivo.SelectedItem;

                this._contratacionBLL.Contratar(cliente, dispositivo);
                CargarGrilla();
                MessageBox.Show($"El cliente {cliente} compro {dispositivo}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
