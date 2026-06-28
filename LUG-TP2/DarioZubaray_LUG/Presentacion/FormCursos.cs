using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using EntidadesNegocio;
using LogicaNegocio;

namespace Presentacion
{
    public partial class FormCursos : Form
    {
        #region Atributos
        private CursoBLL _cursosBLL;
        private List<CursoBE> _listaCursos;

        // Expresiones regulares
        // Nombre: letras, numeros, espacios y guiones, minimo 3 caracteres
        private static readonly Regex _regexNombre = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s\-]{3,}$");
        // Precio: entero positivo de 1 a 7 digitos
        private static readonly Regex _regexPrecio = new Regex(@"^\d{1,7}$");
        #endregion

        #region Constructor
        public FormCursos()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;

            // Suscribir el evento del UserControl
            ucBuscador.Placeholder = "Buscar por nombre de curso...";
            ucBuscador.OnBuscar += UcBuscador_OnBuscar;

            _cursosBLL = new CursoBLL();
            CargarGrilla();
        }
        #endregion

        #region Metodos Auxiliares
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
            ucBuscador.LimpiarBusqueda();
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
                    Nombre = txtNombre.Text.Trim(),
                    Inicio = dateTimePicker.Value,
                    Precio = txtPrecio.Text == "" ? 0 : Convert.ToInt32(txtPrecio.Text)
                };
            }
        }

        private bool ValidarCurso(CursoBE curso)
        {
            if (!_regexNombre.IsMatch(curso.Nombre))
            {
                MessageBox.Show(
                    "El nombre del curso debe tener al menos 3 caracteres y solo puede contener letras, números, espacios y guiones.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Si es pago, validar el precio
            if (curso is CursoPagoBE cursoPago)
            {
                if (!_regexPrecio.IsMatch(cursoPago.Precio.ToString()) || cursoPago.Precio <= 0)
                {
                    MessageBox.Show(
                        "El precio debe ser un número entero positivo.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Eventos UserControl Buscador
        private void UcBuscador_OnBuscar(object sender, string textoBusqueda)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    dataGridView1.DataSource = _listaCursos;
                    return;
                }

                var filtrados = _listaCursos.FindAll(c =>
                    c.Nombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0);

                dataGridView1.DataSource = filtrados;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Eventos Formulario
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                CursoBE nuevoCurso = CargarDelFormulario();
                if (ValidarCurso(nuevoCurso))
                {
                    var mensaje = $"¿Estás seguro de crear el curso: {nuevoCurso.Nombre}?";
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
                txtPrecio.Clear();
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
                    DateTime result = DateTime.Parse(fila.Cells["Inicio"].Value.ToString());
                    dateTimePicker.Value = result;
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
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    CursoBE cursoModificado = CargarDelFormulario();
                    if (ValidarCurso(cursoModificado))
                    {
                        _cursosBLL.Guardar(cursoModificado);
                        CargarGrilla();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var cursoABorrar = CargarDelFormulario();
                    var mensaje = $"¿Estás seguro de eliminar el curso: {cursoABorrar.Nombre}?";
                    DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        _cursosBLL.Baja(cursoABorrar);
                        CargarGrilla();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}