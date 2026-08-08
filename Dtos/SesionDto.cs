namespace BookMatch.Dtos
{
    public class SesionDto
    {
        public int SesionID { get; set; }
        public int UsuarioID { get; set; }
        public string Token { get; set; }
        public string IP { get; set; }
        public string Dispositivo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaExpira { get; set; }
        public int Activa { get; set; }
    }
}
