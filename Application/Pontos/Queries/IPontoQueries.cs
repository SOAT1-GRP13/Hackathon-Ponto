using Application.Pontos.Queries.DTO;
using Domain.Pontos;


namespace Application.Pontos.Queries
{
    public interface IPontoQueries
    {
        Task<Ponto?> ObterPontoById(Guid id);

        Task<PontoDto> ObterPontosByUserId(Guid userId, int dia, int mes, int ano);
    }
}
