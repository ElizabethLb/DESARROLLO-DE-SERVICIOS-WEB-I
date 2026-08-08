using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("Valoraciones")]
    public class Valoracion
    {
        [Key]
        public int ValoracionID { get; set; }

        public int LibroID { get; set; }
        public int UsuarioID { get; set; }

        [Required]
        public int Puntuacion { get; set; }

        public string Comentario { get; set; }

        public DateTime FechaValoracion { get; set; } = DateTime.Now;

        [ForeignKey("LibroID")]
        public Libro Libro { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }
    }
}