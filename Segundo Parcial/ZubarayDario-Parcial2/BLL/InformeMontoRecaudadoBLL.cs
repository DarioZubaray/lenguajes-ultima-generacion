using System;
using System.Collections.Generic;
using System.Linq;

using BE;
using MPP;

namespace BLL
{
    public class InformeMontoRecaudadoBLL
    {
        private ContratacionMPP _contratacionMPP;
        private DispositivosMPP _dispositivosMPP;

        public InformeMontoRecaudadoBLL()
        {
            this._contratacionMPP = new ContratacionMPP();
            this._dispositivosMPP = new DispositivosMPP();
        }
        public (string[] dispositivos, double[] montos) ObtenerMontosRecaudados()
        {
            var contrataciones = _contratacionMPP.ListarTodo();

            var conteoDispositivos = AgruparYContarPorDispositivo(contrataciones);

            var masVendidos = ObtenerTopDispositivos(conteoDispositivos, limite: 5);

            return ConstruirEstructuraRetorno(masVendidos);
        }

        private Dictionary<int, int> AgruparYContarPorDispositivo(IEnumerable<Contratacion> contrataciones)
        {
            return contrataciones
                .GroupBy(c => c.CodigoDispositivo)
                .ToDictionary(grupo => grupo.Key, grupo => grupo.Count());
        }

        private List<KeyValuePair<int, int>> ObtenerTopDispositivos(Dictionary<int, int> conteo, int limite)
        {
            return conteo
                .OrderByDescending(par => par.Value)
                .Take(limite)
                .ToList();
        }

        private (string[] dispositivos, double[] unidades) ConstruirEstructuraRetorno(List<KeyValuePair<int, int>> datos)
        {
            string[] dispositivos = new string[datos.Count];
            double[] unidades = new double[datos.Count];

            for (int i = 0; i < datos.Count; i++)
            {
                int codigo = datos[i].Key;
                var dispositivo = _dispositivosMPP.ObtenerPorCodigo(codigo);

                dispositivos[i] = dispositivo != null ? dispositivo.Descripcion : $"Desconocido ({codigo})";
                unidades[i] = datos[i].Value;
            }

            return (dispositivos, unidades);
        }
    }
}
