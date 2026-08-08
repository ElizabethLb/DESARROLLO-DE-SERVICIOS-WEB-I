using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatch.Models
{
    [Table("Preguntas")]
    public class Pregunta
    {
        [Key]
        public int PreguntaID { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        [StringLength(300)]
        public string Texto { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoRespuesta { get; set; } = "simple";

        public int Activa { get; set; } = 1;

        public ICollection<OpcionesRespuesta> OpcionesRespuesta { get; set; }
        public ICollection<RespuestasUsuario> RespuestasUsuario { get; set; }
    }
}