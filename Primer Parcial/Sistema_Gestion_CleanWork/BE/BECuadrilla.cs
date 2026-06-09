namespace BE
{
    public class BECuadrilla
    {
        public int Codigo { get; set; }
        public string NombreSupervisor { get; set; }
        public string TurnoTrabajo { get; set; }
        public int CantidadOperarios { get; set; }

        public override string ToString()
        {
            return $"{Codigo} - {NombreSupervisor?.Trim()}";
        }
    }
}
