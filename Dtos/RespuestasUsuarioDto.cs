namespace BookMatch.Dtos
{
    public class RespuestasUsuarioDto
    {
        public int RespuestaID { get; set; }
        public int UsuarioID { get; set; }
        public int PreguntaID { get; set; }
        public int OpcionID { get; set; }
        public string FechaRespuesta { get; set; }
    }
}