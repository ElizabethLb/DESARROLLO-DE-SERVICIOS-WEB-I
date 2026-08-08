using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("Ordenes")]
    public class Orden
    {
        [Key]
        public int OrdenID { get; set; }

        [StringLength(20)]
        public string Codigo { get; set; }

        public int UsuarioID { get; set; }

        public DateTime FechaOrden { get; set; } = DateTime.Now;

        [Required]
        [StringLength(10)]
        public string Subtotal { get; set; }

        [Required]
        [StringLength(10)]
        public string Impuesto { get; set; } = "0.00";

        [Required]
        [StringLength(10)]
        public string Total { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPago { get; set; } = "Tarjeta";

        [Required]
        [StringLength(20)]
        public string EstadoPago { get; set; } = "Completado";

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        public ICollection<DetalleOrden> DetalleOrdenes { get; set; }
        public ICollection<BibliotecaPersonal> BibliotecaPersonales { get; set; }
    }
}
