using System;
using System.ComponentModel;

using Abstraccion;

namespace EntidadesNegocio
{
    public class InscripcionBE : IEntidad
    {
        [Browsable(false)]
        public int Legajo { get; set; }
        [Browsable(false)]
        public int IdCurso { get; set; }

        public DateTime FechaInscripcion { get; set; }
        [Browsable(false)]
        public AlumnoBE Alumno { get; set; }
        [Browsable(false)]
        public CursoBE Curso { get; set; }

        [DisplayName("Nombre del Alumno")]
        public string AlumnoNombre => Alumno?.NombreApellido ?? "Sin asignar";

        [DisplayName("Nombre del Curso")]
        public string CursoNombre => Curso?.Nombre ?? "Sin asignar";
    }
}
