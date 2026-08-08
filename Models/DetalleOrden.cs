using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatch.Models
{
    [Table("DetalleOrdenes")]
    public class DetalleOrden
    {
        [Key]
        public int DetalleID { get; set; }

        public int OrdenID { get; set; }
        public int LibroID { get; set; }

        [Required]
        [StringLength(10)]
        public string Precio { get; set; }

        public int EsGratuito { get; set; } = 0;

        [ForeignKey("OrdenID")]
        public Orden Orden { get; set; }

        [ForeignKey("LibroID")]
        public Libro Libro { get; set; }
    }
}
