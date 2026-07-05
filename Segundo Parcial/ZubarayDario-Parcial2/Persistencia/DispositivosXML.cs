using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using BE;

namespace Persistencia
{
    public class DispositivosXML
    {
        #region Atributos
        private readonly string rutaArchivo;
        #endregion

        #region Constructor
        public DispositivosXML()
        {
            this.rutaArchivo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "dispositivos.xml");

            if (!File.Exists(this.rutaArchivo))
                CrearArchivoInicial();
        }
        #endregion

        #region Inicializacion
        private void CrearArchivoInicial()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Dispositivos");
            doc.AppendChild(raiz);

            var ejemplos = new List<Dispositivo>
            {
                new Dispositivo
                {
                    Codigo = 1,
                    Descripcion = "Notebook Lenovo ThinkPad E14",
                    Precio = 1250.50,
                    Cantidad = 10,
                    Estado = EstadoDispositivo.Disponible,
                    Procesador = new Procesador
                    {
                        Codigo = 1,
                        Marca = "Intel Core i5-12400"
                    }
                },

                new Dispositivo
                {
                    Codigo = 2,
                    Descripcion = "PC Gamer Ryzen 7",
                    Precio = 1899.99,
                    Cantidad = 3,
                    Estado = EstadoDispositivo.Disponible,
                    Procesador = new Procesador
                    {
                        Codigo = 2,
                        Marca = "AMD Ryzen 7 7700X"
                    }
                },

                new Dispositivo
                {
                    Codigo = 3,
                    Descripcion = "Servidor Dell PowerEdge",
                    Precio = 5499.00,
                    Cantidad = 1,
                    Estado = EstadoDispositivo.Adquirido,
                    Procesador = new Procesador
                    {
                        Codigo = 3,
                        Marca = "Intel Xeon Silver 4410Y"
                    }
                }
            };

            foreach (var dispositivo in ejemplos)
                raiz.AppendChild(CrearNodo(doc, dispositivo));

            doc.Save(this.rutaArchivo);
        }
        #endregion

        #region Metodos privados auxiliares
        private XmlElement CrearNodo(XmlDocument doc, Dispositivo dispositivo)
        {
            XmlElement nodo = doc.CreateElement("Dispositivo");

            AgregarElemento(doc, nodo, "Codigo", dispositivo.Codigo.ToString());
            AgregarElemento(doc, nodo, "Descripcion", dispositivo.Descripcion);
            AgregarElemento(doc, nodo, "Precio", dispositivo.Precio.ToString());
            AgregarElemento(doc, nodo, "Cantidad", dispositivo.Cantidad.ToString());
            AgregarElemento(doc, nodo, "Estado", dispositivo.Estado.ToString());
            nodo.AppendChild(CrearProcesador(doc, dispositivo.Procesador));

            return nodo;
        }

        private XmlElement CrearProcesador(XmlDocument doc, Procesador procesador)
        {
            XmlElement nodo = doc.CreateElement("Procesador");

            AgregarElemento(doc, nodo, "Codigo", procesador.Codigo.ToString());
            AgregarElemento(doc, nodo, "Marca", procesador.Marca);
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
            foreach (XmlNode nodo in doc.SelectNodes("//Dispositivo"))
            {
                int codigo = int.Parse(nodo["Codigo"].InnerText);
                if (codigo > maxCodigo)
                    maxCodigo = codigo;
            }
            return maxCodigo + 1;
        }

        public XmlDocument GetXmlDocument()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(this.rutaArchivo);
            return doc;
        }

        public XmlNodeList ObtenerDispositivos()
        {
            XmlDocument doc = GetXmlDocument();
            return doc.SelectNodes("//Dispositivo");
        }
        #endregion

        #region CRUD
        public void Insertar(Dispositivo dispositivo)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                dispositivo.Codigo = ObtenerNuevoCodigo(doc);

                XmlElement raiz = doc.DocumentElement;
                raiz.AppendChild(CrearNodo(doc, dispositivo));

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
