using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using EntidadesNegocio;

namespace AccesoDatos
{
    public class AccesoDAL
    {
        private SqlConnection conexion;
        SqlCommand sqlCommand;

        public AccesoDAL()
        {
            this.conexion = new SqlConnection();
            this.conexion.ConnectionString = ConfigurationManager.ConnectionStrings["cadenaConexion"].ToString();
        }

        #region Metodos Genericos
        public DataTable Leer(string consulta)
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlDataAdapter Da = new SqlDataAdapter(consulta, this.conexion);
                Da.Fill(tabla);
            }
            catch
            {
                throw;
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
            catch
            {
                throw;
            }
            finally
            {
                this.conexion.Close();
            }
        }

        public bool Guardar(string consulta)
        {
            this.conexion.Open();
            sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = this.conexion;
            sqlCommand.CommandText = consulta;
            try
            {
                int respuesta = sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                throw;
            }
            finally
            { 
                this.conexion.Close();
            }
        }
        #endregion

        #region Metodos auxiliares
        public void AbrirConexion()
        {
            this.conexion = new SqlConnection();
            this.conexion.ConnectionString = @"Server =.\SQLEXPRESS; Database = GestionAlumnos; User Id = sa; Password = lenguagesultimageneracion; TrustServerCertificate = True; ";
            this.conexion.Open();
        }

        public void CerrarConexion()
        {
            this.conexion.Close();
            this.conexion.Dispose();
            this.conexion = null;
            GC.Collect();
        }
        #endregion

        #region Metodos individuales
        public List<AlumnoBE> LeerAlumnos()
        {
            List<AlumnoBE> alumnos = new List<AlumnoBE>();

            AbrirConexion();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"SELECT a.legajo, a.nombre_apellido, a.documento, a.fecha_nacimiento, 
                                        d.calle_numero, d.ciudad 
                                FROM Alumno a
                                INNER JOIN Direccion d ON a.legajo = d.id_legajo
                                WHERE a.activo = 1";

            cmd.Connection = this.conexion;

            SqlDataReader lector = cmd.ExecuteReader();

            while(lector.Read())
            {
                AlumnoBE alumno = new AlumnoBE();

                alumno.Codigo = Convert.ToInt32(lector[0]);
                alumno.NombreApellido = lector[1].ToString();
                alumno.Documento = Convert.ToInt32(lector[2]);
                alumno.Nacimiento = (DateTime)lector[3];
                
                DireccionBE direccion = new DireccionBE();
                direccion.CalleNumero = lector["calle_numero"].ToString();
                direccion.Ciudad = lector["ciudad"].ToString();

                alumno.Direccion = direccion;

                alumnos.Add(alumno);
            }

            lector.Close();
            CerrarConexion();

            return alumnos;
        }

        public void AltaAlumno(AlumnoBE alumno)
        {
            AbrirConexion();
            SqlTransaction transaccion = this.conexion.BeginTransaction();

            try
            {
                SqlCommand cmdAlumno = new SqlCommand();

                cmdAlumno.CommandType = CommandType.Text;
                cmdAlumno.Transaction = transaccion;

                cmdAlumno.CommandText = @"INSERT INTO Alumno (nombre_apellido, documento, fecha_nacimiento, activo) 
                             VALUES (@nombre, @dni, @nacimiento, 1);
                             SELECT SCOPE_IDENTITY();";
                cmdAlumno.Parameters.AddWithValue("@nombre", alumno.NombreApellido);
                cmdAlumno.Parameters.AddWithValue("@dni", alumno.Documento);
                cmdAlumno.Parameters.AddWithValue("@nacimiento", alumno.Nacimiento);

                cmdAlumno.Connection = this.conexion;

                int nuevoId = Convert.ToInt32(cmdAlumno.ExecuteScalar());

                SqlCommand cmdDireccion = new SqlCommand();

                cmdDireccion.CommandType = CommandType.Text;
                cmdDireccion.Transaction = transaccion;

                cmdDireccion.CommandText = @"INSERT INTO Direccion (id_legajo, calle_numero, ciudad) 
                                VALUES (@id, @calle, @ciudad)";
                cmdDireccion.Parameters.AddWithValue("@id", nuevoId);
                cmdDireccion.Parameters.AddWithValue("@calle", alumno.Direccion.CalleNumero);
                cmdDireccion.Parameters.AddWithValue("@ciudad", alumno.Direccion.Ciudad);

                cmdDireccion.Connection = this.conexion;

                cmdDireccion.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al crear el alumno: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void BajaAlumno(int legajo)
        {
            try
            {
                AbrirConexion();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Alumno SET activo = 0 WHERE legajo = @legajo";
                cmd.Parameters.AddWithValue("@legajo", legajo);
                cmd.Connection = this.conexion;

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró un alumno con ese legajo.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dar de baja: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void ModificacionAlumno(AlumnoBE alumno)
        {
            AbrirConexion();
            SqlTransaction transaccion = this.conexion.BeginTransaction();
            try
            {
                SqlCommand cmdAlumno = new SqlCommand();

                cmdAlumno.CommandType = CommandType.Text;
                cmdAlumno.Transaction = transaccion;

                cmdAlumno.CommandText = @"UPDATE Alumno 
                             SET nombre_apellido = @nombre, 
                                 documento = @dni, 
                                 fecha_nacimiento = @nacimiento 
                             WHERE legajo = @legajo";
                cmdAlumno.Parameters.AddWithValue("@nombre", alumno.NombreApellido);
                cmdAlumno.Parameters.AddWithValue("@dni", alumno.Documento);
                cmdAlumno.Parameters.AddWithValue("@nacimiento", alumno.Nacimiento);
                cmdAlumno.Parameters.AddWithValue("@legajo", alumno.Codigo);

                cmdAlumno.Connection = this.conexion;

                cmdAlumno.ExecuteNonQuery();

                SqlCommand cmdDireccion = new SqlCommand();

                cmdDireccion.CommandType = CommandType.Text;
                cmdDireccion.Transaction = transaccion;

                cmdDireccion.CommandText = @"UPDATE d
                                SET d.calle_numero = @calle, 
                                    d.ciudad = @ciudad
                                FROM Direccion d
                                INNER JOIN Alumno a ON d.id_legajo = a.legajo
                                WHERE a.legajo = @legajo";
                cmdDireccion.Parameters.AddWithValue("@calle", alumno.Direccion.CalleNumero);
                cmdDireccion.Parameters.AddWithValue("@ciudad", alumno.Direccion.Ciudad);
                cmdDireccion.Parameters.AddWithValue("@legajo", alumno.Codigo);

                cmdDireccion.Connection = this.conexion;

                cmdDireccion.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al modificar el alumno: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }
        #endregion
    }
}
