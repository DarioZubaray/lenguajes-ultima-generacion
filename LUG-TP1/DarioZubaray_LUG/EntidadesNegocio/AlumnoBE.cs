using System;

using Abstraccion;

namespace EntidadesNegocio
{
    public class AlumnoBE: IEntidad
    {
        #region Propiedades
        public string NombreApellido { get; set; }
        public int Documento { get; set; }
        public DateTime Nacimiento { get; set; }

        public DireccionBE Direccion { get; set; }
        #endregion

        #region Constructores
        public AlumnoBE() { }

        public AlumnoBE(string pNombreApellido, int pDocumento, DateTime pNacimiento, DireccionBE pDireccion)
        {
            this.NombreApellido = pNombreApellido;
            this.Documento = pDocumento;
            this.Nacimiento = pNacimiento;
            this.Direccion = pDireccion;
        }
        #endregion

        public override string ToString()
        {
            return $"{Codigo}, {NombreApellido}, {Documento}, {Nacimiento}, {Direccion}";
        }
    }
}
