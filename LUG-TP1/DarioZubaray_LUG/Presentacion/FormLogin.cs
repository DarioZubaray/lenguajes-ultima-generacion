using System;
using System.Windows.Forms;

using LogicaNegocio;
using Seguridad;

namespace Presentacion
{
    public partial class FormLogin : Form
    {
        SeguridadBLL seguridad;

        public FormLogin()
        {
            InitializeComponent();
            seguridad = new SeguridadBLL();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pass = Encriptacion.HashPassword(txtPass.Text);

            if (seguridad.Login(user, pass))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }
    }
}
