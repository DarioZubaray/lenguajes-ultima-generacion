namespace BE
{
    public class TelefonoMovil : Dispositivo
    {
        public bool ResistenteAgua { get; set; }
        public override double DescuentoCalculado()
        {
            return Precio * (1 - 0.1);
        }
    }
}
