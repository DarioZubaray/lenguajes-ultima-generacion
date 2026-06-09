using BE;
using MPP;
using System.Collections.Generic;

namespace BLL
{
    public class BLLServicioLimpiezaAlfombras : BLLServicio
    {
        MPPServicioLimpiezaAlfombras mapper;

        public BLLServicioLimpiezaAlfombras()
        {
            mapper = new MPPServicioLimpiezaAlfombras();
        }

        public decimal DescuentoCalculado(BEServicio servicio)
        {
            // el cliente que contrata una limpieza de alfombras recibe un descuento del 10 %
            return servicio.PrecioBase * 0.9m;
        }

        public List<BEServicio> ListarPorCodigoCliente(int codigo)
        {
            return mapper.ListarPorCodigoCliente(codigo);
        }
    }
}
