using System.Collections.Generic;
using System.Linq;

using BE;
using MPP;

namespace BLL
{
    public class InformeDispositivoMasVendidoBLL
    {
        private ContratacionMPP _contratacionMPP;
        private DispositivosMPP _dispositivosMPP;

        public InformeDispositivoMasVendidoBLL()
        {
            this._contratacionMPP = new ContratacionMPP();
            this._dispositivosMPP = new DispositivosMPP();
        }

        public (string[] dispositivos, double[] unidades) ObtenerDispositivosMasVendidos()
        {
            Dictionary<int, int> contadorDispositivos = new Dictionary<int, int>();
            var contrataciones = _contratacionMPP.ListarTodo();

            foreach (Contratacion contratacion in contrataciones)
            {
                int idDispositivo = contratacion.CodigoDispositivo;

                if (contadorDispositivos.ContainsKey(idDispositivo))
                {
                    contadorDispositivos[idDispositivo]++;
                }
                else
                {
                    contadorDispositivos[idDispositivo] = 1;
                }
            }

            // Ordenar y limitar a 5 resultados
            var dispositivosOrdenados = contadorDispositivos
                    .OrderByDescending(par => par.Value)
                    .Take(5)
                    .ToList();

            string[] dispositivos = new string[dispositivosOrdenados.Count];
            double[] unidades = new double[dispositivosOrdenados.Count];

            for (int i = 0; i < dispositivosOrdenados.Count; i++)
            {
                int codigo = dispositivosOrdenados[i].Key;

                var d = this._dispositivosMPP.ObtenerPorCodigo(codigo);

                dispositivos[i] = d != null ? d.Descripcion : $"Desconocido ({codigo})";

                unidades[i] = dispositivosOrdenados[i].Value;
            }

            return (dispositivos, unidades);
        }
    }
}
