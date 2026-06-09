using System;
using System.ComponentModel;
using Abstraccion;

namespace EntidadesNegocio
{
    public class CursoBE: IEntidad
    {
        #region Propiedades
        public string Nombre { get; set; }
        public DateTime Inicio { get; set; }

        [DisplayName("Detalles de Costo")]
        public virtual string ResumenCosto => "Gratis";
        #endregion

        #region Constructores
        public CursoBE() { }

        public CursoBE(string pNombre, DateTime pInicio)
        {
            this.Nombre = pNombre;
            this.Inicio = pInicio;
        }
        #endregion

        public override string ToString()
        {
            return $"{Codigo}, {Nombre}, {Inicio}";
        }
    }
}
