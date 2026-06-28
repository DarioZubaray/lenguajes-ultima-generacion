using System;
using System.Windows.Forms;

namespace Presentacion
{
    /// <summary>
    /// Control de usuario reutilizable para filtrar un DataGridView por texto.
    /// Se usa en FormAlumnos y FormCursos.
    /// </summary>
    public partial class UCBuscador : UserControl
    {
        #region Eventos
        /// <summary>
        /// Se dispara cuando el usuario hace click en Buscar o presiona Enter.
        /// El string del argumento contiene el texto ingresado.
        /// </summary>
        public event EventHandler<string> OnBuscar;
        #endregion

        #region Constructor
        public UCBuscador()
        {
            InitializeComponent();
        }
        #endregion

        #region Propiedades
        public string Placeholder
        {
            get { return lblPlaceholder.Text; }
            set { lblPlaceholder.Text = value; }
        }
        #endregion

        #region Metodos publicos
        public void LimpiarBusqueda()
        {
            txtBuscar.Clear();
        }
        #endregion

        #region Eventos internos
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DispararBusqueda();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DispararBusqueda();
            }
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            lblPlaceholder.Visible = false;
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            lblPlaceholder.Visible = string.IsNullOrEmpty(txtBuscar.Text);
        }

        private void DispararBusqueda()
        {
            OnBuscar?.Invoke(this, txtBuscar.Text.Trim());
        }
        #endregion
    }
}