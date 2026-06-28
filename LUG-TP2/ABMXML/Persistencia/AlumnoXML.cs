using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using EntidadesNegocio;

namespace Persistencia
{
    public class AlumnoXML
    {
        #region Atributos
        private readonly string rutaArchivo;
        #endregion

        #region Constructor
        public AlumnoXML()
        {
            // El XML se guarda junto al ejecutable
            this.rutaArchivo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "alumnos.xml");

            // Si no existe, lo crea con estructura base y datos de ejemplo
            if (!File.Exists(this.rutaArchivo))
                CrearArchivoInicial();
        }
        #endregion

        #region Inicializacion
        private void CrearArchivoInicial()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Alumnos");
            doc.AppendChild(raiz);

            // Datos de ejemplo precargados
            var ejemplos = new List<AlumnoBE>
            {
                new AlumnoBE { Legajo = 1, NombreApellido = "Juan Perez",    Documento = 12345678, Nacimiento = "1990-05-15", CalleNumero = "Av. Corrientes 1234", Ciudad = "Buenos Aires" },
                new AlumnoBE { Legajo = 2, NombreApellido = "Maria Gomez",   Documento = 23456789, Nacimiento = "1995-08-22", CalleNumero = "San Martin 456",      Ciudad = "Rosario"      },
                new AlumnoBE { Legajo = 3, NombreApellido = "Carlos Lopez",  Documento = 34567890, Nacimiento = "1988-11-30", CalleNumero = "Belgrano 789",        Ciudad = "Córdoba"      },
            };

            foreach (var alumno in ejemplos)
                raiz.AppendChild(CrearNodo(doc, alumno));

            doc.Save(this.rutaArchivo);
        }
        #endregion

        #region Metodos privados auxiliares
        private XmlElement CrearNodo(XmlDocument doc, AlumnoBE alumno)
        {
            XmlElement nodo = doc.CreateElement("Alumno");

            AgregarElemento(doc, nodo, "Legajo", alumno.Legajo.ToString());
            AgregarElemento(doc, nodo, "NombreApellido", alumno.NombreApellido);
            AgregarElemento(doc, nodo, "Documento", alumno.Documento.ToString());
            AgregarElemento(doc, nodo, "Nacimiento", alumno.Nacimiento);
            AgregarElemento(doc, nodo, "CalleNumero", alumno.CalleNumero);
            AgregarElemento(doc, nodo, "Ciudad", alumno.Ciudad);

            return nodo;
        }

        private void AgregarElemento(XmlDocument doc, XmlElement padre, string nombre, string valor)
        {
            XmlElement elemento = doc.CreateElement(nombre);
            elemento.InnerText = valor ?? string.Empty;
            padre.AppendChild(elemento);
        }

        private AlumnoBE NodoAAlumno(XmlNode nodo)
        {
            return new AlumnoBE
            {
                Legajo = int.Parse(nodo["Legajo"].InnerText),
                NombreApellido = nodo["NombreApellido"].InnerText,
                Documento = int.Parse(nodo["Documento"].InnerText),
                Nacimiento = nodo["Nacimiento"].InnerText,
                CalleNumero = nodo["CalleNumero"].InnerText,
                Ciudad = nodo["Ciudad"].InnerText
            };
        }

        private int ObtenerNuevoLegajo(XmlDocument doc)
        {
            int maxLegajo = 0;
            foreach (XmlNode nodo in doc.SelectNodes("//Alumno"))
            {
                int legajo = int.Parse(nodo["Legajo"].InnerText);
                if (legajo > maxLegajo)
                    maxLegajo = legajo;
            }
            return maxLegajo + 1;
        }
        #endregion

        #region CRUD

        public List<AlumnoBE> ObtenerTodos()
        {
            List<AlumnoBE> lista = new List<AlumnoBE>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                foreach (XmlNode nodo in doc.SelectNodes("//Alumno"))
                    lista.Add(NodoAAlumno(nodo));
            }
            catch
            {
                throw;
            }

            return lista;
        }

        public AlumnoBE ObtenerPorLegajo(int legajo)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                XmlNode nodo = doc.SelectSingleNode($"//Alumno[Legajo='{legajo}']");
                return nodo != null ? NodoAAlumno(nodo) : null;
            }
            catch
            {
                throw;
            }
        }

        public List<AlumnoBE> Buscar(string texto)
        {
            List<AlumnoBE> lista = new List<AlumnoBE>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                // XPath: busca en nombre o ciudad que contengan el texto
                string xpath = $"//Alumno[contains(translate(NombreApellido,'abcdefghijklmnñopqrstuvwxyz','ABCDEFGHIJKLMNÑOPQRSTUVWXYZ'),'{texto.ToUpper()}') " +
                               $"or contains(translate(Ciudad,'abcdefghijklmnñopqrstuvwxyz','ABCDEFGHIJKLMNÑOPQRSTUVWXYZ'),'{texto.ToUpper()}')]";

                foreach (XmlNode nodo in doc.SelectNodes(xpath))
                    lista.Add(NodoAAlumno(nodo));
            }
            catch
            {
                throw;
            }

            return lista;
        }

        public void Insertar(AlumnoBE alumno)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                alumno.Legajo = ObtenerNuevoLegajo(doc);

                XmlElement raiz = doc.DocumentElement;
                raiz.AppendChild(CrearNodo(doc, alumno));

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        public void Actualizar(AlumnoBE alumno)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                XmlNode nodo = doc.SelectSingleNode($"//Alumno[Legajo='{alumno.Legajo}']");
                if (nodo == null)
                    throw new Exception($"No se encontró el alumno con legajo {alumno.Legajo}.");

                nodo["NombreApellido"].InnerText = alumno.NombreApellido;
                nodo["Documento"].InnerText = alumno.Documento.ToString();
                nodo["Nacimiento"].InnerText = alumno.Nacimiento;
                nodo["CalleNumero"].InnerText = alumno.CalleNumero;
                nodo["Ciudad"].InnerText = alumno.Ciudad;

                doc.Save(this.rutaArchivo);
            }
            catch
            {
                throw;
            }
        }

        public void Eliminar(int legajo)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this.rutaArchivo);

                XmlNode nodo = doc.SelectSingleNode($"//Alumno[Legajo='{legajo}']");
                if (nodo == null)
                    throw new Exception($"No se encontró el alumno con legajo {legajo}.");

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