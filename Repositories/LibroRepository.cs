using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BookMatch.Data;
using BookMatch.Models;
using BookMatch.Repositories.Interfaces;

namespace BookMatch.Repositories.Implementations
{
    public class LibroRepository : GenericRepository<Libro>, ILibroRepository
    {
        public LibroRepository(BookMatchContext context) : base(context) { }

        public async Task<(IEnumerable<Libro> Libros, int Total)> ObtenerCatalogoAsync(FiltroCatalogo filtro)
        {
            // sp_ObtenerCatalogo recibe los 6 filtros + paginación y devuelve:
            //  - result set con los libros de la página actual
            //  - @Total como parámetro de salida con el total de registros (para el paginador)
            await using var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = "sp_ObtenerCatalogo";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Busqueda", (object?)filtro.Busqueda ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@CategoriaId", (object?)filtro.CategoriaId ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@IdiomaId", (object?)filtro.IdiomaId ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@SoloGratis", (object?)filtro.SoloGratis ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ValoracionMinima", (object?)filtro.ValoracionMinima ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Pagina", filtro.Pagina));
            command.Parameters.Add(new SqlParameter("@TamanioPagina", filtro.TamanioPagina));

            var totalParam = new SqlParameter("@Total", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(totalParam);

            var libros = new List<Libro>();
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    libros.Add(MapLibro(reader));
                }
            }

            var total = totalParam.Value == DBNull.Value ? 0 : (int)totalParam.Value;
            return (libros, total);
        }

        public async Task<Libro?> ObtenerDetalleAsync(int libroId)
        {
            var resultado = await _dbSet
                .FromSqlInterpolated($"EXEC sp_DetalleLibro @LibroId = {libroId}")
                .AsNoTracking()
                .ToListAsync();

            return resultado.FirstOrDefault();
        }

        public async Task<int> PublicarLibroAsync(Libro libro)
        {
            // sp_PublicarLibro hace INSERT o UPDATE según si LibroId viene en 0/null,
            // y devuelve el Id del libro publicado (nuevo o actualizado).
            await using var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = "sp_PublicarLibro";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Si LibroID > 0 enviamos el id, sino DBNull (nuevo registro)
            command.Parameters.Add(new SqlParameter("@LibroId", libro.LibroID > 0 ? (object)libro.LibroID : DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Titulo", libro.Titulo));
            command.Parameters.Add(new SqlParameter("@CategoriaId", libro.CategoriaID));
            command.Parameters.Add(new SqlParameter("@IdiomaId", libro.IdiomaID));
            command.Parameters.Add(new SqlParameter("@AutorId", libro.AutorID));
            command.Parameters.Add(new SqlParameter("@Precio", libro.Precio));
            command.Parameters.Add(new SqlParameter("@EsGratuito", libro.EsGratuito));
            command.Parameters.Add(new SqlParameter("@Descripcion", (object?)libro.Sinopsis ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@PortadaUrl", (object?)libro.Portada ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ArchivoUrl", (object?)libro.ArchivoURL ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Estado", libro.Estado));

            var idParam = new SqlParameter("@LibroIdResultado", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(idParam);

            await command.ExecuteNonQueryAsync();

            return idParam.Value == DBNull.Value ? 0 : (int)idParam.Value;
        }

        public async Task<IEnumerable<Libro>> GetByAutorAsync(int usuarioId)
            => await _dbSet.Where(l => l.AutorID == usuarioId).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Libro>> GetByCategoriaAsync(int categoriaId)
            => await _dbSet.Where(l => l.CategoriaID == categoriaId).AsNoTracking().ToListAsync();

        /// <summary>
        /// Mapea manualmente el SqlDataReader a la entidad Libro.
        /// AJUSTAR nombres de columnas/propiedades cuando tu compañero termine Models
        /// si difieren de lo que devuelve sp_ObtenerCatalogo en el script SQL.
        /// </summary>
        private static Libro MapLibro(System.Data.Common.DbDataReader reader)
        {
            return new Libro
            {
                LibroID = reader.GetInt32(reader.GetOrdinal("LibroId")),
                Codigo = reader["Codigo"]?.ToString(),
                Titulo = reader["Titulo"]?.ToString() ?? string.Empty,
                CategoriaID = reader.GetInt32(reader.GetOrdinal("CategoriaId")),
                IdiomaID = reader.GetInt32(reader.GetOrdinal("IdiomaId")),
                AutorID = reader.GetInt32(reader.GetOrdinal("AutorId")),
                Precio = reader["Precio"] == DBNull.Value ? "0.00" : Convert.ToDecimal(reader["Precio"]).ToString("F2"),
                EsGratuito = reader["EsGratuito"] != DBNull.Value && Convert.ToBoolean(reader["EsGratuito"]) ? 1 : 0,
                Sinopsis = reader["Descripcion"]?.ToString(),
                Portada = reader["PortadaUrl"]?.ToString(),
                Estado = reader["Estado"]?.ToString() ?? string.Empty
            };
        }
    }
}
