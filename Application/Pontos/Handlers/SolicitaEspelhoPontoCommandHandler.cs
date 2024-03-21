using MediatR;
using Domain.RabbitMQ;
using System.Text.Json;
using Domain.Configuration;
using Domain.Base.DomainObjects;
using Application.Pontos.Commands;
using Microsoft.Extensions.Options;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;

namespace Application.Pontos.Handlers
{
    public class SolicitaEspelhoPontoCommandHandler : IRequestHandler<SolicitaEspelhoPontoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly Secrets _secrets;

        public SolicitaEspelhoPontoCommandHandler(
            IMediatorHandler mediatorHandler,
            IRabbitMQService rabbitMQService,
            IOptions<Secrets> options
        )
        {
            _mediatorHandler = mediatorHandler;
            _rabbitMQService = rabbitMQService;
            _secrets = options.Value;
        }

        public async Task<bool> Handle(SolicitaEspelhoPontoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                foreach (var error in message.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

                return false;
            }

            try
            {
                string mensagem = JsonSerializer.Serialize(message.Input);
                _rabbitMQService.PublicaMensagem(_secrets.ExchangeEspelhoPonto, mensagem);
            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
                return false;
            }

            return true;
        }
    }
}
