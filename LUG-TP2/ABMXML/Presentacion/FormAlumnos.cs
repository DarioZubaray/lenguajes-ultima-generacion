using System;
using System.Collections.Generic;
using System.Windows.Forms;

using EntidadesNegocio;
using LogicaNegocio;

namespace Presentacion
{
    public partial class FormAlumnos : Form
    {
        #region Atributos
        private AlumnoBLL _bll;
        private List<AlumnoBE> _listaAlumnos;
        #endregion

        #region Constructor
        public FormAlumnos()
        {
            InitializeComponent();
            _bll = new AlumnoBLL();

            ConfigurarGrilla();
            CargarGrilla();
        }
        #endregion

        #region Configuracion
        private void ConfigurarGrilla()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Legajo", HeaderText = "Legajo" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreApellido", HeaderText = "Nombre y Apellido" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Documento", HeaderText = "Documento" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nacimiento", HeaderText = "Nacimiento" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CalleNumero", HeaderText = "Calle y Número" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Ciudad", HeaderText = "Ciudad" });
        }
        #endregion

        #region Carga y filtro
        private void CargarGrilla()
        {
            try
            {
                _listaAlumnos = _bll.ObtenerTodos();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _listaAlumnos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<AlumnoBE> resultado = _bll.Buscar(txtBuscar.Text.Trim());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarGrilla();
        }
        #endregion

        #region Auxiliares
        private AlumnoBE CargarDesdeFormulario()
        {
            int.TryParse(txtLegajo.Text, out int legajo);

            return new AlumnoBE
            {
                Legajo = legajo,
                NombreApellido = txtNombre.Text.Trim(),
                Documento = int.TryParse(txtDocumento.Text, out int doc) ? doc : 0,
                Nacimiento = dateTimePicker.Value.ToString("yyyy-MM-dd"),
                CalleNumero = txtCalle.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim()
            };
        }

        private void CargarFormularioDesdeGrilla(AlumnoBE alumno)
        {
            txtLegajo.Text = alumno.Legajo.ToString();
            txtNombre.Text = alumno.NombreApellido;
            txtDocumento.Text = alumno.Documento.ToString();
            txtCalle.Text = alumno.CalleNumero;
            txtCiudad.Text = alumno.Ciudad;

            if (DateTime.TryParse(alumno.Nacimiento, out DateTime fecha))
                dateTimePicker.Value = fecha;
        }

        private void LimpiarFormulario()
        {
            txtLegajo.Clear();
            txtNombre.Clear();
            txtDocumento.Clear();
            txtCalle.Clear();
            txtCiudad.Clear();
            dateTimePicker.Value = DateTime.Now;
        }

        private void HabilitarEdicion(bool esNuevo)
        {
            btnCrear.Enabled = esNuevo;
            btnModificar.Enabled = !esNuevo;
            btnEliminar.Enabled = !esNuevo;
            txtLegajo.ReadOnly = true;
        }
        #endregion

        #region ABM
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                AlumnoBE nuevo = CargarDesdeFormulario();
                nuevo.Legajo = 0; // La capa MPP asigna el legajo

                var confirmacion = MessageBox.Show(
                    $"¿Confirma la creación del alumno: {nuevo.NombreApellido}?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    _bll.Guardar(nuevo);
                    CargarGrilla();
                    LimpiarFormulario();
                    HabilitarEdicion(true);
                    MessageBox.Show("Alumno creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                AlumnoBE modificado = CargarDesdeFormulario();

                var confirmacion = MessageBox.Show(
                    $"¿Confirma la modificación del alumno: {modificado.NombreApellido}?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    _bll.Guardar(modificado);
                    CargarGrilla();
                    LimpiarFormulario();
                    HabilitarEdicion(true);
                    MessageBox.Show("Alumno modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!int.TryParse(txtLegajo.Text, out int legajo) || legajo == 0)
                {
                    MessageBox.Show("Seleccione un alumno para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show(
                    $"¿Confirma la eliminación del alumno con legajo {legajo}?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    _bll.Eliminar(legajo);
                    CargarGrilla();
                    LimpiarFormulario();
                    HabilitarEdicion(true);
                    MessageBox.Show("Alumno eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            HabilitarEdicion(true);
        }
        #endregion

        #region Seleccion en grilla
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is AlumnoBE alumno)
            {
                CargarFormularioDesdeGrilla(alumno);
                HabilitarEdicion(false);
            }
        }
        #endregion
    }
}