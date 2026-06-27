using System;
using System.Collections.Generic;
using System.Data;

using Abstraccion;
using AccesoDatos;
using EntidadesNegocio;

namespace Mapeador
{
    public class MapeadorAlumnos : IGestor<AlumnoBE>
    {
        AccesoDAL acceso;

        public MapeadorAlumnos()
        {
            this.acceso = new AccesoDAL();
        }

        public bool Baja(AlumnoBE objeto)
        {
            string consulta = string.Format(@"UPDATE Alumno SET activo = 0 WHERE legajo = {0}", objeto.Codigo);
            return this.acceso.Guardar(consulta);
        }

        public bool Guardar(AlumnoBE objeto)
        {
            if (objeto.Codigo == 0)
            {
                string consultaAlumno = string.Format(@"INSERT INTO Alumno(nombre_apellido, documento, fecha_nacimiento, activo) 
                                                VALUES('{0}',{1},'{2}',1);
                                                SELECT SCOPE_IDENTITY();",
                                                objeto.NombreApellido, objeto.Documento, objeto.Nacimiento.ToString("yyyyMMdd"));

                var nuevoId = this.acceso.LeerScalar(consultaAlumno);

                string consultaDireccion = string.Format(@"INSERT INTO Direccion (id_legajo, calle_numero, ciudad) 
                                            VALUES ({0}, '{1}', '{2}')",
                                                    nuevoId, objeto.Direccion.CalleNumero, objeto.Direccion.Ciudad);

                return this.acceso.Guardar(consultaDireccion);
            }
            else
            {
                string consultaAlumno = string.Format(@"UPDATE Alumno SET nombre_apellido='{0}', documento={1}, fecha_nacimiento='{2}' 
                                                    WHERE legajo = {3}",
                                                    objeto.NombreApellido, objeto.Documento, objeto.Nacimiento.ToString("yyyyMMdd"), objeto.Codigo);

                this.acceso.Guardar(consultaAlumno);

                string consultaDireccion = string.Format(@"UPDATE Direccion SET calle_numero='{0}', ciudad='{1}' 
                                                        WHERE id_legajo = {2}",
                                                        objeto.Direccion.CalleNumero, objeto.Direccion.Ciudad, objeto.Codigo);

                return this.acceso.Guardar(consultaDireccion);
            }
        }

        public AlumnoBE ListarObjeto(AlumnoBE objeto)
        {
            string Consulta = string.Format(@"SELECT a.legajo, a.nombre_apellido, a.documento, a.fecha_nacimiento, 
                                        d.calle_numero, d.ciudad 
                                FROM Alumno a
                                INNER JOIN Direccion d ON a.legajo = d.id_legajo
                                WHERE a.activo = 1 and a.legajo = {0}", objeto.Codigo);

            DataTable Tabla = this.acceso.Leer(Consulta);

            AlumnoBE alumnoBD = new AlumnoBE();
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    alumnoBD.Codigo = Convert.ToInt32(fila[0]);
                    alumnoBD.NombreApellido = fila[1].ToString();
                    alumnoBD.Documento = Convert.ToInt32(fila[2]);
                    alumnoBD.Nacimiento = (DateTime)fila[3];

                    DireccionBE direccion = new DireccionBE();
                    direccion.CalleNumero = fila["calle_numero"].ToString();
                    direccion.Ciudad = fila["ciudad"].ToString();

                    alumnoBD.Direccion = direccion;
                }
            }
            return alumnoBD;
        }

        public List<AlumnoBE> ListarTodo()
        {
            List<AlumnoBE> listaAlumnos = new List<AlumnoBE>();

            string Consulta = @"SELECT a.legajo, a.nombre_apellido, a.documento, a.fecha_nacimiento, 
                                        d.calle_numero, d.ciudad 
                                FROM Alumno a
                                INNER JOIN Direccion d ON a.legajo = d.id_legajo
                                WHERE a.activo = 1";

            DataTable Tabla = this.acceso.Leer(Consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    AlumnoBE alumnoBD = new AlumnoBE();
                    alumnoBD.Codigo = Convert.ToInt32(fila[0]);
                    alumnoBD.NombreApellido = fila[1].ToString();
                    alumnoBD.Documento = Convert.ToInt32(fila[2]);
                    alumnoBD.Nacimiento = (DateTime)fila[3];

                    DireccionBE direccion = new DireccionBE();
                    direccion.CalleNumero = fila["calle_numero"].ToString();
                    direccion.Ciudad = fila["ciudad"].ToString();

                    alumnoBD.Direccion = direccion;
                    listaAlumnos.Add(alumnoBD);
                }
            }
            return listaAlumnos;
        }
    }
}
