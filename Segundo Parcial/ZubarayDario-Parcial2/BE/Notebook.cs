namespace BE
{
    public class Notebook : Dispositivo
    {
        public string Proposito { get; set; }
        public override double DescuentoCalculado()
        {
            return Precio * (1 - 0.15);
        }
    }
}
