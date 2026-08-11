using BookMatch.Models;

namespace BookMatch.Repositories.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetByEmailAsync(string email);

        Task<Usuario?> LoginAsync(
            string email,
            string passwordHash);

        Task<string?> GenerarTokenRecoveryAsync(
            string email);

        Task<bool> RestablecerPasswordAsync(
            string token,
            string nuevoPasswordHash);

        Task<bool> ActivarPerfilEscritorAsync(
            int usuarioId,
            bool activar);

        // ==========================================
        // SEGURIDAD - ADMINISTRADOR
        // ==========================================

        Task<IEnumerable<Usuario>> ListarUsuariosAsync();

        Task<IEnumerable<Usuario>> BuscarUsuariosAsync(
            string texto);

        Task<bool> RegistrarUsuarioAsync(
            Usuario usuario);

        Task<bool> ActualizarUsuarioAsync(
            Usuario usuario);

        Task<bool> CambiarEstadoUsuarioAsync(
            int usuarioId,
            string estado);
    }
}