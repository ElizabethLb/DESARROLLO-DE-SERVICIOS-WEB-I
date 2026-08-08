namespace BookMatch.Dtos
{
    public class AuditoriaLogDto
    {
        public int LogID { get; set; }
        public int UsuarioID { get; set; }
        public string Accion { get; set; }
        public string Tabla { get; set; }
        public int RegistroID { get; set; }
        public string Descripcion { get; set; }
        public string IP { get; set; }
        public string Fecha { get; set; }
    }
}
