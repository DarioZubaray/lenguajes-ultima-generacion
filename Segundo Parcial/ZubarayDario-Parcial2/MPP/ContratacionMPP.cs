using System;
using System.Collections.Generic;
using System.Xml;

using BE;
using Persistencia;

namespace MPP
{
    public class ContratacionMPP
    {
        private ContratacionXML _ContratacionXML;

        public ContratacionMPP()
        {
            this._ContratacionXML = new ContratacionXML();
        }

        private Contratacion NodoAContratacion(XmlNode nodo)
        {
            return new Contratacion
            {
                CodigoCliente = int.Parse(nodo["CodigoCliente"].InnerText),
                CodigoDispositivo = int.Parse(nodo["CodigoDispositivo"].InnerText),
            };
        }

        #region CRUD
        public List<Contratacion> ListarTodo()
        {
            List<Contratacion> lista = new List<Contratacion>();
            try
            {
                var xmlContrataciones = this._ContratacionXML.ObtenerContrataciones();
                foreach (XmlNode contratacionNodo in xmlContrataciones)
                {
                    var cliente = NodoAContratacion(contratacionNodo);
                    lista.Add(cliente);
                }
            }
            catch
            {
                throw;
            }

            return lista;
        }

        public void Guardar(Cliente cliente, Dispositivo dispositivo)
        {
            Contratacion contratacion = new Contratacion
            {
                CodigoCliente = cliente.Codigo,
                CodigoDispositivo = dispositivo.Codigo
            };
            this._ContratacionXML.Guardar(contratacion);
        }
        #endregion
    }
}
