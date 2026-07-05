using System;

namespace BLL
{
    public class DispositivoException : Exception
    {
        public string Tipo { get; }

        public DispositivoException(string mensaje, string tipo)
            : base(mensaje)
        {
            Tipo = tipo;
        }
    }
}
