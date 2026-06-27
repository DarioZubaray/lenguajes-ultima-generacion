namespace EntidadesNegocio
{
    public class InformeCursoInscripcionesBE
    {
        #region Propiedades
        public string NombreCurso { get; set; }
        public int CantidadInscriptos { get; set; }
        #endregion

        #region Constructor
        public InformeCursoInscripcionesBE() { }

        public InformeCursoInscripcionesBE(string pNombreCurso, int pCantidadInscriptos)
        {
            this.NombreCurso = pNombreCurso;
            this.CantidadInscriptos = pCantidadInscriptos;
        }
        #endregion

        public override string ToString()
        {
            return $"{NombreCurso}, {CantidadInscriptos}";
        }
    }
}
