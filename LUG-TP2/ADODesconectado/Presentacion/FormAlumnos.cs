using System;
using System.Data;
using System.Windows.Forms;

using AccesoDatos;

namespace Presentacion
{
    public partial class FormAlumnos : Form
    {
        #region Atributos
        private AlumnoDataAccess _dataAccess;
        private DataSet _dataSet;
        private DataView _dataView;  // Vista de Alumnos para filtro
        #endregion

        #region Constructor
        public FormAlumnos()
        {
            InitializeComponent();
            _dataAccess = new AlumnoDataAccess();
            ConfigurarGrilla();
            CargarDatos();
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
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "legajo", HeaderText = "Legajo" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "nombre_apellido", HeaderText = "Nombre y Apellido" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "documento", HeaderText = "Documento" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "fecha_nacimiento", HeaderText = "Fecha Nacimiento" });
        }
        #endregion

        #region Carga de datos
        private void CargarDatos()
        {
            try
            {
                _dataSet = _dataAccess.CargarAlumnos();
                _dataView = new DataView(_dataSet.Tables["Alumnos"]);

                dataGridView1.DataSource = _dataView;
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Metodos auxiliares
        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDocumento.Clear();
            txtCalle.Clear();
            txtCiudad.Clear();
            dateTimePicker.Value = DateTime.Now;
            txtFiltro.Clear();
            if (_dataView != null)
                _dataView.RowFilter = string.Empty;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre y apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            if (!int.TryParse(txtDocumento.Text, out _) || txtDocumento.Text.Length < 7)
            {
                MessageBox.Show("El documento debe ser numérico y tener al menos 7 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCalle.Text))
            {
                MessageBox.Show("La calle y número es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCalle.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                MessageBox.Show("La ciudad es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCiudad.Focus();
                return false;
            }
            return true;
        }

        private void CargarFormularioDesdeGrilla()
        {
            if (dataGridView1.CurrentRow == null) return;

            DataRowView filaVista = (DataRowView)dataGridView1.CurrentRow.DataBoundItem;
            if (filaVista == null) return;

            txtNombre.Text = filaVista["nombre_apellido"].ToString();
            txtDocumento.Text = filaVista["documento"].ToString();
            dateTimePicker.Value = Convert.ToDateTime(filaVista["fecha_nacimiento"]);

            // Buscar la direccion relacionada en la tabla Direcciones
            int legajo = Convert.ToInt32(filaVista["legajo"]);
            DataRow[] direcciones = _dataSet.Tables["Direcciones"].Select($"id_legajo = {legajo}");
            if (direcciones.Length > 0)
            {
                txtCalle.Text = direcciones[0]["calle_numero"].ToString();
                txtCiudad.Text = direcciones[0]["ciudad"].ToString();
            }
            else
            {
                txtCalle.Clear();
                txtCiudad.Clear();
            }
        }

        private void ActualizarEstado()
        {
            bool pendientes = _dataSet != null && _dataSet.HasChanges();
            btnGuardarCambios.Enabled = pendientes;
            btnDescartar.Enabled = pendientes;
            lblEstado.Text = pendientes ? "Hay cambios sin guardar" : "Sin cambios pendientes";
        }
        #endregion

        #region Filtro desconectado (Punto 16)
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string texto = txtFiltro.Text.Trim();
                _dataView.RowFilter = string.IsNullOrEmpty(texto)
                    ? string.Empty
                    : $"nombre_apellido LIKE '%{texto}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en filtro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ABM en memoria

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                DataTable tablaAlumnos = _dataSet.Tables["Alumnos"];
                DataRow nuevaFilaAlumno = tablaAlumnos.NewRow();
                nuevaFilaAlumno["nombre_apellido"] = txtNombre.Text.Trim();
                nuevaFilaAlumno["documento"] = Convert.ToInt32(txtDocumento.Text);
                nuevaFilaAlumno["fecha_nacimiento"] = dateTimePicker.Value.Date;
                nuevaFilaAlumno["activo"] = true;
                tablaAlumnos.Rows.Add(nuevaFilaAlumno);

                // Obtener el legajo temporal asignado por el DataTable
                int legajoTemporal = Convert.ToInt32(nuevaFilaAlumno["legajo"]);

                DataTable tablaDirecciones = _dataSet.Tables["Direcciones"];
                DataRow nuevaFilaDireccion = tablaDirecciones.NewRow();
                nuevaFilaDireccion["id_legajo"] = legajoTemporal;
                nuevaFilaDireccion["calle_numero"] = txtCalle.Text.Trim();
                nuevaFilaDireccion["ciudad"] = txtCiudad.Text.Trim();
                tablaDirecciones.Rows.Add(nuevaFilaDireccion);

                LimpiarFormulario();
                ActualizarEstado();

                MessageBox.Show("Alumno agregado en memoria. Presione 'Guardar Cambios' para persistir.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un alumno para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!ValidarCampos()) return;

                DataRowView filaVista = (DataRowView)dataGridView1.CurrentRow.DataBoundItem;
                DataRow filaAlumno = filaVista.Row;
                int legajo = Convert.ToInt32(filaAlumno["legajo"]);

                filaAlumno.BeginEdit();
                filaAlumno["nombre_apellido"] = txtNombre.Text.Trim();
                filaAlumno["documento"] = Convert.ToInt32(txtDocumento.Text);
                filaAlumno["fecha_nacimiento"] = dateTimePicker.Value.Date;
                filaAlumno.EndEdit();

                // Modificar direccion relacionada
                DataRow[] direcciones = _dataSet.Tables["Direcciones"].Select($"id_legajo = {legajo}");
                if (direcciones.Length > 0)
                {
                    direcciones[0].BeginEdit();
                    direcciones[0]["calle_numero"] = txtCalle.Text.Trim();
                    direcciones[0]["ciudad"] = txtCiudad.Text.Trim();
                    direcciones[0].EndEdit();
                }

                ActualizarEstado();
                MessageBox.Show("Alumno modificado en memoria. Presione 'Guardar Cambios' para persistir.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un alumno para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView filaVista = (DataRowView)dataGridView1.CurrentRow.DataBoundItem;
                string nombre = filaVista["nombre_apellido"].ToString();
                int legajo = Convert.ToInt32(filaVista["legajo"]);

                var confirmacion = MessageBox.Show(
                    $"¿Está seguro de eliminar al alumno: {nombre}?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    // Marcar direccion como eliminada primero
                    DataRow[] direcciones = _dataSet.Tables["Direcciones"].Select($"id_legajo = {legajo}");
                    foreach (DataRow dir in direcciones)
                        dir.Delete();

                    // Marcar alumno como eliminado
                    filaVista.Row.Delete();

                    LimpiarFormulario();
                    ActualizarEstado();

                    MessageBox.Show("Alumno marcado para eliminar. Presione 'Guardar Cambios' para persistir.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Persistencia

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmacion = MessageBox.Show(
                    "¿Desea guardar todos los cambios pendientes en la base de datos?",
                    "Confirmar guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    _dataAccess.GuardarCambios(_dataSet);
                    ActualizarEstado();
                    MessageBox.Show("Cambios guardados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDescartar_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "¿Desea descartar todos los cambios pendientes?",
                "Confirmar descarte", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                _dataSet.RejectChanges();
                _dataView.RowFilter = string.Empty;
                LimpiarFormulario();
                ActualizarEstado();
            }
        }

        #endregion
        #region Seleccion en grilla
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
                CargarFormularioDesdeGrilla();
        }
        #endregion
    }
}