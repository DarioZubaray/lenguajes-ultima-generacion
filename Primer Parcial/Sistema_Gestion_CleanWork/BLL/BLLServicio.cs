using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using BE;
using MPP;

namespace BLL
{
    public class BLLServicio
    {
        //public abstract decimal DescuentoCalculado(BEServicio servicio);
        MPPServicioLimpiezaAlfombras mapperLimpiezaAlfombras;
        MPPServicioLimpiezaVidriosAltura mapperLimpiezaVidriosAltura;

        public BLLServicio()
        {
            mapperLimpiezaAlfombras = new MPPServicioLimpiezaAlfombras();
            mapperLimpiezaVidriosAltura = new MPPServicioLimpiezaVidriosAltura();
        }

        public bool Baja(BEServicio servicio)
        {
            if (servicio is BEServicioLimpiezaAlfombras)
            {
                return mapperLimpiezaAlfombras.Baja(servicio);
            }
            else if (servicio is BEServicioLimpiezaVidriosAltura)
            {
                return mapperLimpiezaVidriosAltura.Baja(servicio);
            }
            else
            {
                throw new Exception("Tipo de servicio no soportado");
            }
        }

        public bool Guardar(BEServicio servicio)
        {
            if (servicio is BEServicioLimpiezaAlfombras)
            {
                return mapperLimpiezaAlfombras.Guardar(servicio);
            }
            else if (servicio is BEServicioLimpiezaVidriosAltura)
            {
                return mapperLimpiezaVidriosAltura.Guardar(servicio);
            }
            else
            {
                throw new Exception("Tipo de servicio no soportado");
            }
        }

        public List<BEServicio> ListarTodo()
        {
            var list1 = mapperLimpiezaAlfombras.ListarTodo();
            var list2 = mapperLimpiezaVidriosAltura.ListarTodo();

            return list1.Union(list2).ToList();
        }

        public DataTable ListarLimpiezaServicioAlfombrasMasVendidoPorcuadrilla()
        {
            return mapperLimpiezaAlfombras.ListarLimpiezaServicioAlfombrasMasVendidoPorcuadrilla();
        }

        public DataTable ListarLimpiezaServicioVidriosAlturaMenosVendidoPorCuadrilla()
        {
            return mapperLimpiezaVidriosAltura.ListarLimpiezaVidriosAlturaMenosVendidoPorcuadrilla();
        }

    }
}
