using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BE;
using BLL;

namespace UI
{
    public partial class FormServicios : Form
    {
        List<BEServicio> _BEServicios;

        BLLServicio _bllServicio;
        BLLCuadrilla _bllCuadrilla;

        public FormServicios()
        {
            InitializeComponent();
            cbAbono.DataSource = Enum.GetValues(typeof(TipoAbono));

            _BEServicios = new List<BEServicio>();
            _bllServicio = new BLLServicio();
            _bllCuadrilla = new BLLCuadrilla();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            cbCuadrilla.DataSource = _bllCuadrilla.ListarTodo();
            CargarGrilla();
            cbTipo.SelectedItem = "Limpieza de Alfombras";
            cbQuimico.SelectedItem = "Estándar";
        }

        private void CargarGrilla()
        {
            try
            {
                _BEServicios = _bllServicio.ListarTodo();

                dataGridView1.DataSource = _BEServicios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtcodigo.Clear();
            txtNombre.Clear();
            txtPrecioBase.Clear();
            cbAbono.SelectedItem = TipoAbono.Semanal;
            cbTipo.SelectedItem = "Limpieza de Alfombras";
            cbQuimico.SelectedItem = "Estándar";
        }

        private BEServicio CargarDelFormulario()
        {
            if (cbTipo.SelectedItem.ToString() == "Limpieza de Alfombras")
            {
                BEServicioLimpiezaAlfombras formulario = new BEServicioLimpiezaAlfombras();
                formulario.Quimico = cbQuimico.SelectedItem.ToString();
                int.TryParse(txtcodigo.Text, out int codigo);
                formulario.Codigo = codigo;
                formulario.Nombre = txtNombre.Text;
                formulario.Abono = (TipoAbono)cbAbono.SelectedItem;
                decimal.TryParse(txtPrecioBase.Text.Replace('.', ','), out decimal precioBase);
                formulario.PrecioBase = precioBase;
                var cuadrilla = new BECuadrilla();
                cuadrilla.Codigo = Convert.ToInt32(cbCuadrilla.SelectedItem.ToString().Split('-')[0]);
                formulario.CuadrillaTrabajo = cuadrilla;
                return formulario;
            }
            else
            {
                BEServicioLimpiezaVidriosAltura formulario = new BEServicioLimpiezaVidriosAltura();
                formulario.AlturaMaxima = Convert.ToInt32(txtMaximaAltura.Text);
                int.TryParse(txtcodigo.Text, out int codigo);
                formulario.Codigo = codigo;
                formulario.Nombre = txtNombre.Text;
                formulario.Abono = (TipoAbono)cbAbono.SelectedItem;
                decimal.TryParse(txtPrecioBase.Text, out decimal precioBase);
                formulario.PrecioBase = precioBase;
                var cuadrilla = new BECuadrilla();
                cuadrilla.Codigo = Convert.ToInt32(cbCuadrilla.SelectedItem.ToString().Split('-')[0]);
                formulario.CuadrillaTrabajo = cuadrilla;
                return formulario;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    var codigo = fila.Cells["Codigo"].Value.ToString().Trim();
                    txtcodigo.Text = codigo;
                    txtNombre.Text = fila.Cells["Nombre"].Value.ToString().Trim();
                    cbAbono.Text = fila.Cells["Abono"].Value.ToString().Trim();
                    txtPrecioBase.Text = fila.Cells["PrecioBase"].Value.ToString().Trim();
                    cbCuadrilla.Text = fila.Cells["CuadrillaTrabajo"].Value.ToString().Trim();

                    foreach (BEServicio servicio in _BEServicios)
                    {
                        if (servicio.Codigo == int.Parse(codigo))
                        {
                            if (servicio is BEServicioLimpiezaAlfombras)
                            {
                                cbTipo.SelectedItem = "Limpieza de Alfombras";
                                cbQuimico.SelectedItem = ((BEServicioLimpiezaAlfombras)servicio).Quimico;
                            }
                            else
                            {
                                cbTipo.SelectedItem = "Limpieza de Vidrios en Altura";
                                txtMaximaAltura.Text = ((BEServicioLimpiezaVidriosAltura)servicio).AlturaMaxima.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BEServicio nuevoServicio = CargarDelFormulario();
                var mensaje = $"¿Estás seguro de crear al servicio: {nuevoServicio.Nombre}?";
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _bllServicio.Guardar(nuevoServicio);
                }
                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = cbTipo.SelectedItem.ToString();

            if (valor == "Limpieza de Alfombras")
            {
                lblQuimico.Visible = true;
                cbQuimico.Visible = true;
                lblMaximaAltura.Visible = false;
                txtMaximaAltura.Visible = false;
            }
            else if (valor == "Limpieza de Vidrios en Altura")
            {
                lblQuimico.Visible = false;
                cbQuimico.Visible = false;
                lblMaximaAltura.Visible = true;
                txtMaximaAltura.Visible = true;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                BEServicio servicioModificado = CargarDelFormulario();
                _bllServicio.Guardar(servicioModificado);
                CargarGrilla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var servicioABorrar = CargarDelFormulario();

                var mensaje = $"¿Estás seguro de eliminar el servicio: {servicioABorrar.Nombre}?";
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _bllServicio.Baja(servicioABorrar);
                    CargarGrilla();
                }
            }
        }
    }
}
