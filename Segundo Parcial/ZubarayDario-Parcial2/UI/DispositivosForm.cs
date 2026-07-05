using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class DispositivosForm : Form
    {
        private DispositivosBLL _dispositivosBLL;
        private ProcesadorBLL _procesadorBLL;
        private List<Dispositivo> _listaDispositivos;

        public DispositivosForm()
        {
            InitializeComponent();
            this._dispositivosBLL = new DispositivosBLL();
            this._procesadorBLL = new ProcesadorBLL();
            this._listaDispositivos = new List<Dispositivo>();

            cbProcesador.DataSource = this._procesadorBLL.ListarTodo();
            cbTipo.SelectedIndex = 0;
            cbProposito.SelectedIndex = 0;

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
                this._listaDispositivos = _dispositivosBLL.ListarTodo();
                dataGridView1.DataSource = this._listaDispositivos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dispositivo CargarDelFormulario()
        {
            Dispositivo dispositivo;

            if (cbTipo.SelectedItem?.ToString() == "Notebook")
            {
                dispositivo = new Notebook
                {
                    Proposito = cbProposito.SelectedItem.ToString().Trim()
                };
            }
            else
            {
                dispositivo = new TelefonoMovil
                {
                    ResistenteAgua = chkResistenteAgua.Checked
                };
            }

            dispositivo.Codigo = 0;
            dispositivo.Descripcion = txtDescripcion.Text.Trim();
            dispositivo.Precio = Convert.ToDouble(txtPrecio.Text.Trim());
            dispositivo.Cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
            dispositivo.Estado = EstadoDispositivo.Disponible;
            dispositivo.Procesador = (Procesador)cbProcesador.SelectedItem;

            return dispositivo;
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            chkResistenteAgua.Checked = false;
            cbTipo.SelectedIndex = 0;
            cbProposito.SelectedIndex = 0;
        }
        #endregion

        #region Eventos
        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                var dispositivo = CargarDelFormulario();
                if (!this._dispositivosBLL.Guardar(dispositivo))
                    throw new ClienteException("Error al intentar dar el alta del cliente", "Desconocido");

                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esNotebook = cbTipo.SelectedItem?.ToString() == "Notebook";

            cbProposito.Visible = esNotebook;
            lblProposito.Visible = esNotebook;
            chkResistenteAgua.Visible = !esNotebook;
        }
    }
}