using System;
using System.Collections.Generic;
using System.Data;

using BE;
using DAL;

namespace MPP
{
    public class MPPCliente
    {
        DALAcceso acceso;

        public MPPCliente()
        {
            acceso = new DALAcceso();
        }

        public List<BECliente> ListarTodo()
        {
            List<BECliente> clientes = new List<BECliente>();

            string Consulta = @"SELECT c.codigo, c.razon_social, c.cuit, c.rubro, c.descuentos
                                FROM Cliente c";

            DataTable Tabla = this.acceso.Leer(Consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    BECliente clienteDB = new BECliente();

                    clienteDB.Codigo = Convert.ToInt32(fila[0]);
                    clienteDB.RazonSocial = fila[1].ToString().Trim();
                    clienteDB.CUIT = fila[2].ToString().Trim();
                    clienteDB.Rubro = fila[3].ToString().Trim();
                    int.TryParse(fila[4].ToString(), out int descuentos);
                    clienteDB.Descuento= descuentos;

                    clientes.Add(clienteDB);
                }
            }
            return clientes;
        }

        public DataTable ListarClientesConMayorDescuento()
        {
            string Consulta = @"SELECT cs.codigo_cliente, SUM(cs.descuentos) AS total_descuentos
                                FROM Cliente_Servicio cs
                                GROUP BY cs.codigo_cliente
                                ORDER BY total_descuentos DESC;";

            return this.acceso.Leer(Consulta);
        }
    }
}
