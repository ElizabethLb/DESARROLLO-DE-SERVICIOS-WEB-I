namespace BookMatch.Dtos
{
    public class LibroDto
    {
        public int LibroID { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public int AutorID { get; set; }
        public int CategoriaID { get; set; }
        public int IdiomaID { get; set; }
        public string Sinopsis { get; set; }
        public string Precio { get; set; }
        public int EsGratuito { get; set; }
        public int Paginas { get; set; }
        public string Portada { get; set; }
        public string ArchivoURL { get; set; }
        public string Estado { get; set; }
        public string FechaPublicacion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaActualizacion { get; set; }
        public int TotalVentas { get; set; }
        public int TotalDescargas { get; set; }
    }
}