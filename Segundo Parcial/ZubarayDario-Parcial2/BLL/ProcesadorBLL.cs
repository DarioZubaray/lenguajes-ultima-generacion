using System.Collections.Generic;

using BE;
using MPP;

namespace BLL
{
    public class ProcesadorBLL
    {
        private ProcesadorMPP _mapeador;

        public ProcesadorBLL()
        {
            this._mapeador = new ProcesadorMPP();
        }

        public List<Procesador> ListarTodo()
        {
            return this._mapeador.ListarTodo();
        }
    }
}
