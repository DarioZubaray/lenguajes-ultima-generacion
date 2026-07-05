using System;
using System.Collections.Generic;
using System.Xml;

using BE;
using Persistencia;

namespace MPP
{
    public class ProcesadorMPP
    {
        private ProcesadorXML _procesadorXML;

        public ProcesadorMPP()
        {
            this._procesadorXML = new ProcesadorXML();
        }

        private Procesador NodoAProcesador(XmlNode nodo)
        {
            return new Procesador
            {
                Codigo = int.Parse(nodo["Codigo"].InnerText),
                Marca = nodo["Marca"].InnerText,
                Frecuencia = Convert.ToDouble(nodo["Frecuencia"].InnerText)
            };
        }

        public List<Procesador> ListarTodo()
        {
            List<Procesador> lista = new List<Procesador>();
            try
            {
                var xmlProcesadores = this._procesadorXML.ObtenerProcesadores();
                foreach (XmlNode procesadorNodo in xmlProcesadores)
                {
                    var dispositivo = NodoAProcesador(procesadorNodo);
                    lista.Add(dispositivo);
                }
            }
            catch
            {
                throw;
            }

            return lista;
        }
    }
}
