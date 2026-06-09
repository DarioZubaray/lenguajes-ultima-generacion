using System.Collections.Generic;

using BE;
using MPP;

namespace BLL
{
    public class BLLServicioLimpiezaVidriosAltura : BLLServicio
    {
        MPPServicioLimpiezaVidriosAltura mapper;

        public BLLServicioLimpiezaVidriosAltura()
        {
            mapper = new MPPServicioLimpiezaVidriosAltura();
        }

        public decimal DescuentoCalculado(BEServicio servicio)
        {
            // y si contrata una limpieza de vidrios en altura tiene un descuento del 20 %
            return servicio.PrecioBase * 0.80m;
        }

        public List<BEServicio> ListarPorCodigoCliente(int codigo)
        {
            return mapper.ListarPorCodigoCliente(codigo);
        }
    }
}
