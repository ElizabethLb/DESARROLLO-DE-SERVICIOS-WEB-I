using BookMatch.Models;

namespace BookMatch.Services.Interfaces
{
    public interface IOrdenService
    {
        Task<int> ConfirmarCompraAsync(int usuarioId, List<int> libroIds);
        Task<IEnumerable<Orden>> GetHistorialByUsuarioAsync(int usuarioId);
        Task<IEnumerable<DetalleOrden>> GetDetalleByOrdenAsync(int ordenId);
    }
}
