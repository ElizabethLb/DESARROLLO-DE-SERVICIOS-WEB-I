using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatch.Models
{
    [Table("BibliotecaPersonal")]
    public class BibliotecaPersonal
    {
        [Key]
        public int BibliotecaID { get; set; }

        public int UsuarioID { get; set; }
        public int LibroID { get; set; }
        public int? OrdenID { get; set; }

        public DateTime FechaAdquisicion { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string EstadoLectura { get; set; } = "Pendiente";

        public int PaginaActual { get; set; } = 0;

        public DateTime? FechaUltimaLectura { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("LibroID")]
        public Libro Libro { get; set; }

        [ForeignKey("OrdenID")]
        public Orden Orden { get; set; }
    }
}