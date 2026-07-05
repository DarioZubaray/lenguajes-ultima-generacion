using System;
using System.Collections.Generic;

using BE;
using MPP;

namespace BLL
{
    public class ContratacionBLL
    {
        private ClientesMPP _clienteMPP;
        private DispositivosMPP _dispositivoMPP;
        private ContratacionMPP _contratacionesMPP;

        public ContratacionBLL()
        {
            this._clienteMPP = new ClientesMPP();
            this._dispositivoMPP = new DispositivosMPP();
            this._contratacionesMPP = new ContratacionMPP();
        }

        public List<ContratacionView> ListarTodo()
        {
            var contrataciones = _contratacionesMPP.ListarTodo();

            List<ContratacionView> listaView = new List<ContratacionView>();
            foreach(var contratacion in contrataciones)
            {
                ContratacionView view = new ContratacionView();

                var cliente = this._clienteMPP.ObtenerPorCodigo(contratacion.CodigoCliente);

                view.cliente = cliente.ToString();

                var dispositivo = this._dispositivoMPP.ObtenerPorCodigo(contratacion.CodigoDispositivo);
                view.dispositivo = dispositivo.ToString();

                listaView.Add(view);
            }

            return listaView;
        }

        public void Contratar(Cliente cliente, Dispositivo dispositivo)
        {
            this._contratacionesMPP.Guardar(cliente, dispositivo);
            dispositivo.Estado = EstadoDispositivo.Adquirido;
            cliente.Dispositivo = dispositivo;
            this._clienteMPP.Guardar(cliente);
        }
    }
}
