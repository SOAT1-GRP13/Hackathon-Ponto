using AutoMapper;
using Domain.Base.DomainObjects;
using Domain.Pontos;


namespace Application.Pontos.UseCases
{
    public sealed class PontoUseCase : IPontoUseCase
    {
        private readonly IPontoRepository _pontoRepository;

        public PontoUseCase(
            IPontoRepository pontoRepository)
        {
            _pontoRepository = pontoRepository;
        }

        public async Task<bool> AdicionarPonto(DateTime dataHora, int tipoPonto, string observacao, Guid userId)
        {
            var ponto = new Ponto(dataHora, (TipoPontoEnum)tipoPonto, observacao, userId);
            await _pontoRepository.Adicionar(ponto);
            return await _pontoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AtualizarPonto(Guid pontoId, DateTime dataHora, int tipoPonto, string observacao, Guid userId)
        {
            var ponto = await _pontoRepository.ObterPorId(pontoId);
            if(ponto is null) 
                throw new DomainException("Pedido não encontrado!");

            ponto.DataHora = dataHora;
            ponto.TipoPonto = (TipoPontoEnum)tipoPonto;
            ponto.Observacao = observacao;
            ponto.UserId = userId;

            await _pontoRepository.Atualizar(ponto);
            return await _pontoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> RemoverPonto(Guid pontoId)
        {
            var ponto = await _pontoRepository.ObterPorId(pontoId);
            if (ponto is null)
                throw new DomainException("Pedido não encontrado!");

            await _pontoRepository.Remover(pontoId);
            return await _pontoRepository.UnitOfWork.Commit();
        }
        public void Dispose()
        {
            _pontoRepository?.Dispose();
        }
    }
}
