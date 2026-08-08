using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BookMatch.Data;
using BookMatch.Models;
using BookMatch.Repositories.Interfaces;

namespace BookMatch.Repositories.Implementations
{
    public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
    {
        public OrdenRepository(BookMatchContext context) : base(context) { }

        public async Task<int> ConfirmarCompraAsync(int usuarioId, List<int> libroIds)
        {
            if (libroIds is null || !libroIds.Any())
                return 0;

            var libroIdsCsv = string.Join(",", libroIds);

            await using var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = "sp_ConfirmarCompra";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UsuarioId", usuarioId));
            command.Parameters.Add(new SqlParameter("@LibroIds", libroIdsCsv));

            var ordenIdParam = new SqlParameter("@OrdenId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(ordenIdParam);

            var exitoParam = new SqlParameter("@Exito", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(exitoParam);

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                return 0;
            }

            var exito = exitoParam.Value != DBNull.Value && (bool)exitoParam.Value;
            if (!exito) return 0;

            return ordenIdParam.Value == DBNull.Value ? 0 : (int)ordenIdParam.Value;
        }

        public async Task<IEnumerable<Orden>> GetHistorialByUsuarioAsync(int usuarioId)
            => await _dbSet
                .Where(o => o.UsuarioID == usuarioId)
                .Include(o => o.DetalleOrdenes)
                .OrderByDescending(o => o.FechaOrden)
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<DetalleOrden>> GetDetalleByOrdenAsync(int ordenId)
            => await _context.Set<DetalleOrden>()
                .Where(d => d.OrdenID == ordenId)
                .Include(d => d.Libro)
                .AsNoTracking()
                .ToListAsync();
    }
}
