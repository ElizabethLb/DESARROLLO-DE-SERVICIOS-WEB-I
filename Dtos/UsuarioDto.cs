namespace BookMatch.Dtos
{
    public class UsuarioDto
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RolID { get; set; }
        public int EsEscritor { get; set; }
        public string Biografia { get; set; }
        public string FotoPerfil { get; set; }
        public string Estado { get; set; }
        public string FechaRegistro { get; set; }
        public string UltimoAcceso { get; set; }
        public string TokenRecovery { get; set; }
        public string TokenExpira { get; set; }
    }
}