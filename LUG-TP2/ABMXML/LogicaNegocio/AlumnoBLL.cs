using System.Collections.Generic;

using EntidadesNegocio;
using Persistencia;

namespace LogicaNegocio
{
    public class AlumnoBLL
    {
        #region Atributos
        private AlumnoXML _persistencia;
        #endregion

        #region Constructor
        public AlumnoBLL()
        {
            _persistencia = new AlumnoXML();
        }
        #endregion

        #region Metodos publicos

        public List<AlumnoBE> ObtenerTodos()
        {
            try
            {
                return _persistencia.ObtenerTodos();
            }
            catch
            {
                throw;
            }
        }

        public AlumnoBE ObtenerPorLegajo(int legajo)
        {
            try
            {
                return _persistencia.ObtenerPorLegajo(legajo);
            }
            catch
            {
                throw;
            }
        }

        public List<AlumnoBE> Buscar(string texto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(texto))
                    return _persistencia.ObtenerTodos();

                return _persistencia.Buscar(texto);
            }
            catch
            {
                throw;
            }
        }

        public void Guardar(AlumnoBE alumno)
        {
            try
            {
                ValidarAlumno(alumno);

                if (alumno.Legajo == 0)
                    _persistencia.Insertar(alumno);
                else
                    _persistencia.Actualizar(alumno);
            }
            catch
            {
                throw;
            }
        }

        public void Eliminar(int legajo)
        {
            try
            {
                _persistencia.Eliminar(legajo);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Validaciones
        private void ValidarAlumno(AlumnoBE alumno)
        {
            if (string.IsNullOrWhiteSpace(alumno.NombreApellido))
                throw new System.Exception("El nombre y apellido es obligatorio.");

            if (alumno.Documento <= 0)
                throw new System.Exception("El documento debe ser un número positivo.");

            if (string.IsNullOrWhiteSpace(alumno.CalleNumero))
                throw new System.Exception("La calle y número es obligatoria.");

            if (string.IsNullOrWhiteSpace(alumno.Ciudad))
                throw new System.Exception("La ciudad es obligatoria.");
        }
        #endregion
    }
}