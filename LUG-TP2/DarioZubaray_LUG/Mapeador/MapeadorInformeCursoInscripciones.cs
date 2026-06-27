using System;
using System.Collections.Generic;
using System.Data;

using AccesoDatos;
using EntidadesNegocio;

namespace Mapeador
{
    public class MapeadorInformeCursoInscripciones
    {
        AccesoDAL acceso;

        public MapeadorInformeCursoInscripciones()
        {
            this.acceso = new AccesoDAL();
        }

        public List<InformeCursoInscripcionesBE> Map()
        {
            List<InformeCursoInscripcionesBE> lista = new List<InformeCursoInscripcionesBE>();

            string consulta = @"SELECT TOP 1 C.nombre, COUNT(I.id_curso) as Cantidad
                                FROM Curso C
                                INNER JOIN Inscripcion I ON C.id_curso = I.id_curso
                                GROUP BY C.nombre
                                ORDER BY Cantidad DESC";

            DataTable tabla = this.acceso.Leer(consulta);
            foreach (DataRow row in tabla.Rows)
            {
                lista.Add(new InformeCursoInscripcionesBE
                {
                    NombreCurso = row["nombre"].ToString(),
                    CantidadInscriptos = Convert.ToInt32(row["Cantidad"])
                });
            }
            return lista;
        }
    }
}
