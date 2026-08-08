namespace BookMatch.Dtos
{
    public class OrdenDto
    {
        public int OrdenID { get; set; }
        public string Codigo { get; set; }
        public int UsuarioID { get; set; }
        public string FechaOrden { get; set; }
        public string Subtotal { get; set; }
        public string Impuesto { get; set; }
        public string Total { get; set; }
        public string MetodoPago { get; set; }
        public string EstadoPago { get; set; }
    }
}