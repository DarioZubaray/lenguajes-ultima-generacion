using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using BE;

namespace Persistencia
{
    public class ClienteXML
    {
        #region Atributos
        private readonly string rutaArchivo;
        #endregion

        #region Constructor
        public ClienteXML()
        {
            this.rutaArchivo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "clientes.xml");

            if (!File.Exists(this.rutaArchivo))
                CrearArchivoInicial();
        }
        #endregion

        #region Inicializacion
        private void CrearArchivoInicial()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Clientes");
            doc.AppendChild(raiz);

            var ejemplos = new List<Cliente>
            {
                new Cliente { Codigo = 1, Nombre = "Juan", Apellido = "Perez",   DNI = 12345678},
                new Cliente { Codigo = 2, Nombre = "Maria", Apellido = "Gomez",   DNI = 23456789},
                new Cliente { Codigo = 3, Nombre = "Carlos", Apellido = "Lopez",  DNI = 34567890},
            };

            foreach (var cliente in ejemplos)
                raiz.AppendChild(CrearNodo(doc, cliente));

            doc.Save(this.rutaArchivo);
        }
        #endregion

        #region Metodos privados auxiliares
        private XmlElement CrearNodo(XmlDocument doc, Cliente cliente)
        {
            XmlElement nodo = doc.CreateElement("Cliente");

            AgregarElemento(doc, nodo, "Codigo", cliente.Codigo.ToString());
            AgregarElemento(doc, nodo, "Nombre", cliente.Nombre);
            AgregarElemento(doc, nodo, "Apellido", cliente.Apellido);
            AgregarElemento(doc, nodo, "DNI", cliente.DNI.ToString());

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
            foreach (XmlNode nodo in doc.SelectNodes("//Cliente"))
            {
                int codigo = int.Parse(nodo["Codigo"].InnerText);
                if (codigo > maxCodigo)
                    maxCodigo = codigo;
            }
            return maxCodigo + 1;
        }

        public XmlNodeList ObtenerClientes()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo);
            return doc.SelectNodes("//Cliente");
        }
        #endregion

        #region CRUD
        public void Insertar(Cliente cliente)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                cliente.Codigo = ObtenerNuevoCodigo(doc);

                XmlElement raiz = doc.DocumentElement;
                raiz.AppendChild(CrearNodo(doc, cliente));

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        public void Actualizar(Cliente cliente)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                XmlNode nodo = doc.SelectSingleNode($"//Cliente[Codigo='{cliente.Codigo}']");
                if (nodo == null)
                    throw new Exception($"No se encontró el cliente con código {cliente.Codigo}.");

                nodo["Nombre"].InnerText = cliente.Nombre;
                nodo["Apellido"].InnerText = cliente.Apellido;
                nodo["DNI"].InnerText = cliente.DNI.ToString();

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        public void Eliminar(int codigo)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                XmlNode nodo = doc.SelectSingleNode($"//Cliente[Codigo='{codigo}']");
                if (nodo == null)
                    throw new Exception($"No se encontró el cliente con código {codigo}.");

                doc.DocumentElement.RemoveChild(nodo);
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
