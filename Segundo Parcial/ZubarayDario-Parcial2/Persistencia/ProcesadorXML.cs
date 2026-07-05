using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using BE;

namespace Persistencia
{
    public class ProcesadorXML
    {
        #region Atributos
        private readonly string rutaArchivo;
        #endregion

        #region Constructor
        public ProcesadorXML()
        {
            this.rutaArchivo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "procesadores.xml");

            if (!File.Exists(this.rutaArchivo))
                CrearArchivoInicial();
        }
        #endregion


        #region Inicializacion
        private void CrearArchivoInicial()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Procesador");
            doc.AppendChild(raiz);

            var ejemplos = new List<Procesador>
            {
                new Procesador
                    {
                        Codigo = 1,
                        Marca = "Intel Core i5-12400",
                        Frecuencia = 2.0
                    },

                new Procesador
                    {
                        Codigo = 2,
                        Marca = "AMD Ryzen 7 7700X",
                        Frecuencia = 3.8
                    },

                new Procesador
                    {
                        Codigo = 3,
                        Marca = "Intel Xeon Silver 4410Y",
                        Frecuencia = 3.0
                    }
            };

            foreach (var procesador in ejemplos)
                raiz.AppendChild(CrearNodo(doc, procesador));

            doc.Save(this.rutaArchivo);
        }
        #endregion

        #region Metodos privados auxiliares
        private XmlElement CrearNodo(XmlDocument doc, Procesador procesador)
        {
            XmlElement nodo = doc.CreateElement("Procesador");

            AgregarElemento(doc, nodo, "Codigo", procesador.Codigo.ToString());
            AgregarElemento(doc, nodo, "Marca", procesador.Marca.ToString());
            AgregarElemento(doc, nodo, "Frecuencia", procesador.Frecuencia.ToString());

            return nodo;
        }

        private void AgregarElemento(XmlDocument doc, XmlElement padre, string nombre, string valor)
        {
            XmlElement elemento = doc.CreateElement(nombre);
            elemento.InnerText = valor ?? string.Empty;
            padre.AppendChild(elemento);
        }
        #endregion

        #region Metodos Publicos
        public int ObtenerNuevoCodigo(XmlDocument doc)
        {
            int maxCodigo = 0;
            foreach (XmlNode nodo in doc.SelectNodes("//Procesador"))
            {
                int codigo = int.Parse(nodo["Codigo"].InnerText);
                if (codigo > maxCodigo)
                    maxCodigo = codigo;
            }
            return maxCodigo + 1;
        }

        public XmlNodeList ObtenerProcesadores()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo);
            return doc.SelectNodes("//Procesador/Procesador");
        }
        #endregion
    }
}
