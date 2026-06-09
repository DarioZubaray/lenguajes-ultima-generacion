using System.Collections.Generic;

using BE;

namespace MPP
{
    public abstract class MPPServicio
    {
        public abstract bool Baja(BEServicio servicio);
        public abstract bool Guardar(BEServicio servicio);
        public abstract List<BEServicio> ListarTodo();

    }
}
