using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BookMatch.Data;
using BookMatch.Models;
using BookMatch.Repositories.Interfaces;

namespace BookMatch.Repositories.Implementations
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(BookMatchContext context) : base(context) { }

        public async Task<Usuario?> GetByEmailAsync(string email)
            => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<Usuario?> LoginAsync(string email, string passwordHash)
        {
            // sp_LoginUsuario valida email + hash y devuelve la fila del usuario (o vacío si falla)
            var resultado = await _dbSet
                .FromSqlInterpolated($"EXEC sp_LoginUsuario @Email = {email}, @PasswordHash = {passwordHash}")
                .AsNoTracking()
                .ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<string?> GenerarTokenRecoveryAsync(string email)
        {
            // Este SP devuelve un único valor escalar (el token), por eso usamos ADO.NET directo
            // en vez de FromSqlInterpolated (que está pensado para result sets de entidades).
            await using var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = "sp_GenerarTokenRecovery";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Email", email));
            var tokenParam = new SqlParameter("@Token", System.Data.SqlDbType.NVarChar, 200)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(tokenParam);

            await command.ExecuteNonQueryAsync();

            return tokenParam.Value == DBNull.Value ? null : (string)tokenParam.Value;
        }

        public async Task<bool> RestablecerPasswordAsync(string token, string nuevoPasswordHash)
        {
            var usuario = await _dbSet.FirstOrDefaultAsync(u => u.TokenRecovery == token);
            if (usuario is null) return false;

            usuario.PasswordHash = nuevoPasswordHash;
            usuario.TokenRecovery = null;
            Update(usuario);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> ActivarPerfilEscritorAsync(int usuarioId, bool activar)
        {
            var usuario = await GetByIdAsync(usuarioId);
            if (usuario is null) return false;

            usuario.EsEscritor = activar;
            Update(usuario);
            return await SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Usuario>> GestionUsuarioAsync(string accion, int? usuarioId, Usuario? datos)
        {
            // sp_GestionUsuario centraliza LISTAR / CREAR / EDITAR / ELIMINAR / TOGGLE_ESTADO
            // según el parámetro @Accion, usado desde el módulo de Seguridad (solo Administrador).
            var parametros = new[]
            {
                new SqlParameter("@Accion", accion),
                new SqlParameter("@UsuarioId", (object?)usuarioId ?? DBNull.Value),
                new SqlParameter("@Nombre", (object?)datos?.Nombre ?? DBNull.Value),
                new SqlParameter("@Email", (object?)datos?.Email ?? DBNull.Value),
                new SqlParameter("@RolId", (object?)datos?.RolId ?? DBNull.Value),
                new SqlParameter("@Estado", (object?)datos?.Estado ?? DBNull.Value),
            };

            return await _dbSet
                .FromSqlRaw(
                    "EXEC sp_GestionUsuario @Accion, @UsuarioId, @Nombre, @Email, @RolId, @Estado",
                    parametros)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
