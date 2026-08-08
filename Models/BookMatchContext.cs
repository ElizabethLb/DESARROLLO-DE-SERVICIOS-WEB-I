using Microsoft.EntityFrameworkCore;

namespace BookMatchBD.Models
{
    public class BookMatchContext : DbContext
    {
        public BookMatchContext(DbContextOptions<BookMatchContext> options) : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<OpcionesRespuesta> OpcionesRespuesta { get; set; }
        public DbSet<RespuestasUsuario> RespuestasUsuario { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }
        public DbSet<BibliotecaPersonal> BibliotecaPersonal { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<AuditoriaLog> AuditoriaLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // este seria la Configuracion del índice único compuesto descrito en el script del SQL
            modelBuilder.Entity<Valoracion>()
                .HasIndex(v => new { v.LibroID, v.UsuarioID })
                .IsUnique();

            modelBuilder.Entity<Carrito>()
                .HasIndex(c => new { c.UsuarioID, c.LibroID })
                .IsUnique();

            modelBuilder.Entity<DetalleOrden>()
                .HasIndex(d => new { d.OrdenID, d.LibroID })
                .IsUnique();

            modelBuilder.Entity<BibliotecaPersonal>()
                .HasIndex(bp => new { bp.UsuarioID, bp.LibroID })
                .IsUnique();
        }
    }
}