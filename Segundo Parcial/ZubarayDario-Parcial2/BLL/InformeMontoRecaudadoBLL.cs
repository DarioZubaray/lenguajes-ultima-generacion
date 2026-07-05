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

        public (string[] tipos, double[] montos) ObtenerMontosRecaudados()
        {
            var contrataciones = _contratacionMPP.ListarTodo();

            double montoNotebook = 0;
            double montoTelefonoMovil = 0;

            foreach (var contratacion in contrataciones)
            {
                var dispositivo = _dispositivosMPP.ObtenerPorCodigo(contratacion.CodigoDispositivo);

                if (dispositivo == null)
                    continue;

                if (dispositivo is Notebook)
                {
                    montoNotebook += dispositivo.DescuentoCalculado();
                }
                else if (dispositivo is TelefonoMovil)
                {
                    montoTelefonoMovil += dispositivo.DescuentoCalculado();
                }
            }

            string[] tipos = { "Notebook", "Telefono Movil" };
            double[] montos = { montoNotebook, montoTelefonoMovil };

            return (tipos, montos);
        }
    }
}