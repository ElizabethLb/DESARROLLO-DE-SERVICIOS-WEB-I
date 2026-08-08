using BookMatch.Models;

namespace BookMatch.Repositories.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        /// <summary>Busca un usuario por su correo (para validar duplicados, recovery, etc.)</summary>
        Task<Usuario?> GetByEmailAsync(string email);

        /// <summary>Ejecuta sp_LoginUsuario. Devuelve el usuario si las credenciales son válidas, null si no.</summary>
        Task<Usuario?> LoginAsync(string email, string passwordHash);

        /// <summary>Ejecuta sp_GenerarTokenRecovery. Devuelve el token generado para el correo indicado.</summary>
        Task<string?> GenerarTokenRecoveryAsync(string email);

        /// <summary>Valida el token de recovery y actualiza la contraseña.</summary>
        Task<bool> RestablecerPasswordAsync(string token, string nuevoPasswordHash);

        /// <summary>Activa o desactiva el perfil de escritor del usuario (rol dual lector/escritor).</summary>
        Task<bool> ActivarPerfilEscritorAsync(int usuarioId, bool activar);

        /// <summary>Ejecuta sp_GestionUsuario para operaciones CRUD desde el panel de Administrador.</summary>
        Task<IEnumerable<Usuario>> GestionUsuarioAsync(string accion, int? usuarioId, Usuario? datos);
    }
}
