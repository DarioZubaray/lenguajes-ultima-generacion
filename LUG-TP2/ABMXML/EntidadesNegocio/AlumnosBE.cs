namespace EntidadesNegocio
{
    public class AlumnoBE
    {
        public int Legajo { get; set; }
        public string NombreApellido { get; set; }
        public int Documento { get; set; }
        public string Nacimiento { get; set; } // formato yyyy-MM-dd para XML
        public string CalleNumero { get; set; }
        public string Ciudad { get; set; }

        public override string ToString()
        {
            return $"{Legajo} - {NombreApellido} ({Documento})";
        }
    }
}