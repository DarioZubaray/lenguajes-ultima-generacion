using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using BE;
using DAL;

namespace MPP
{
    public class MPPServicioLimpiezaVidriosAltura : MPPServicio
    {
        DALAcceso acceso;

        public MPPServicioLimpiezaVidriosAltura()
        {
            this.acceso = new DALAcceso();
        }

        public override bool Baja(BEServicio servicio)
        {
            string consulta = string.Format(@"DELETE FROM Servicio WHERE codigo = {0}", servicio.Codigo);
            return this.acceso.Escribir(consulta);
        }

        public override bool Guardar(BEServicio servicio)
        {
            var limpiezaVidriosAltura = servicio as BEServicioLimpiezaVidriosAltura;
            if (limpiezaVidriosAltura.Codigo == 0)
            {
                string consultaServicio = string.Format(@"INSERT INTO Servicio(nombre, abono, precio_base, altura_maxima) 
                                                VALUES('{0}','{1}',{2}, '{3}');
                                                SELECT SCOPE_IDENTITY();",
                                                limpiezaVidriosAltura.Nombre, limpiezaVidriosAltura.Abono,
                                                limpiezaVidriosAltura.PrecioBase.ToString("N2", CultureInfo.InvariantCulture).Replace(",", ""),
                                                limpiezaVidriosAltura.AlturaMaxima);

                return this.acceso.Escribir(consultaServicio);
            }
            else
            {
                string consultaServicio = string.Format(@"UPDATE Servicio SET nombre='{0}', abono='{1}', precio_base={2}, altura_maxima = '{3}' 
                                                    WHERE codigo = {4}",
                                                    limpiezaVidriosAltura.Nombre, limpiezaVidriosAltura.Abono,
                                                    limpiezaVidriosAltura.PrecioBase.ToString("N2", CultureInfo.InvariantCulture).Replace(",", ""),
                                                    limpiezaVidriosAltura.AlturaMaxima, limpiezaVidriosAltura.Codigo);

                return this.acceso.Escribir(consultaServicio);
            }
        }

        private List<BEServicio> MapBEServicio(string consulta)
        {
            List<BEServicio> listaServicios = new List<BEServicio>();

            DataTable Tabla = this.acceso.Leer(consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    BEServicio servicioBD = new BEServicioLimpiezaVidriosAltura();
                    ((BEServicioLimpiezaVidriosAltura)servicioBD).AlturaMaxima = Convert.ToInt32(fila[6]);

                    servicioBD.Codigo = Convert.ToInt32(fila[0]);
                    servicioBD.Nombre = fila[1].ToString();
                    Enum.TryParse(fila[2].ToString(), out TipoAbono abono);
                    servicioBD.Abono = abono;
                    decimal.TryParse(fila[3].ToString(), out decimal precio);
                    servicioBD.PrecioBase = precio;

                    BECuadrilla cuadrilla = new BECuadrilla();
                    cuadrilla.Codigo = Convert.ToInt32(fila[4]);
                    cuadrilla.NombreSupervisor = fila[8].ToString();
                    cuadrilla.TurnoTrabajo = fila[9].ToString();
                    cuadrilla.CantidadOperarios = Convert.ToInt32(fila[10]);
                    servicioBD.CuadrillaTrabajo = cuadrilla;

                    listaServicios.Add(servicioBD);
                }
            }
            return listaServicios;
        }

        public override List<BEServicio> ListarTodo()
        {
            string Consulta = @"SELECT s.codigo, s.nombre, s.abono, s.precio_base, s.codigo_cuadrilla, s.quimico, s.altura_maxima,
	                                   c.codigo, c.nombre_supervisor, c.turno_trabajo, c.cantidad_operarios
                                FROM Servicio s, Cuadrilla c
                                WHERE s.codigo_cuadrilla = c.codigo
                                      AND NOT s.altura_maxima IS NULL";
            return MapBEServicio(Consulta);
        }

        public List<BEServicio> ListarPorCodigoCliente(int codigo)
        {
            string Consulta = string.Format(@"SELECT cs.codigo_cliente, cs.codigo_servicio, cs.estado, cs.descuentos,
                                                    s.codigo, s.nombre, s.abono, s.precio_base, s.codigo_cuadrilla, s.quimico, s.altura_maxima
                                            FROM Cliente_Servicio cs, Servicio s
                                            WHERE cs.codigo_servicio = s.codigo AND cs.codigo_cliente = {0}
                                                  AND NOT s.altura_maxima IS NULL", codigo);

            List<BEServicio> listaServicios = new List<BEServicio>();

            DataTable Tabla = this.acceso.Leer(Consulta);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow fila in Tabla.Rows)
                {
                    BEServicio servicioBD = new BEServicioLimpiezaVidriosAltura();
                    ((BEServicioLimpiezaVidriosAltura)servicioBD).AlturaMaxima = Convert.ToInt32(fila[10]);

                    servicioBD.Codigo = Convert.ToInt32(fila[4]);
                    servicioBD.Nombre = fila[5].ToString();
                    Enum.TryParse(fila[6].ToString(), out TipoAbono abono);
                    servicioBD.Abono = abono;
                    decimal.TryParse(fila[7].ToString(), out decimal precio);
                    servicioBD.PrecioBase = precio;

                    BECuadrilla cuadrilla = new BECuadrilla();
                    cuadrilla.Codigo = Convert.ToInt32(fila[8]);
                    servicioBD.CuadrillaTrabajo = cuadrilla;

                    listaServicios.Add(servicioBD);
                }
            }
            return listaServicios;
        }

        public DataTable ListarLimpiezaVidriosAlturaMenosVendidoPorcuadrilla()
        {
            string Consulta = @"SELECT s.codigo_cuadrilla, s.nombre, s.altura_maxima, COUNT(*) AS cantidad_solicitudes
                                FROM Cliente_Servicio cs
                                INNER JOIN Servicio s
                                    ON cs.codigo_servicio = s.codigo
                                WHERE NOT s.altura_maxima IS NULL
                                GROUP BY
                                    s.codigo_cuadrilla, s.nombre, s.altura_maxima
                                ORDER BY cantidad_solicitudes ASC;";

            return this.acceso.Leer(Consulta);
        }
    }
}
