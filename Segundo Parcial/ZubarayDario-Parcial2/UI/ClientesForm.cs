using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class ClientesForm : Form
    {
        #region Atributos
        ClientesBLL _clientesBLL;
        List<Cliente> _listaClientes;
        #endregion

        public ClientesForm()
        {
            InitializeComponent();

            _clientesBLL = new ClientesBLL();
            _listaClientes = new List<Cliente>();
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
                _listaClientes = _clientesBLL.ListarTodo();
                dataGridView1.DataSource = _listaClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HabilitarEdicion()
        {
            btnCrear.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void DeshabilitarEdicion()
        {
            btnCrear.Enabled = false;
            btnModificar.Enabled = true;
            btnBorrar.Enabled = true;
        }

        private void LimpiarFormulario()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
        }

        private Cliente CargarDelFormulario()
        {
            return new Cliente()
            {
                Codigo = Convert.ToInt32(txtCodigo.Text == "" ? "0" : txtCodigo.Text),
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                DNI = txtDNI.Text == "" ? 0 : Convert.ToInt32(txtDNI.Text)
            };
        }
        #endregion

        #region Eventos
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarEdicion();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = CargarDelFormulario();
                if(!_clientesBLL.Guardar(cliente))
                    throw new ClienteException("Error al intentar dar el alta del cliente", "Desconocido");

                CargarGrilla();
                LimpiarFormulario();
                HabilitarEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = CargarDelFormulario();
                if(!_clientesBLL.Guardar(cliente))
                    throw new ClienteException("Error al intentar modificar el cliente", "Desconocido");

                CargarGrilla();
                LimpiarFormulario();
                HabilitarEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = CargarDelFormulario();
                if (!_clientesBLL.Baja(cliente))
                    throw new ClienteException("Error al intentar dar la baja del cliente", "Desconocido");

                CargarGrilla();
                LimpiarFormulario();
                HabilitarEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    txtCodigo.Text = fila.Cells["Codigo"].Value.ToString();
                    txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                    txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                    txtDNI.Text = fila.Cells["DNI"].Value.ToString();
                }

                DeshabilitarEdicion();
            }
            else
            {
                LimpiarFormulario();
                HabilitarEdicion();
            }
        }
        #endregion
    }
}
