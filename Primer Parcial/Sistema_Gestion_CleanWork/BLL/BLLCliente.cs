using System.Collections.Generic;
using System.Data;

using BE;
using MPP;

namespace BLL
{
    public class BLLCliente
    {
        MPPCliente mapper;

        public BLLCliente()
        {
            mapper = new MPPCliente();
        }

        public List<BECliente> ListarTodo()
        {
            return mapper.ListarTodo();
        }

        public DataTable ListarClientesMayorDescuentos()
        {
            return mapper.ListarClientesConMayorDescuento();
        }
    }
}
