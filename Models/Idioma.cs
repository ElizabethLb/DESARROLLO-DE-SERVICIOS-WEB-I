using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("Idiomas")]
    public class Idioma
    {
        [Key]
        public int IdiomaID { get; set; }

        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(5)]
        public string Codigo { get; set; }

        public ICollection<Libro> Libros { get; set; }
    }
}