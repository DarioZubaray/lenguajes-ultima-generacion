using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{

    public class AccesoSP
    {
        #region Atributos
        private readonly string cadenaConexion;
        #endregion

        #region Constructor
        public AccesoSP()
        {
            this.cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ToString();
        }
        #endregion

        #region Metodos privados
        private SqlConnection CrearConexion()
        {
            return new SqlConnection(this.cadenaConexion);
        }

        private SqlCommand CrearComando(string nombreSP, SqlConnection conexion, SqlParameter[] parametros)
        {
            SqlCommand cmd = new SqlCommand(nombreSP, conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parametros != null)
            {
                cmd.Parameters.AddRange(parametros);
            }

            return cmd;
        }
        #endregion

        #region Metodos publicos

        public DataTable Leer(string nombreSP, SqlParameter[] parametros = null)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    SqlCommand cmd = CrearComando(nombreSP, conexion, parametros);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tabla);
                }
            }
            catch
            {
                throw;
            }

            return tabla;
        }

        public bool Ejecutar(string nombreSP, SqlParameter[] parametros = null)
        {
            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = CrearComando(nombreSP, conexion, parametros);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch
            {
                throw;
            }
        }

        public int EjecutarConOutput(string nombreSP, SqlParameter[] parametros, string nombreParametroOutput)
        {
            try
            {
                using (SqlConnection conexion = CrearConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = CrearComando(nombreSP, conexion, parametros);
                    cmd.ExecuteNonQuery();

                    return Convert.ToInt32(cmd.Parameters[nombreParametroOutput].Value);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}