using System.Collections.Generic;

namespace BE
{
    public class BECliente
    {
        public int Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Rubro { get; set; }
        public decimal Descuento { get; set; }
        public List<BEServicio> ServiciosContratados { get; set; }
    }
}
