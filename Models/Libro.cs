using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatch.Models
{
    [Table("Libros")]
    public class Libro
    {
        [Key]
        public int LibroID { get; set; }

        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }

        public int AutorID { get; set; }
        public int CategoriaID { get; set; }
        public int IdiomaID { get; set; }

        public string Sinopsis { get; set; }

        [Required]
        [StringLength(10)]
        public string Precio { get; set; } = "0.00";

        public int EsGratuito { get; set; } = 0;

        public int? Paginas { get; set; }

        [StringLength(500)]
        public string Portada { get; set; }

        [StringLength(500)]
        public string ArchivoURL { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Borrador";

        public DateTime? FechaPublicacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }

        public int TotalVentas { get; set; } = 0;
        public int TotalDescargas { get; set; } = 0;

        [ForeignKey("AutorID")]
        public Usuario Autor { get; set; }

        [ForeignKey("CategoriaID")]
        public Categoria Categoria { get; set; }

        [ForeignKey("IdiomaID")]
        public Idioma Idioma { get; set; }

        public ICollection<Valoracion> Valoraciones { get; set; }
        public ICollection<Carrito> CarritoItems { get; set; }
        public ICollection<DetalleOrden> DetalleOrdenes { get; set; }
        public ICollection<BibliotecaPersonal> BibliotecaPersonales { get; set; }
    }
}