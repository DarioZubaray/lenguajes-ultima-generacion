namespace EntidadesNegocio
{
    public class CursoPagoBE: CursoBE
    {
        public int Precio { get; set; }

        public override string ResumenCosto => $"Costo: {Precio:C}";
    }
}
