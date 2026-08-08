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

            // sp_ConfirmarCompra recibe la lista de libros como string separado por comas
            // (STRING_SPLIT del lado del SP). Alternativa más robusta: Table-Valued Parameter,
            // pero esto mantiene el SP simple si no lo definieron con TVP.
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
                // El SP ya maneja ROLLBACK internamente ante error; si aun así explota
                // (ej. timeout, conexión), devolvemos 0 para que el Service lo trate como fallo.
                return 0;
            }

            var exito = exitoParam.Value != DBNull.Value && (bool)exitoParam.Value;
            if (!exito) return 0;

            return ordenIdParam.Value == DBNull.Value ? 0 : (int)ordenIdParam.Value;
        }

        public async Task<IEnumerable<Orden>> GetHistorialByUsuarioAsync(int usuarioId)
            => await _dbSet
                .Where(o => o.UsuarioId == usuarioId)
                .Include(o => o.DetalleOrdenes)
                .OrderByDescending(o => o.FechaOrden)
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<DetalleOrden>> GetDetalleByOrdenAsync(int ordenId)
            => await _context.Set<DetalleOrden>()
                .Where(d => d.OrdenId == ordenId)
                .Include(d => d.Libro)
                .AsNoTracking()
                .ToListAsync();
    }
}
