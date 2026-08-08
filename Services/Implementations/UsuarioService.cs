using BookMatch.Models;
using BookMatch.Repositories.Interfaces;
using BookMatch.Services.Interfaces;

namespace BookMatch.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<Usuario?> GetByEmailAsync(string email)
            => _usuarioRepository.GetByEmailAsync(email);

        public Task<Usuario?> LoginAsync(string email, string passwordHash)
            => _usuarioRepository.LoginAsync(email, passwordHash);

        public Task<string?> GenerarTokenRecoveryAsync(string email)
            => _usuarioRepository.GenerarTokenRecoveryAsync(email);

        public Task<bool> RestablecerPasswordAsync(string token, string nuevoPasswordHash)
            => _usuarioRepository.RestablecerPasswordAsync(token, nuevoPasswordHash);

        public Task<bool> ActivarPerfilEscritorAsync(int usuarioId, bool activar)
            => _usuarioRepository.ActivarPerfilEscritorAsync(usuarioId, activar);

        public Task<IEnumerable<Usuario>> GestionUsuarioAsync(string accion, int? usuarioId, Usuario? datos)
            => _usuarioRepository.GestionUsuarioAsync(accion, usuarioId, datos);
    }
}
