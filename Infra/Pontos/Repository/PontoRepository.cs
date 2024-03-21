using Domain.Base.Data;
using Domain.Pontos;
using Microsoft.EntityFrameworkCore;

namespace Infra.Pontos.Repository
{
    public class PontoRepository : IPontoRepository
    {
        private readonly PontosContext _context;

        public PontoRepository(PontosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task<Ponto> Adicionar(Ponto ponto)
        {
            return (await _context.Pontos.AddAsync(ponto)).Entity;
        }

        public async Task<Ponto> Atualizar(Ponto ponto)
        {
            return _context.Pontos.Update(ponto).Entity;
        }

        public async Task<Ponto> ObterPorId(Guid id)
        {
            return await _context.Pontos.FindAsync(id);
        }

        public async Task<IEnumerable<Ponto>> ObterPontosPorUsuario(Guid userId)
        {
            return await _context.Pontos.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Ponto> Remover(Guid id)
        {
            var ponto = await _context.Pontos.FindAsync(id);
            return _context.Pontos.Remove(ponto).Entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
