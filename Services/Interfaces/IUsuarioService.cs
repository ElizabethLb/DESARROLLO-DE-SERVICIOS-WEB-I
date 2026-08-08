using BookMatch.Models;

namespace BookMatch.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> LoginAsync(string email, string passwordHash);
        Task<string?> GenerarTokenRecoveryAsync(string email);
        Task<bool> RestablecerPasswordAsync(string token, string nuevoPasswordHash);
        Task<bool> ActivarPerfilEscritorAsync(int usuarioId, bool activar);
        Task<IEnumerable<Usuario>> GestionUsuarioAsync(string accion, int? usuarioId, Usuario? datos);
    }
}
