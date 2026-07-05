using System;
using System.Collections.Generic;
using System.Xml;

using BE;
using Persistencia;

namespace MPP
{
    public class DispositivosMPP
    {
        private DispositivosXML _dispositivoXML;

        public DispositivosMPP()
        {
            this._dispositivoXML = new DispositivosXML();
        }

        #region Metodos privados
        private Dispositivo NodoADispositivo(XmlNode nodo)
        {
            string input = nodo["Estado"].InnerText;
            Enum.TryParse<EstadoDispositivo>(input, out EstadoDispositivo result);

            string tipo = nodo.Attributes["tipo"]?.Value;
            Dispositivo dispositivo;

            switch (tipo)
            {
                case "Notebook":
                    dispositivo = new Notebook
                    {
                        Proposito = nodo["Proposito"]?.InnerText
                    };
                    break;

                case "TelefonoMovil":
                    dispositivo = new TelefonoMovil
                    {
                        ResistenteAgua = nodo["ResistenteAgua"] != null
                            && bool.Parse(nodo["ResistenteAgua"].InnerText)
                    };
                    break;

                default:
                    dispositivo = new Dispositivo();
                    break;
            }

            dispositivo.Codigo = int.Parse(nodo["Codigo"].InnerText);
            dispositivo.Descripcion = nodo["Descripcion"].InnerText;
            dispositivo.Precio = Convert.ToDouble(nodo["Precio"].InnerText);
            dispositivo.Cantidad = int.Parse(nodo["Cantidad"].InnerText);
            dispositivo.Estado = result;
            dispositivo.Procesador = NodoAProcesador(nodo["Procesador"]);

            return dispositivo;
        }

        private Procesador NodoAProcesador(XmlNode nodo)
        {
            return new Procesador
            {
                Codigo = int.Parse(nodo["Codigo"].InnerText),
                Marca = nodo["Marca"].InnerText
            };
        }
        #endregion

        #region CRUD
        public List<Dispositivo> ListarTodo()
        {
            List<Dispositivo> lista = new List<Dispositivo>();
            try
            {
                var xmlDispositivo = this._dispositivoXML.ObtenerDispositivos();
                foreach (XmlNode dispositivoNodo in xmlDispositivo)
                {
                    var dispositivo = NodoADispositivo(dispositivoNodo);
                    lista.Add(dispositivo);
                }
            }
            catch
            {
                throw;
            }

            return lista;
        }

        public Dispositivo ObtenerPorCodigo(int codigo)
        {
            try
            {
                XmlDocument doc = _dispositivoXML.GetXmlDocument();

                XmlNode nodo = doc.SelectSingleNode($"//Dispositivo[Codigo='{codigo}']");
                return nodo != null ? NodoADispositivo(nodo) : null;
            }
            catch
            {
                throw;
            }
        }

        public bool Guardar(Dispositivo dispositivo)
        {
            try
            {
                if (dispositivo.Codigo == 0)
                {
                    this._dispositivoXML.Insertar(dispositivo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}