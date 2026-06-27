using Abstraccion;

namespace EntidadesNegocio
{
    public class DireccionBE : IEntidad
    {
        #region Propiedades
        public string CalleNumero { get; set; }
        public string Ciudad { get; set; }
        #endregion

        #region Constructores
        public DireccionBE() { }

        public DireccionBE(string pCalleNumero, string pCiudad)
        {
            CalleNumero = pCalleNumero;
            Ciudad = pCiudad;
        }
        #endregion

        public override string ToString()
        {
            return $"{Codigo}, {CalleNumero}, {Ciudad}";
        }
    }
}
