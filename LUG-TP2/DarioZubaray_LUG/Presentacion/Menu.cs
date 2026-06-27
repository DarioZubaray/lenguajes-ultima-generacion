using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlumnos alumnos = new FormAlumnos
            {
                MdiParent = this
            };
            alumnos.Show();
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCursos cursos = new FormCursos
            {
                MdiParent = this
            };
            cursos.Show();
        }

        private void inscripcionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInscripcion inscripcion = new FormInscripcion
            {
                MdiParent = this
            };

            inscripcion.Show();
        }

        private void cursoMásPopularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInformesCursoPopular informe = new FormInformesCursoPopular
            {
                MdiParent = this
            };
            informe.Show();
        
        }
    }
}
