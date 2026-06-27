using System;
using System.Collections.Generic;
using System.Windows.Forms;

using EntidadesNegocio;
using LogicaNegocio;

namespace Presentacion
{
    public partial class FormAlumnos : Form
    {
        private AlumnoBLL _alumnoBLL;
        private List<AlumnoBE> _listaAlumnos;

        public FormAlumnos()
        {
            InitializeComponent();
            _alumnoBLL = new AlumnoBLL();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            CargarGrilla();
        }

        #region Metodos Auxiliares
        private void CargarGrilla()
        {
            try
            {
                _listaAlumnos = _alumnoBLL.ListarTodo();

                dataGridView1.DataSource = _listaAlumnos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private AlumnoBE CargarDelFormulario()
        {
            return new AlumnoBE()
            {
                Codigo = Convert.ToInt32(txtLegajo.Text),
                NombreApellido = txtNombreYApellido.Text.Trim(),
                Documento = Convert.ToInt32(txtDocumento.Text),
                Nacimiento = dateTimePicker.Value,
                Direccion = new DireccionBE()
                {
                    CalleNumero = txtCalleYNumero.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim()
                }
            };
        }

        private bool ValidarAlumno(AlumnoBE alumno)
        {
            return !string.IsNullOrWhiteSpace(alumno.NombreApellido)
                && !string.IsNullOrWhiteSpace(alumno.Direccion.CalleNumero)
                && !string.IsNullOrWhiteSpace(alumno.Direccion.Ciudad);
        }

        private void LimpiarFormulario()
        {
            txtLegajo.Clear();
            txtNombreYApellido.Clear();
            txtDocumento.Clear();
            dateTimePicker.Value = DateTime.Now;
            txtCalleYNumero.Clear();
            txtCiudad.Clear();
        }

        private void HabilitarEdicion()
        {
            btnCrear.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        #endregion

        #region Metodos Eventos
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                AlumnoBE nuevoAlumno = CargarDelFormulario();
                if (ValidarAlumno(nuevoAlumno))
                {
                    var mensaje = $"¿Estás seguro de crear al alumno: {nuevoAlumno.NombreApellido}, dni: {nuevoAlumno.Documento}?";
                    DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        _alumnoBLL.Guardar(nuevoAlumno);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    txtLegajo.Text = fila.Cells["Codigo"].Value.ToString();
                    txtNombreYApellido.Text = fila.Cells["NombreApellido"].Value.ToString();
                    txtDocumento.Text = fila.Cells["Documento"].Value.ToString();
                    var nacimiento = fila.Cells["Nacimiento"].Value.ToString();
                    DateTime result = DateTime.Parse(nacimiento);
                    dateTimePicker.Value = result;
                    var direccion = fila.Cells["Direccion"].Value.ToString();
                    string[] split = direccion.Split(',');
                    txtCalleYNumero.Text = split[0];
                    txtCiudad.Text = split[1];
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
                AlumnoBE alumnoModificado = CargarDelFormulario();
                _alumnoBLL.Guardar(alumnoModificado);
                CargarGrilla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var alumnoABorrar = CargarDelFormulario();

                var mensaje = $"¿Estás seguro de eliminar al alumno: {alumnoABorrar.NombreApellido}, dni: {alumnoABorrar.Documento}?";
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _alumnoBLL.Baja(alumnoABorrar);
                    CargarGrilla();
                }
            }
        }
        #endregion
    }
}
