using System.ComponentModel;

namespace BE
{
    public class BEServicio
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public TipoAbono Abono { get; set; }
        public decimal PrecioBase { get; set; }
        public BECuadrilla CuadrillaTrabajo { get; set; }
    }

    public enum TipoAbono
    {
        Mensual,
        Semanal,
        [Description("A demanda")]
        ADemanda
    }
}
