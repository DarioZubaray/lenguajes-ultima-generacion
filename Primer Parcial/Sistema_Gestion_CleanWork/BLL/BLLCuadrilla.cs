using System.Collections.Generic;

using BE;
using MPP;

namespace BLL
{
    public class BLLCuadrilla
    {
        MPPCuadrilla mapper;

        public BLLCuadrilla()
        {
            mapper = new MPPCuadrilla();
        }

        public bool Guardar(BECuadrilla cuadrilla)
        {
            return mapper.Guardar(cuadrilla);
        }

        public List<BECuadrilla> ListarTodo()
        {
            return mapper.ListarTodo();
        }
    }
}
