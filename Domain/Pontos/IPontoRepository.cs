using Domain.Base.Data;


namespace Domain.Pontos
{
    public interface IPontoRepository : IRepository
    {
        Task<Ponto> Adicionar(Ponto ponto);
        Task<Ponto> ObterPorId(Guid id);
        Task<IEnumerable<Ponto>> ObterPontosPorUsuario(Guid userId, int dia, int mes, int ano);
        Task<Ponto> Atualizar(Ponto ponto);
        Task<Ponto> Remover(Guid id);
    }
}
