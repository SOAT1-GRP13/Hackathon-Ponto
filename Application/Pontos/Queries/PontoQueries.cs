using Application.Pontos.Queries.DTO;
using Domain.Pontos;


namespace Application.Pontos.Queries
{
    public class PontoQueries : IPontoQueries
    {
        private readonly IPontoRepository _pontoRepository;

        public PontoQueries(IPontoRepository pontoRepository)
        {
            _pontoRepository = pontoRepository;
        }

        public async Task<Ponto?> ObterPontoById(Guid id)
        {
            var ponto = await _pontoRepository.ObterPorId(id);
            if (ponto == null) return null;

            return ponto;
        }

        public async Task<List<PontoDto>> ObterPontosByUserId(Guid userId)
        {
            var pontos = await _pontoRepository.ObterPontosPorUsuario(userId);
            if (pontos == null) return new List<PontoDto>();

            return pontos.Select(ponto => new PontoDto
            {
                Id = ponto.Id,
                UserId = ponto.UserId,
                DataHora = TimeZoneInfo.ConvertTimeFromUtc(ponto.DataHora, TimeZoneInfo.Local),
                TipoPonto = (int)ponto.TipoPonto,
                TipoPontoDescricao = ponto.TipoPonto.ToString(),
                Observacao = ponto.Observacao
            }).ToList();
        }
    }
}
