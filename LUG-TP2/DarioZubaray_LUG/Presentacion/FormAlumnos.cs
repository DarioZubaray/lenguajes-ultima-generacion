using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using EntidadesNegocio;
using LogicaNegocio;

namespace Presentacion
{
    public partial class FormAlumnos : Form
    {
        #region Atributos
        private AlumnoBLL _alumnoBLL;
        private List<AlumnoBE> _listaAlumnos;

        // Expresiones regulares
        // Documento: entre 7 y 8 digitos numericos
        private static readonly Regex _regexDocumento = new Regex(@"^\d{7,8}$");
        // Nombre y apellido: solo letras, espacios y acentos, minimo dos palabras
        private static readonly Regex _regexNombreApellido = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+(\s[a-zA-ZáéíóúÁÉÍÓÚñÑ]+)+$");
        #endregion

        #region Constructor
        public FormAlumnos()
        {
            InitializeComponent();
            _alumnoBLL = new AlumnoBLL();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            // Suscribir el evento del UserControl
            ucBuscador.Placeholder = "Buscar por nombre...";
            ucBuscador.OnBuscar += UcBuscador_OnBuscar;

            CargarGrilla();
        }
        #endregion

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
                Codigo = Convert.ToInt32(txtLegajo.Text == "" ? "0" : txtLegajo.Text),
                NombreApellido = txtNombreYApellido.Text.Trim(),
                Documento = txtDocumento.Text == "" ? 0 : Convert.ToInt32(txtDocumento.Text),
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
            if (!_regexNombreApellido.IsMatch(alumno.NombreApellido))
            {
                MessageBox.Show(
                    "El nombre y apellido debe contener al menos dos palabras y solo letras.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreYApellido.Focus();
                return false;
            }

            if (!_regexDocumento.IsMatch(alumno.Documento.ToString()))
            {
                MessageBox.Show(
                    "El documento debe contener entre 7 y 8 dígitos numéricos.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(alumno.Direccion.CalleNumero))
            {
                MessageBox.Show("La calle y número es obligatoria.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCalleYNumero.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(alumno.Direccion.Ciudad))
            {
                MessageBox.Show("La ciudad es obligatoria.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCiudad.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtLegajo.Clear();
            txtNombreYApellido.Clear();
            txtDocumento.Clear();
            dateTimePicker.Value = DateTime.Now;
            txtCalleYNumero.Clear();
            txtCiudad.Clear();
            ucBuscador.LimpiarBusqueda();
        }

        private void HabilitarEdicion()
        {
            btnCrear.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        #endregion

        #region Eventos UserControl Buscador
        private void UcBuscador_OnBuscar(object sender, string textoBusqueda)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    dataGridView1.DataSource = _listaAlumnos;
                    return;
                }

                var filtrados = _listaAlumnos.FindAll(a =>
                    a.NombreApellido.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0);

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
                    DateTime result = DateTime.Parse(fila.Cells["Nacimiento"].Value.ToString());
                    dateTimePicker.Value = result;
                    string[] split = fila.Cells["Direccion"].Value.ToString().Split(',');
                    txtCalleYNumero.Text = split[0];
                    txtCiudad.Text = split.Length > 1 ? split[1] : string.Empty;
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
                    AlumnoBE alumnoModificado = CargarDelFormulario();
                    if (ValidarAlumno(alumnoModificado))
                    {
                        _alumnoBLL.Guardar(alumnoModificado);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}