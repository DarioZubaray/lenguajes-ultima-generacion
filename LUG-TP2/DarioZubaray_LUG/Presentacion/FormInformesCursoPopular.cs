using System;
using System.Windows.Forms;

using LogicaNegocio;

namespace Presentacion
{
    public partial class FormInformesCursoPopular : Form
    {
        private InformeCursoInscripcionesBLL informes;

        public FormInformesCursoPopular()
        {
            InitializeComponent();
            this.informes = new InformeCursoInscripcionesBLL();

            CargarReporte();
        }

        private void CargarReporte()
        {
            var resultados = informes.ObtenerCursoMasPopular();

            if (resultados.Count > 0)
            {
                var topCurso = resultados[0];
                txtCursoEstrella.Text = topCurso.NombreCurso;
                txtInscriptos.Text = topCurso.CantidadInscriptos.ToString();

                dataGridView1.DataSource = resultados;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }
    }
}
