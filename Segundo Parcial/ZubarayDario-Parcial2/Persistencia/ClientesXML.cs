using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using BE;

namespace Persistencia
{
    public class ClientesXML
    {
        #region Atributos
        private readonly string rutaArchivo;
        #endregion

        #region Constructor
        public ClientesXML()
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

        public XmlDocument GetXmlDocument()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(this.rutaArchivo);
            return doc;
        }

        public XmlNodeList ObtenerClientes()
        {
            XmlDocument doc = GetXmlDocument();
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

                XmlNode nodoCliente = doc.SelectSingleNode($"//Cliente[Codigo='{cliente.Codigo}']");
                if (nodoCliente == null)
                    throw new Exception($"No se encontró el cliente con código {cliente.Codigo}.");

                nodoCliente["Nombre"].InnerText = cliente.Nombre;
                nodoCliente["Apellido"].InnerText = cliente.Apellido;
                nodoCliente["DNI"].InnerText = cliente.DNI.ToString();

                if (cliente.Dispositivo != null)
                {
                    var dispositivo = cliente.Dispositivo;

                    XmlElement LennodoDispositivo = nodoCliente["Dispositivo"];
                    if (LennodoDispositivo == null)
                    {
                        LennodoDispositivo = doc.CreateElement("Dispositivo");
                        nodoCliente.AppendChild(LennodoDispositivo);
                    }

                    string tipoDispositivo = "";
                    if (dispositivo is Notebook)
                    {
                        tipoDispositivo = "Notebook";
                    }
                    else if (dispositivo is TelefonoMovil)
                    {
                        tipoDispositivo = "Teléfono Movil";
                    }

                    LennodoDispositivo.SetAttribute("tipo", tipoDispositivo);

                    ActualizarOCrearSubNodo(doc, LennodoDispositivo, "Codigo", dispositivo.Codigo.ToString());
                    ActualizarOCrearSubNodo(doc, LennodoDispositivo, "Descripcion", dispositivo.Descripcion);
                    ActualizarOCrearSubNodo(doc, LennodoDispositivo, "Precio", dispositivo.Precio.ToString());
                    ActualizarOCrearSubNodo(doc, LennodoDispositivo, "Cantidad", dispositivo.Cantidad.ToString());
                    ActualizarOCrearSubNodo(doc, LennodoDispositivo, "Estado", EstadoDispositivo.Adquirido.ToString());
                    ActualizarOCrearProcesador(doc, LennodoDispositivo, dispositivo.Procesador);

                    if (dispositivo is Notebook notebook)
                    {
                        ActualizarOCrearSubNodo(doc, LennodoDispositivo, "Proposito", notebook.Proposito);
                    }
                    else if (dispositivo is TelefonoMovil telefono)
                    {
                        ActualizarOCrearSubNodo(doc, LennodoDispositivo, "ResistenteAgua", telefono.ResistenteAgua.ToString());
                    }
                }

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        private void ActualizarOCrearSubNodo(XmlDocument doc, XmlNode nodoPadre, string nombreNodo, string valor)
        {
            XmlNode subNodo = nodoPadre[nombreNodo];
            if (subNodo == null)
            {
                subNodo = doc.CreateElement(nombreNodo);
                nodoPadre.AppendChild(subNodo);
            }
            subNodo.InnerText = valor;
        }

        private void ActualizarOCrearProcesador(XmlDocument doc, XmlNode nodoPadre, Procesador procesador)
        {
            XmlElement nodoProcesador = nodoPadre["Procesador"];
            if (nodoProcesador == null)
            {
                nodoProcesador = doc.CreateElement("Procesador");
                nodoPadre.AppendChild(nodoProcesador);
            }

            ActualizarOCrearSubNodo(doc, nodoProcesador, "Codigo", procesador.Codigo.ToString());
            ActualizarOCrearSubNodo(doc, nodoProcesador, "Marca", procesador.Marca);
            ActualizarOCrearSubNodo(doc, nodoProcesador, "Frecuencia", procesador.Frecuencia.ToString());
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