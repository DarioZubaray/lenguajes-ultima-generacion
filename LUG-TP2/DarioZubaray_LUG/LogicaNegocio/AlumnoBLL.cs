using System.Collections.Generic;

using Abstraccion;
using EntidadesNegocio;
using Mapeador;

namespace LogicaNegocio
{
    public class AlumnoBLL : IGestor<AlumnoBE>
    {
        private MapeadorAlumnos mapeador;

        public AlumnoBLL()
        {
            mapeador = new MapeadorAlumnos();
        }

        public bool Baja(AlumnoBE alumno)
        {
            return mapeador.Baja(alumno);
        }

        public bool Guardar(AlumnoBE alumno)
        {
            return mapeador.Guardar(alumno);
        }

        public AlumnoBE ListarObjeto(AlumnoBE objeto)
        {
            throw new System.NotImplementedException();
        }

        public List<AlumnoBE> ListarTodo()
        {
            return mapeador.ListarTodo();
        }
    }
}
