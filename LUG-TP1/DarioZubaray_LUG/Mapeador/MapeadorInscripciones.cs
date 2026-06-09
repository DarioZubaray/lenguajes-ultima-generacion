using System;
using System.Collections.Generic;
using System.Data;

using Abstraccion;
using AccesoDatos;
using EntidadesNegocio;

namespace Mapeador
{
    public class MapeadorInscripciones : IGestor<InscripcionBE>
    {
        AccesoDAL acceso;

        public MapeadorInscripciones()
        {
            acceso = new AccesoDAL();
        }

        public bool Baja(InscripcionBE objeto)
        {

            string consulta = string.Format(@"DELETE FROM Inscripcion WHERE legajo = {0} and id_curso = {1}", objeto.Legajo, objeto.IdCurso);
            return this.acceso.Guardar(consulta);
        }

        public bool Guardar(InscripcionBE objeto)
        {
            string consulta = string.Format(@"INSERT INTO Inscripcion (legajo, id_curso) VALUES ({0}, {1})", objeto.Legajo, objeto.IdCurso);
            return this.acceso.Guardar(consulta);
        }

        public InscripcionBE ListarObjeto(InscripcionBE objeto)
        {
            throw new NotImplementedException();
        }

        public List<InscripcionBE> ListarTodo()
        {
            List<InscripcionBE> listaInscripciones = new List<InscripcionBE>();

            string Consulta = @"SELECT legajo, id_curso, fecha_inscripcion FROM Inscripcion";

            DataTable Tabla = this.acceso.Leer(Consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    InscripcionBE inscripcionBD = new InscripcionBE();
                    inscripcionBD.Legajo = Convert.ToInt32(fila[0]);
                    inscripcionBD.IdCurso = Convert.ToInt32(fila[1]);
                    inscripcionBD.FechaInscripcion = (DateTime)fila[2];
                    listaInscripciones.Add(inscripcionBD);
                }
            }
            return listaInscripciones;
        }
    }
}
