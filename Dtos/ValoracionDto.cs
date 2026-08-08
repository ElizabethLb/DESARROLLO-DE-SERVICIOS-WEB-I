namespace BookMatch.Dtos
{
    public class ValoracionDto
    {
        public int ValoracionID { get; set; }
        public int LibroID { get; set; }
        public int UsuarioID { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; }
        public string FechaValoracion { get; set; }
    }
}