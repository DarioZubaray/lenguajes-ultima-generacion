using System.Collections.Generic;
using System.Xml;

using BE;
using Persistencia;

namespace MPP
{
    public class ClientesMPP
    {
        private ClientesXML _clienteXML;
        private DispositivosMPP _dispositivosMPP;

        public ClientesMPP()
        {
            this._clienteXML = new ClientesXML();
            this._dispositivosMPP = new DispositivosMPP();
        }

        #region Metodos privados
        private Cliente NodoACliente(XmlNode nodo)
        {
            var dispositivo = _dispositivosMPP.NodoADispositivo(nodo);
            return new Cliente
            {
                Codigo = int.Parse(nodo["Codigo"].InnerText),
                Nombre = nodo["Nombre"].InnerText,
                Apellido = nodo["Apellido"].InnerText,
                DNI = int.Parse(nodo["DNI"].InnerText),
                Dispositivo = dispositivo
            };
        }
        #endregion

        #region CRUD
        public List<Cliente> ListarTodo()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                var xmlClientes = _clienteXML.ObtenerClientes();
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

        public Cliente ObtenerPorCodigo(int codigo)
        {
            try
            {
                XmlDocument doc = _clienteXML.GetXmlDocument();

                XmlNode nodo = doc.SelectSingleNode($"//Cliente[Codigo='{codigo}']");
                return nodo != null ? NodoACliente(nodo) : null;
            }
            catch
            {
                throw;
            }
        }

        public bool Guardar(Cliente cliente)
        {
            try
            {
                if (cliente.Codigo == 0)
                {
                    _clienteXML.Insertar(cliente);
                    return true;
                }
                else
                {
                    _clienteXML.Actualizar(cliente);
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
                _clienteXML.Eliminar(cliente.Codigo);
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
