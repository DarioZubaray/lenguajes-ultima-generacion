using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using BE;
using MPP;

namespace BLL
{
    public class DispositivosBLL
    {
        private static readonly Regex _regexDescripcion = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\s\-\.,()/]{3,100}$");


        public DispositivosMPP _mapeador;

        public DispositivosBLL()
        {
            this._mapeador = new DispositivosMPP();
        }

        #region Metodos Logica Negocio
        private void Validar(Dispositivo dispositivo)
        {
            if (dispositivo == null)
                throw new DispositivoException("El dispositivo no puede ser nulo.", "Validación");

            if (!_regexDescripcion.IsMatch(dispositivo.Descripcion))
                throw new DispositivoException(
                    "La descripción contiene caracteres inválidos.",
                    "Validación");

            if (dispositivo.Precio <= 0)
                throw new DispositivoException(
                    "El precio debe ser mayor a cero.",
                    "Validación");

            if (dispositivo.Cantidad < 0)
                throw new DispositivoException(
                    "La cantidad no puede ser negativa.",
                    "Validación");

            if (dispositivo.Procesador == null)
                throw new DispositivoException(
                    "Debe seleccionar un procesador.",
                    "Validación");
            if (!Enum.IsDefined(typeof(EstadoDispositivo), dispositivo.DevolverEstado()))
            {
                throw new DispositivoException(
                    "El estado del dispositivo no es válido.",
                    "Validación");
            }
        }

        public List<Dispositivo> ListarTodo()
        {
            return this._mapeador.ListarTodo();
        }

        public bool Guardar(Dispositivo dispositivo)
        {
            Validar(dispositivo);
            return this._mapeador.Guardar(dispositivo);
        }
        #endregion
    }
}
