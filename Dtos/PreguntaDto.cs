namespace BookMatch.Dtos
{
    public class PreguntaDto
    {
        public int PreguntaID { get; set; }
        public int Orden { get; set; }
        public string Texto { get; set; }
        public string TipoRespuesta { get; set; }
        public int Activa { get; set; }
    }
}