using System;

namespace BLL
{
    public class ClienteException : Exception
    {
        public string Tipo { get; }

        public ClienteException(string mensaje, string tipo)
            : base(mensaje)
        {
            Tipo = tipo;
        }
    }
}
