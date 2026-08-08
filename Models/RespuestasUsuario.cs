using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatchBD.Models
{
    [Table("RespuestasUsuario")]
    public class RespuestasUsuario
    {
        [Key]
        public int RespuestaID { get; set; }

        public int UsuarioID { get; set; }
        public int PreguntaID { get; set; }
        public int OpcionID { get; set; }

        public DateTime FechaRespuesta { get; set; } = DateTime.Now;

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("PreguntaID")]
        public Pregunta Pregunta { get; set; }

        [ForeignKey("OpcionID")]
        public OpcionesRespuesta OpcionRespuesta { get; set; }
    }
}