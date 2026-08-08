using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("OpcionesRespuesta")]
    public class OpcionesRespuesta
    {
        [Key]
        public int OpcionID { get; set; }

        public int PreguntaID { get; set; }

        [Required]
        [StringLength(200)]
        public string Texto { get; set; }

        [ForeignKey("PreguntaID")]
        public Pregunta Pregunta { get; set; }

        public ICollection<RespuestasUsuario> RespuestasUsuario { get; set; }
    }
}