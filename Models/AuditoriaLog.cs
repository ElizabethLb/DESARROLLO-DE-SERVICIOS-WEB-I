using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatch.Models
{
    [Table("AuditoriaLog")]
    public class AuditoriaLog
    {
        [Key]
        public int LogID { get; set; }

        public int? UsuarioID { get; set; }

        [Required]
        [StringLength(100)]
        public string Accion { get; set; }

        [StringLength(100)]
        public string Tabla { get; set; }

        public int? RegistroID { get; set; }

        public string Descripcion { get; set; }

        [StringLength(45)]
        public string IP { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }
    }
}