namespace BookMatch.Dtos
{
    public class DetalleOrdenDto
    {
        public int DetalleID { get; set; }
        public int OrdenID { get; set; }
        public int LibroID { get; set; }
        public string Precio { get; set; }
        public int EsGratuito { get; set; }
    }
}
