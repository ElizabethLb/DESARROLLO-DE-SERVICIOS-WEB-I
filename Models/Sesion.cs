using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("Sesiones")]
    public class Sesion
    {
        [Key]
        public int SesionID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        [StringLength(512)]
        public string Token { get; set; }

        [StringLength(45)]
        public string IP { get; set; }

        [StringLength(200)]
        public string Dispositivo { get; set; }

        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required]
        public DateTime FechaExpira { get; set; }

        public int Activa { get; set; } = 1;

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }
    }
}