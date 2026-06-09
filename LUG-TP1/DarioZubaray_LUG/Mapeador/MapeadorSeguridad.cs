using AccesoDatos;

namespace Mapeador
{
    public class MapeadorSeguridad
    {
        AccesoDAL acceso;

        public MapeadorSeguridad()
        {
            this.acceso = new AccesoDAL();
        }

        public bool Login(string user, string pass)
        {
            string consulta = $"SELECT COUNT(*) FROM Usuario WHERE nombre_usuario='{user}' AND clave_hash='{pass}'";
            return this.acceso.LeerScalar(consulta) > 0;
        }
    }
}
