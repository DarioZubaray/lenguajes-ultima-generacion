using System;
using System.Collections.Generic;
using System.Windows.Forms;

using EntidadesNegocio;
using LogicaNegocio;

namespace Presentacion
{
    public partial class FormCursos : Form
    {
        private CursoBLL _cursosBLL;
        private List<CursoBE> _listaCursos;

        public FormCursos()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;

            _cursosBLL = new CursoBLL();

            CargarGrilla();
        }

        #region Metodo Auxiliares
        private void CargarGrilla()
        {
            try
            {
                _listaCursos = _cursosBLL.ListarTodo();

                dataGridView1.DataSource = _listaCursos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            dateTimePicker.Value = DateTime.Now;
            comboBox.SelectedIndex = 0;
        }

        private void HabilitarEdicion()
        {
            btnCrear.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private CursoBE CargarDelFormulario()
        {
            int.TryParse(txtCodigo.Text, out int codigo);
            if ("Gratuito".Equals(comboBox.Text))
            {
                return new CursoGratuitoBE()
                {
                    Codigo = codigo,
                    Nombre = txtNombre.Text.Trim(),
                    Inicio = dateTimePicker.Value,
                };
            }
            else 
            {
                return new CursoPagoBE()
                {
                    Codigo = codigo,
                    Nombre = txtNombre.Text,
                    Inicio = dateTimePicker.Value,
                    Precio = Convert.ToInt32(txtPrecio.Text)
                };
            }
        }

        private bool ValidarCurso(CursoBE curso)
        {
            return !string.IsNullOrWhiteSpace(curso.Nombre);
        }
        #endregion

        #region Eventos
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                CursoBE nuevoCurso = CargarDelFormulario();
                if (ValidarCurso(nuevoCurso))
                {
                    var mensaje = $"¿Estás seguro de crear al curso: {nuevoCurso.Nombre}?";
                    DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        _cursosBLL.Guardar(nuevoCurso);
                    }
                    CargarGrilla();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarEdicion();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = comboBox.SelectedItem.ToString();

            if (seleccion == "Gratuito")
            {
                txtPrecio.Enabled = false;
            }
            else
            {
                txtPrecio.Enabled = true;
                txtPrecio.Text = "0";
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
                    var inicio = fila.Cells["Inicio"].Value.ToString();
                    DateTime result = DateTime.Parse(inicio);
                    dateTimePicker.Value = result;
                    //txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
                }

                btnCrear.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                LimpiarFormulario();
                HabilitarEdicion();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                CursoBE cursoModificado = CargarDelFormulario();
                _cursosBLL.Guardar(cursoModificado);
                CargarGrilla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var cursoABorrar = CargarDelFormulario();

                var mensaje = $"¿Estás seguro de eliminar al curso: {cursoABorrar.Nombre}?";
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _cursosBLL.Baja(cursoABorrar);
                    CargarGrilla();
                }
            }
        }
        #endregion
    }
}
