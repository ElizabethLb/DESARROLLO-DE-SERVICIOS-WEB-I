using BookMatch.Models;
using BookMatch.Repositories.Interfaces;
using BookMatch.Services.Interfaces;

namespace BookMatch.Services.Implementations
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;

        public OrdenService(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public Task<int> ConfirmarCompraAsync(int usuarioId, List<int> libroIds)
            => _ordenRepository.ConfirmarCompraAsync(usuarioId, libroIds);

        public Task<IEnumerable<Orden>> GetHistorialByUsuarioAsync(int usuarioId)
            => _ordenRepository.GetHistorialByUsuarioAsync(usuarioId);

        public Task<IEnumerable<DetalleOrden>> GetDetalleByOrdenAsync(int ordenId)
            => _ordenRepository.GetDetalleByOrdenAsync(ordenId);
    }
}
