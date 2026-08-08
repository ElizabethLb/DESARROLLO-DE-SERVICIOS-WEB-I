using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMatch.Models
{
	[Table("Usuarios")]
	public class Usuario
	{
		[Key]
		public int UsuarioID { get; set; }

		[Required]
		[StringLength(120)]
		public string Nombre { get; set; }

		[Required]
		[StringLength(120)]
		public string Apellido { get; set; }

		[Required]
		[StringLength(200)]
		public string Email { get; set; }

		[Required]
		[StringLength(256)]
		public string PasswordHash { get; set; }

		public int RolID { get; set; }

		public int EsEscritor { get; set; } = 0;

		public string Biografia { get; set; }

		[StringLength(500)]
		public string FotoPerfil { get; set; }

		[Required]
		[StringLength(20)]
		public string Estado { get; set; } = "Activo";

		public DateTime FechaRegistro { get; set; } = DateTime.Now;

		public DateTime? UltimoAcceso { get; set; }

		[StringLength(256)]
		public string TokenRecovery { get; set; }

		public DateTime? TokenExpira { get; set; }

		[ForeignKey("RolID")]
		public Rol Rol { get; set; }

		public ICollection<Libro> LibrosEscritos { get; set; }
		public ICollection<Valoracion> Valoraciones { get; set; }
		public ICollection<RespuestasUsuario> RespuestasUsuario { get; set; }
		public ICollection<Carrito> Carrito { get; set; }
		public ICollection<Orden> Ordenes { get; set; }
		public ICollection<BibliotecaPersonal> BibliotecaPersonal { get; set; }
		public ICollection<Sesion> Sesiones { get; set; }
		public ICollection<AuditoriaLog> AuditoriaLogs { get; set; }
	}
}