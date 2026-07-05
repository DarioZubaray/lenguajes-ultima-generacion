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

            cbEstado.DataSource = Enum.GetValues(typeof(EstadoDispositivo));
            cbProcesador.DataSource = this._procesadorBLL.ListarTodo();

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
            return new Dispositivo()
            {
                Codigo = 0,
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = Convert.ToDouble(txtPrecio.Text.Trim()),
                Cantidad = Convert.ToInt32(txtCantidad.Text.Trim()),
                Estado = (EstadoDispositivo)cbEstado.SelectedItem,
                Procesador = (Procesador)cbProcesador.SelectedItem
            };
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            cbEstado.SelectedIndex = 0;
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
    }
}
