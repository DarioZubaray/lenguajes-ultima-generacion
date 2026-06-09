using System;
using System.Collections.Generic;
using System.Windows.Forms;

using EntidadesNegocio;
using LogicaNegocio;

namespace Presentacion
{
    public partial class FormInscripcion : Form
    {

        private AlumnoBLL _alumnoBLL;
        private List<AlumnoBE> _listaAlumnos;
        private CursoBLL _cursosBLL;
        private List<CursoBE> _listaCursos;
        private InscripcionBLL _inscripcionBLL;
        private List<InscripcionBE> _listaIncripciones;
        bool alumnoSeleccionado = false;
        bool cursoSeleccionado = false;
        int alumnoLegajo = 0;
        int cursoId = 0;

        public FormInscripcion()
        {
            InitializeComponent();
            _alumnoBLL = new AlumnoBLL();
            _cursosBLL = new CursoBLL();
            _inscripcionBLL = new InscripcionBLL();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.MultiSelect = false;
            dataGridView3.ReadOnly = true;

            CargarGrillaAlumnos();
            CargarGrillaCursos();
            CargarGrillaInscripciones();
        }

        #region Metodo auxiliares
        private void CargarGrillaAlumnos()
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
        private void CargarGrillaCursos()
        {
            try
            {
                _listaCursos = _cursosBLL.ListarTodo();

                dataGridView2.DataSource = _listaCursos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrillaInscripciones()
        {
            try
            {
                _listaIncripciones = _inscripcionBLL.ListarTodo();
                dataGridView3.DataSource = _listaIncripciones;
                dataGridView3.Columns["Codigo"].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Eventos
        private void btnInscribir_Click(object sender, EventArgs e)
        {
            if (alumnoSeleccionado && cursoSeleccionado)
            {
                string mensaje = string.Format($"¿Desea inscribir a {txtAlumno.Text} al curso {txtCurso.Text}?");
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    InscripcionBE nuevaInscripcion = new InscripcionBE()
                    {
                        Legajo = alumnoLegajo,
                        IdCurso = cursoId
                    };
                    _inscripcionBLL.Guardar(nuevaInscripcion);
                    CargarGrillaInscripciones();
                }
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null)
            {
                var seleccionado = (InscripcionBE)dataGridView3.CurrentRow.DataBoundItem;

                DataGridViewRow fila = dataGridView3.SelectedRows[0];
                var alumnoNombre = fila.Cells["AlumnoNombre"].Value.ToString();
                var cursoNombre = fila.Cells["CursoNombre"].Value.ToString();

                string mensaje = string.Format($"¿Desea desuscribir a {alumnoNombre} al curso {cursoNombre}?");
                DialogResult result = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    InscripcionBE nuevaInscripcion = new InscripcionBE()
                    {
                        Legajo = seleccionado.Legajo,
                        IdCurso = seleccionado.IdCurso
                    };
                    _inscripcionBLL.Baja(nuevaInscripcion);

                    CargarGrillaInscripciones();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    alumnoLegajo = Convert.ToInt32(fila.Cells["Codigo"].Value);
                    txtAlumno.Text = fila.Cells["NombreApellido"].Value.ToString();
                }
                alumnoSeleccionado = true;
            }
            else
            {
                alumnoSeleccionado = false;
                txtAlumno.Clear();
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView2.SelectedRows[0];

                if (fila.Cells["Codigo"].Value != null)
                {
                    cursoId = Convert.ToInt32(fila.Cells["Codigo"].Value);
                    txtCurso.Text = fila.Cells["Nombre"].Value.ToString();
                }
                cursoSeleccionado = true;
            }
            else
            {
                cursoSeleccionado = false;
                txtCurso.Clear();
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dataGridView3.SelectedRows[0];
                //txtAlumno.Text = fila.Cells["AlumnoNombre"].Value.ToString();
                //txtCurso.Text = fila.Cells["CursoNombre"].Value.ToString();
            }
        }
        #endregion
    }
}
