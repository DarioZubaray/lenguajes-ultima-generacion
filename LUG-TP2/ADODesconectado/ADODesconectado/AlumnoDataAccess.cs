using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class AlumnoDataAccess
    {
        #region Atributos
        private readonly string cadenaConexion;
        private SqlDataAdapter adaptadorAlumno;
        private SqlDataAdapter adaptadorDireccion;
        #endregion

        #region Constructor
        public AlumnoDataAccess()
        {
            this.cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ToString();
        }
        #endregion

        #region Carga desconectada
        public DataSet CargarAlumnos()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Tabla Alumnos
                    string sqlAlumnos = @"SELECT legajo, nombre_apellido, documento, 
                                                 fecha_nacimiento, activo
                                          FROM Alumno
                                          WHERE activo = 1";

                    adaptadorAlumno = new SqlDataAdapter(sqlAlumnos, conexion);
                    adaptadorAlumno.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    ConfigurarComandosAlumno(adaptadorAlumno);
                    adaptadorAlumno.Fill(ds, "Alumnos");

                    // Tabla Direcciones
                    string sqlDirecciones = @"SELECT id_legajo, calle_numero, ciudad
                                              FROM Direccion";

                    adaptadorDireccion = new SqlDataAdapter(sqlDirecciones, conexion);
                    adaptadorDireccion.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    ConfigurarComandosDireccion(adaptadorDireccion);
                    adaptadorDireccion.Fill(ds, "Direcciones");

                    // Relacion entre tablas en memoria
                    if (!ds.Relations.Contains("AlumnoDireccion"))
                    {
                        ds.Relations.Add("AlumnoDireccion",
                            ds.Tables["Alumnos"].Columns["legajo"],
                            ds.Tables["Direcciones"].Columns["id_legajo"],
                            createConstraints: false);
                    }
                }
            }
            catch
            {
                throw;
            }

            return ds;
        }

        #endregion

        #region Comandos manuales

        private void ConfigurarComandosAlumno(SqlDataAdapter da)
        {
            // INSERT
            da.InsertCommand = new SqlCommand(@"
                INSERT INTO Alumno (nombre_apellido, documento, fecha_nacimiento, activo)
                VALUES (@nombre, @documento, @fecha_nacimiento, @activo);
                SELECT SCOPE_IDENTITY();");
            da.InsertCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 200, "nombre_apellido");
            da.InsertCommand.Parameters.Add("@documento", SqlDbType.Int, 0, "documento");
            da.InsertCommand.Parameters.Add("@fecha_nacimiento", SqlDbType.Date, 0, "fecha_nacimiento");
            da.InsertCommand.Parameters.Add("@activo", SqlDbType.Bit, 0, "activo");

            // UPDATE
            da.UpdateCommand = new SqlCommand(@"
                UPDATE Alumno 
                SET nombre_apellido  = @nombre,
                    documento        = @documento,
                    fecha_nacimiento = @fecha_nacimiento
                WHERE legajo = @legajo");
            da.UpdateCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 200, "nombre_apellido");
            da.UpdateCommand.Parameters.Add("@documento", SqlDbType.Int, 0, "documento");
            da.UpdateCommand.Parameters.Add("@fecha_nacimiento", SqlDbType.Date, 0, "fecha_nacimiento");
            da.UpdateCommand.Parameters.Add("@legajo", SqlDbType.Int, 0, "legajo");

            // DELETE
            da.DeleteCommand = new SqlCommand(@"
                UPDATE Alumno SET activo = 0 WHERE legajo = @legajo");
            da.DeleteCommand.Parameters.Add("@legajo", SqlDbType.Int, 0, "legajo");
        }

        private void ConfigurarComandosDireccion(SqlDataAdapter da)
        {
            // INSERT
            da.InsertCommand = new SqlCommand(@"
                INSERT INTO Direccion (id_legajo, calle_numero, ciudad)
                VALUES (@id_legajo, @calle_numero, @ciudad)");
            da.InsertCommand.Parameters.Add("@id_legajo", SqlDbType.Int, 0, "id_legajo");
            da.InsertCommand.Parameters.Add("@calle_numero", SqlDbType.VarChar, 250, "calle_numero");
            da.InsertCommand.Parameters.Add("@ciudad", SqlDbType.VarChar, 100, "ciudad");

            // UPDATE
            da.UpdateCommand = new SqlCommand(@"
                UPDATE Direccion
                SET calle_numero = @calle_numero,
                    ciudad       = @ciudad
                WHERE id_legajo  = @id_legajo");
            da.UpdateCommand.Parameters.Add("@calle_numero", SqlDbType.VarChar, 250, "calle_numero");
            da.UpdateCommand.Parameters.Add("@ciudad", SqlDbType.VarChar, 100, "ciudad");
            da.UpdateCommand.Parameters.Add("@id_legajo", SqlDbType.Int, 0, "id_legajo");

            // DELETE
            da.DeleteCommand = new SqlCommand(@"
                DELETE FROM Direccion WHERE id_legajo = @id_legajo");
            da.DeleteCommand.Parameters.Add("@id_legajo", SqlDbType.Int, 0, "id_legajo");
        }

        #endregion

        #region Persistencia
        public void GuardarCambios(DataSet ds)
        {
            if (ds == null || !ds.HasChanges())
                return;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    AsignarConexion(adaptadorAlumno, conexion, transaccion);
                    AsignarConexion(adaptadorDireccion, conexion, transaccion);

                    DataTable deletedAlumnos = ds.Tables["Alumnos"].GetChanges(DataRowState.Deleted);
                    DataTable deletedDirecciones = ds.Tables["Direcciones"].GetChanges(DataRowState.Deleted);
                    DataTable changedAlumnos = ds.Tables["Alumnos"].GetChanges(DataRowState.Added | DataRowState.Modified);
                    DataTable changedDirecciones = ds.Tables["Direcciones"].GetChanges(DataRowState.Added | DataRowState.Modified);

                    if (deletedDirecciones != null)
                        adaptadorDireccion.Update(deletedDirecciones);

                    if (deletedAlumnos != null)
                        adaptadorAlumno.Update(deletedAlumnos);

                    if (changedAlumnos != null)
                        adaptadorAlumno.Update(changedAlumnos);

                    if (changedDirecciones != null)
                        adaptadorDireccion.Update(changedDirecciones);

                    transaccion.Commit();
                    ds.AcceptChanges();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        private void AsignarConexion(SqlDataAdapter da, SqlConnection conexion, SqlTransaction transaccion)
        {
            if (da.InsertCommand != null) { da.InsertCommand.Connection = conexion; da.InsertCommand.Transaction = transaccion; }
            if (da.UpdateCommand != null) { da.UpdateCommand.Connection = conexion; da.UpdateCommand.Transaction = transaccion; }
            if (da.DeleteCommand != null) { da.DeleteCommand.Connection = conexion; da.DeleteCommand.Transaction = transaccion; }
        }

        #endregion
    }
}