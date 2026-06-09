using System;
using System.Collections.Generic;
using System.Data;

using BE;
using DAL;

namespace MPP
{
    public class MPPCuadrilla
    {
        DALAcceso acceso;

        public MPPCuadrilla()
        {
            acceso = new DALAcceso();
        }

        public bool Guardar(BECuadrilla cuadrilla)
        {
            if (cuadrilla.Codigo == 0)
            {
                string consulta = string.Format(@"INSERT INTO Cuadrilla(nombre_supervisor, turno_trabajo, cantidad_operarios) 
                                                    VALUES('{0}', '{1}', {2});",
                                                    cuadrilla.NombreSupervisor,
                                                    cuadrilla.TurnoTrabajo,
                                                    cuadrilla.CantidadOperarios);

                return this.acceso.Escribir(consulta);
            }
            else
            {
                throw new Exception("Cuadrilla Update NOT Implemented.");
            }
        }

        public List<BECuadrilla> ListarTodo()
        {
            List<BECuadrilla> cuadrillas = new List<BECuadrilla>();

            string Consulta = @"SELECT c.codigo, c.nombre_supervisor, c.turno_trabajo, c.cantidad_operarios
                                FROM Cuadrilla c";

            DataTable Tabla = this.acceso.Leer(Consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    BECuadrilla cuadrillaBD = new BECuadrilla();

                    cuadrillaBD.Codigo = Convert.ToInt32(fila[0]);
                    cuadrillaBD.NombreSupervisor = fila[1].ToString().Trim();
                    cuadrillaBD.TurnoTrabajo = fila[2].ToString().Trim();
                    int.TryParse(fila[3].ToString(), out int cantidadOperarios);
                    cuadrillaBD.CantidadOperarios = cantidadOperarios;

                    cuadrillas.Add(cuadrillaBD);
                }
            }
            return cuadrillas;
        }
    }
}
