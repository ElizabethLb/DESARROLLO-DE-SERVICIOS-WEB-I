namespace BookMatch.Dtos
{
    public class BibliotecaPersonalDto
    {
        public int BibliotecaID { get; set; }
        public int UsuarioID { get; set; }
        public int LibroID { get; set; }
        public int OrdenID { get; set; }
        public string FechaAdquisicion { get; set; }
        public string EstadoLectura { get; set; }
        public int PaginaActual { get; set; }
        public string FechaUltimaLectura { get; set; }
    }
}
