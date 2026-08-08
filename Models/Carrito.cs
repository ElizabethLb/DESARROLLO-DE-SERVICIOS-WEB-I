using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("Carrito")]
    public class Carrito
    {
        [Key]
        public int CarritoID { get; set; }

        public int UsuarioID { get; set; }
        public int LibroID { get; set; }

        public DateTime FechaAgregado { get; set; } = DateTime.Now;

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("LibroID")]
        public Libro Libro { get; set; }
    }
}