namespace BE
{
    public class Procesador
    {
        public int Codigo { get; set; }
        public string Marca { get; set; }
        public double Frecuencia { get; set; }

        public override string ToString()
        {
            return $"{Marca} - {Frecuencia}Ghz";
        }
    }
}
