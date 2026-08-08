using BookMatch.Models;
using BookMatch.Repositories.Interfaces;
using BookMatch.Services.Interfaces;

namespace BookMatch.Services.Implementations
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository;

        public CarritoService(ICarritoRepository carritoRepository)
        {
            _carritoRepository = carritoRepository;
        }

        public Task<IEnumerable<Carrito>> GetByUsuarioAsync(int usuarioId)
            => _carritoRepository.GetByUsuarioAsync(usuarioId);

        public Task<bool> AgregarAsync(int usuarioId, int libroId)
            => _carritoRepository.AgregarAsync(usuarioId, libroId);

        public Task<bool> QuitarAsync(int usuarioId, int libroId)
            => _carritoRepository.QuitarAsync(usuarioId, libroId);

        public Task<bool> VaciarCarritoAsync(int usuarioId)
            => _carritoRepository.VaciarCarritoAsync(usuarioId);
    }
}
