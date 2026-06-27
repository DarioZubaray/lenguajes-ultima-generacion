using System;
using System.Collections.Generic;

using Abstraccion;
using EntidadesNegocio;
using Mapeador;

namespace LogicaNegocio
{
    public class CursoBLL : IGestor<CursoBE>
    {
        private MapeadorCursos mapeador;

        public CursoBLL()
        {
            mapeador = new MapeadorCursos();
        }

        public bool Baja(CursoBE objeto)
        {
            return mapeador.Baja(objeto);
        }

        public bool Guardar(CursoBE objeto)
        {
            return mapeador.Guardar(objeto);
        }

        public CursoBE ListarObjeto(CursoBE objeto)
        {
            throw new NotImplementedException();
        }

        public List<CursoBE> ListarTodo()
        {
            return mapeador.ListarTodo();
        }
    }
}
