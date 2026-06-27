using System;
using System.Collections.Generic;

using Abstraccion;
using EntidadesNegocio;
using Mapeador;

namespace LogicaNegocio
{
    public class InscripcionBLL : IGestor<InscripcionBE>
    {
        MapeadorAlumnos mapeadorAlumnos;
        MapeadorCursos mapeadorCursos;
        MapeadorInscripciones mapeador;

        public InscripcionBLL()
        {
            mapeadorAlumnos = new MapeadorAlumnos();
            mapeadorCursos = new MapeadorCursos();
            mapeador = new MapeadorInscripciones();
        }

        public bool Baja(InscripcionBE objeto)
        {
            return mapeador.Baja(objeto);
        }

        public bool Guardar(InscripcionBE objeto)
        {
            return mapeador.Guardar(objeto);
        }

        public InscripcionBE ListarObjeto(InscripcionBE objeto)
        {
            throw new NotImplementedException();
        }

        public List<InscripcionBE> ListarTodo()
        {
            List<InscripcionBE> inscripciones = mapeador.ListarTodo();

            foreach(InscripcionBE inscripcion in inscripciones)
            {
                var legajo = inscripcion.Legajo;
                inscripcion.Alumno = mapeadorAlumnos.ListarObjeto(new AlumnoBE { Codigo = legajo });

                var cursoId = inscripcion.IdCurso;
                inscripcion.Curso = mapeadorCursos.ListarObjeto(new CursoBE { Codigo = cursoId });
            }
            return inscripciones;
        }
    }
}
