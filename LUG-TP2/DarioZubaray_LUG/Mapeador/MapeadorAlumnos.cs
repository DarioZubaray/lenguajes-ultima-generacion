using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Abstraccion;
using AccesoDatos;
using EntidadesNegocio;

namespace Mapeador
{
    public class MapeadorAlumnos : IGestor<AlumnoBE>
    {
        #region Atributos
        private AccesoSP acceso;
        #endregion

        #region Constructor
        public MapeadorAlumnos()
        {
            this.acceso = new AccesoSP();
        }
        #endregion

        #region Metodos privados
        private AlumnoBE MapearFila(DataRow fila)
        {
            AlumnoBE alumno = new AlumnoBE();
            alumno.Codigo = Convert.ToInt32(fila["legajo"]);
            alumno.NombreApellido = fila["nombre_apellido"].ToString();
            alumno.Documento = Convert.ToInt32(fila["documento"]);
            alumno.Nacimiento = Convert.ToDateTime(fila["fecha_nacimiento"]);

            DireccionBE direccion = new DireccionBE();
            direccion.CalleNumero = fila["calle_numero"] != DBNull.Value ? fila["calle_numero"].ToString() : string.Empty;
            direccion.Ciudad = fila["ciudad"] != DBNull.Value ? fila["ciudad"].ToString() : string.Empty;

            alumno.Direccion = direccion;
            return alumno;
        }
        #endregion

        #region IGestor

        public List<AlumnoBE> ListarTodo()
        {
            List<AlumnoBE> lista = new List<AlumnoBE>();

            try
            {
                DataTable tabla = this.acceso.Leer("sp_AlumnoObtenerTodos");

                foreach (DataRow fila in tabla.Rows)
                {
                    lista.Add(MapearFila(fila));
                }
            }
            catch
            {
                throw;
            }

            return lista;
        }

        public AlumnoBE ListarObjeto(AlumnoBE objeto)
        {
            AlumnoBE alumno = new AlumnoBE();

            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@legajo", SqlDbType.Int) { Value = objeto.Codigo }
                };

                DataTable tabla = this.acceso.Leer("sp_AlumnoObtenerPorLegajo", parametros);

                if (tabla.Rows.Count > 0)
                {
                    alumno = MapearFila(tabla.Rows[0]);
                }
            }
            catch
            {
                throw;
            }

            return alumno;
        }

        public bool Guardar(AlumnoBE objeto)
        {
            try
            {
                if (objeto.Codigo == 0)
                {
                    // Alta: el SP devuelve el nuevo legajo via parametro OUTPUT
                    SqlParameter paramOutput = new SqlParameter("@nuevo_legajo", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter[] parametros = new SqlParameter[]
                    {
                        new SqlParameter("@nombre_apellido",  SqlDbType.VarChar, 200) { Value = objeto.NombreApellido },
                        new SqlParameter("@documento",        SqlDbType.Int)          { Value = objeto.Documento },
                        new SqlParameter("@fecha_nacimiento", SqlDbType.Date)         { Value = objeto.Nacimiento },
                        new SqlParameter("@calle_numero",     SqlDbType.VarChar, 250) { Value = objeto.Direccion.CalleNumero },
                        new SqlParameter("@ciudad",           SqlDbType.VarChar, 100) { Value = objeto.Direccion.Ciudad },
                        paramOutput
                    };

                    int nuevoLegajo = this.acceso.EjecutarConOutput("sp_AlumnoInsertar", parametros, "@nuevo_legajo");
                    objeto.Codigo = nuevoLegajo;
                    return nuevoLegajo > 0;
                }
                else
                {
                    // Modificacion
                    SqlParameter[] parametros = new SqlParameter[]
                    {
                        new SqlParameter("@legajo",           SqlDbType.Int)          { Value = objeto.Codigo },
                        new SqlParameter("@nombre_apellido",  SqlDbType.VarChar, 200) { Value = objeto.NombreApellido },
                        new SqlParameter("@documento",        SqlDbType.Int)          { Value = objeto.Documento },
                        new SqlParameter("@fecha_nacimiento", SqlDbType.Date)         { Value = objeto.Nacimiento },
                        new SqlParameter("@calle_numero",     SqlDbType.VarChar, 250) { Value = objeto.Direccion.CalleNumero },
                        new SqlParameter("@ciudad",           SqlDbType.VarChar, 100) { Value = objeto.Direccion.Ciudad }
                    };

                    return this.acceso.Ejecutar("sp_AlumnoActualizar", parametros);
                }
            }
            catch
            {
                throw;
            }
        }

        public bool Baja(AlumnoBE objeto)
        {
            try
            {
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@legajo", SqlDbType.Int) { Value = objeto.Codigo }
                };

                return this.acceso.Ejecutar("sp_AlumnoBorrar", parametros);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}