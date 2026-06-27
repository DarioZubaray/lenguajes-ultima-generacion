using System.Collections.Generic;

using EntidadesNegocio;
using Mapeador;

namespace LogicaNegocio
{
    public class InformeCursoInscripcionesBLL
    {
        private MapeadorInformeCursoInscripciones mapper;

        public InformeCursoInscripcionesBLL()
        {
            this.mapper = new MapeadorInformeCursoInscripciones();
        }

        public List<InformeCursoInscripcionesBE> ObtenerCursoMasPopular()
        {
            return mapper.Map();
        }
    }
}
