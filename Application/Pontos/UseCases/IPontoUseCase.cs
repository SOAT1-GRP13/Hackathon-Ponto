using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pontos.UseCases
{
    public interface IPontoUseCase : IDisposable
    {
        Task<bool> AdicionarPonto(DateTime dataHora, int tipoPonto, string observacao, Guid userId);

        Task<bool> RemoverPonto(Guid pontoId);

        Task<bool> AtualizarPonto(Guid pontoId, DateTime dataHora, int tipoPonto, string observacao, Guid userId);

    }
}
