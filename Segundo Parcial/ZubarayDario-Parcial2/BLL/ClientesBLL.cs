using System.Collections.Generic;
using System.Text.RegularExpressions;

using BE;
using MPP;

namespace BLL
{
    public class ClientesBLL
    {
        private static readonly Regex _regexNombre = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,50}$");
        private static readonly Regex _regexApellido = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,50}$");

        public ClientesMPP mapeador;

        public ClientesBLL()
        {
            mapeador = new ClientesMPP();
        }

        #region Metodos Logica Negocio
        private void Validar(Cliente cliente)
        {
            if (!_regexNombre.IsMatch(cliente.Nombre))
            {
                throw new ClienteException(
                    "El nombre contiene caracteres inválidos.",
                    "Validación");
            }

            if (!_regexApellido.IsMatch(cliente.Apellido))
            {
                throw new ClienteException(
                    "El apellido contiene caracteres inválidos.",
                    "Validación");
            }

            if (cliente.DNI < 1000000 || cliente.DNI > 99999999)
            {
                throw new ClienteException(
                    "El DNI debe contener entre 7 y 8 dígitos.",
                    "Validación");
            }
        }

        public bool Baja(Cliente cliente)
        {
            Validar(cliente);
            return mapeador.Baja(cliente);
        }

        public bool Guardar(Cliente cliente)
        {
            Validar(cliente);
            return mapeador.Guardar(cliente);
        }

        public List<Cliente> ListarTodo()
        {
            return mapeador.ListarTodo();
        }
        #endregion
    }
}
