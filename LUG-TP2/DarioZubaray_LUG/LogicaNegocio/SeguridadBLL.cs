using Mapeador;

namespace LogicaNegocio
{
    public class SeguridadBLL
    {
        MapeadorSeguridad seguridad;

        public SeguridadBLL()
        {
            this.seguridad = new MapeadorSeguridad();
        }

        public bool Login(string user, string pass)
        {
            return seguridad.Login(user, pass);
        }
    }
}
