using System.Collections.Generic;
using System.Xml;

using BE;
using Persistencia;

namespace MPP
{
    public class ClienteMPP
    {
        private ClienteXML ClienteXML;

        public ClienteMPP()
        {
            ClienteXML = new ClienteXML();
        }

        #region Metodos privados
        private Cliente NodoACliente(XmlNode nodo)
        {
            return new Cliente
            {
                Codigo = int.Parse(nodo["Codigo"].InnerText),
                Nombre = nodo["Nombre"].InnerText,
                Apellido = nodo["Apellido"].InnerText,
                DNI = int.Parse(nodo["DNI"].InnerText)
            };
        }
        #endregion

        #region CRUD

        public List<Cliente> ListarTodo()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                var xmlClientes = ClienteXML.ObtenerClientes();
                foreach (XmlNode clienteNodo in xmlClientes)
                {
                    var cliente = NodoACliente(clienteNodo);
                    lista.Add(cliente);
                }
            }
            catch
            {
                throw;
            }

            return lista;
        }

        public bool Guardar(Cliente cliente)
        {
            try
            {
                if (cliente.Codigo == 0)
                {
                    ClienteXML.Insertar(cliente);
                    return true;
                }
                else
                {
                    ClienteXML.Actualizar(cliente);
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool Baja(Cliente cliente)
        {
            try
            {
                ClienteXML.Eliminar(cliente.Codigo);
                return true;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
