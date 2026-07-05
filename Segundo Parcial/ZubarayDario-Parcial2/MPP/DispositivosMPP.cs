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

            return new Dispositivo
            {
                Codigo = int.Parse(nodo["Codigo"].InnerText),
                Descripcion = nodo["Descripcion"].InnerText,
                Precio = Convert.ToDouble(nodo["Precio"].InnerText),
                Cantidad = int.Parse(nodo["Cantidad"].InnerText),
                Estado = result,
                Procesador = NodoAProcesador(nodo["Procesador"])
            };
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
