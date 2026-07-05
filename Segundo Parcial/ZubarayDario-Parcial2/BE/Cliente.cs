namespace BE
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public Dispositivo Dispositivo { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - ({DNI})";
        }
    }
}
