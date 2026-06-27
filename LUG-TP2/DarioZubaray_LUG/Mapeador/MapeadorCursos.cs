using System;
using System.Collections.Generic;
using System.Data;

using Abstraccion;
using AccesoDatos;
using EntidadesNegocio;

namespace Mapeador
{
    public class MapeadorCursos : IGestor<CursoBE>
    {
        AccesoDAL acceso;

        public MapeadorCursos()
        {
            this.acceso = new AccesoDAL();
        }

        public bool Baja(CursoBE objeto)
        {
            string consulta = string.Format(@"UPDATE Curso SET activo = 0 WHERE id_curso = {0}", objeto.Codigo);
            return this.acceso.Guardar(consulta);
        }

        public bool Guardar(CursoBE objeto)
        {
            decimal? precio = null;
            if (objeto is CursoPagoBE cursoPago)
            {
                precio = cursoPago.Precio;
            }
            string costoSql = precio.HasValue ? precio.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) : "NULL";

            string consulta = "";

            if (objeto.Codigo == 0)
            {
                consulta = string.Format(@"INSERT INTO Curso(nombre, inicio, precio, activo) 
                                           VALUES('{0}', '{1}', {2}, 1);",
                                            objeto.Nombre,
                                            objeto.Inicio.ToString("yyyyMMdd"),
                                            costoSql);

                return this.acceso.Guardar(consulta);
            }
            else
            {
                consulta = string.Format(@"UPDATE Curso SET nombre='{0}', inicio='{1}', precio={2}
                                    WHERE id_curso = {3}",
                                    objeto.Nombre,
                                    objeto.Inicio.ToString("yyyyMMdd"),
                                    costoSql,
                                    objeto.Codigo);

                return this.acceso.Guardar(consulta);
            }
        }

        public CursoBE ListarObjeto(CursoBE objeto)
        {
            string Consulta = string.Format(@"SELECT c.id_curso, c.nombre, c.inicio, c.precio, c.activo
                                FROM Curso c
                                WHERE c.activo = 1 and id_curso = {0}", objeto.Codigo);

            DataTable Tabla = this.acceso.Leer(Consulta);

            CursoPagoBE cursoBD = new CursoPagoBE();
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    cursoBD.Codigo = Convert.ToInt32(fila[0]);
                    cursoBD.Nombre = fila[1].ToString();
                    cursoBD.Inicio = (DateTime)fila[2];
                    int.TryParse(fila[3].ToString(), out int precio);
                    cursoBD.Precio = precio;
                }
            }
            return cursoBD;
        }

        public List<CursoBE> ListarTodo()
        {
            List<CursoBE> listaCursos = new List<CursoBE>();

            string Consulta = @"SELECT c.id_curso, c.nombre, c.inicio, c.precio, c.activo
                                FROM Curso c
                                WHERE c.activo = 1";

            DataTable Tabla = this.acceso.Leer(Consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    CursoPagoBE cursoBD = new CursoPagoBE();
                    cursoBD.Codigo = Convert.ToInt32(fila[0]);
                    cursoBD.Nombre = fila[1].ToString();
                    cursoBD.Inicio = (DateTime)fila[2];
                    int.TryParse(fila[3].ToString(), out int precio);
                    cursoBD.Precio = precio;

                    listaCursos.Add(cursoBD);
                }
            }
            return listaCursos;
        }
    }
}
