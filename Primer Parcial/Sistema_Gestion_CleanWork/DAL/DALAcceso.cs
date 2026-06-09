using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALAcceso
    {
        private SqlConnection conexion;
        SqlCommand sqlCommand;

        public DALAcceso()
        {
            this.conexion = new SqlConnection();
            this.conexion.ConnectionString = ConfigurationManager.ConnectionStrings["cadenaConexion"].ToString();
        }

        public DataTable Leer(string consulta)
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlDataAdapter Da = new SqlDataAdapter(consulta, this.conexion);
                Da.Fill(tabla);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.conexion.Close();
            }
            return tabla;
        }

        public int LeerScalar(string consulta)
        {
            this.conexion.Open();
            sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = this.conexion;
            sqlCommand.CommandText = consulta;
            try
            {
                int respuesta = Convert.ToInt32(sqlCommand.ExecuteScalar());
                this.conexion.Close();

                return respuesta;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.conexion.Close();
            }
        }

        public bool Escribir(string consulta)
        {
            this.conexion.Open();

            SqlTransaction transaccion = this.conexion.BeginTransaction();

            try
            {
                sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = this.conexion;
                sqlCommand.Transaction = transaccion;
                sqlCommand.CommandText = consulta;

                int respuesta = sqlCommand.ExecuteNonQuery();

                transaccion.Commit();

                return true;
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw ex;
            }
            finally
            {
                this.conexion.Close();
            }
        }
    }
}
