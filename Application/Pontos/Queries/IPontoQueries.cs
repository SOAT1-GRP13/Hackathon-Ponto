using Application.Pontos.Queries.DTO;
using Domain.Pontos;


namespace Application.Pontos.Queries
{
    public interface IPontoQueries
    {
        Task<Ponto?> ObterPontoById(Guid id);

        Task<List<PontoDto>>  ObterPontosByUserId(Guid userId);
    }
}
