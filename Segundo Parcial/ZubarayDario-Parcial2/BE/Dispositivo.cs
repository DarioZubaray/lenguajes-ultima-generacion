namespace BE
{
    public class Dispositivo
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        private EstadoDispositivo Estado { get; set; }
        public Procesador Procesador { get; set; }

        public virtual double DescuentoCalculado()
        {
            return 0f;
        }

        public EstadoDispositivo DevolverEstado()
        {
            return Estado;
        }
    }
}
