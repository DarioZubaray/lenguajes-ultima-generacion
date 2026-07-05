using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using BE;

namespace Persistencia
{
    public class ContratacionXML
    {
        #region Atributos
        private readonly string rutaArchivo;
        #endregion

        #region Constructor
        public ContratacionXML()
        {
            this.rutaArchivo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "contratacion.xml");

            if (!File.Exists(this.rutaArchivo))
                CrearArchivoInicial();
        }
        #endregion

        #region Inicializacion
        private void CrearArchivoInicial()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Contratacion");
            doc.AppendChild(raiz);

            var ejemplos = new List<Contratacion>
            {
                new Contratacion
                {
                    CodigoCliente = 2,
                    CodigoDispositivo = 1
                }
            };

            foreach (var contratacion in ejemplos)
                raiz.AppendChild(CrearNodo(doc, contratacion));

            doc.Save(this.rutaArchivo);
        }
        #endregion

        #region Metodos privados auxiliares
        private XmlElement CrearNodo(XmlDocument doc, Contratacion contratacion)
        {
            XmlElement nodo = doc.CreateElement("Contratacion");

            AgregarElemento(doc, nodo, "CodigoCliente", contratacion.CodigoCliente.ToString());
            AgregarElemento(doc, nodo, "CodigoDispositivo", contratacion.CodigoDispositivo.ToString());

            return nodo;
        }

        public void Guardar(Contratacion contratacion)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                XmlElement raiz = doc.DocumentElement;
                raiz.AppendChild(CrearNodo(doc, contratacion));

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        private void AgregarElemento(XmlDocument doc, XmlElement padre, string nombre, string valor)
        {
            XmlElement elemento = doc.CreateElement(nombre);
            elemento.InnerText = valor ?? string.Empty;
            padre.AppendChild(elemento);
        }
        #endregion

        public XmlNodeList ObtenerContrataciones()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo);
            return doc.SelectNodes("//Contratacion/Contratacion");
        }
    }
}
