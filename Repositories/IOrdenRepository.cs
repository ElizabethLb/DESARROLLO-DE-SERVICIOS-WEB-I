using BookMatch.Models;

namespace BookMatch.Repositories.Interfaces
{
    public interface IOrdenRepository : IGenericRepository<Orden>
    {
        /// <summary>
        /// Ejecuta sp_ConfirmarCompra: crea la Orden + DetalleOrdenes, actualiza BibliotecaPersonal
        /// y las estadísticas de ventas del autor, todo dentro de una transacción con ROLLBACK
        /// si algo falla. Devuelve el Id de la orden creada (0 si falló).
        /// </summary>
        Task<int> ConfirmarCompraAsync(int usuarioId, List<int> libroIds);

        Task<IEnumerable<Orden>> GetHistorialByUsuarioAsync(int usuarioId);
        Task<IEnumerable<DetalleOrden>> GetDetalleByOrdenAsync(int ordenId);
    }
}
