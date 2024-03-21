using MediatR;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Pontos;
using Infra.Pontos.Repository;
using Application.Pontos.Queries;
using Application.Pontos.UseCases;
using Infra.Pontos;
using Application.Pontos.Commands;
using Application.Pontos.Handlers;

namespace API.Setup
{
    public static class DependencyInjection
    { 
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Domain Notifications 
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Pedidos
            services.AddScoped<IPontoRepository, PontoRepository>();
            services.AddScoped<IPontoQueries, PontoQueries>();
            services.AddScoped<IPontoUseCase, PontoUseCase>();
            services.AddScoped<PontosContext>();

            services.AddScoped<IRequestHandler<AdicionarPontoCommand, bool>, AdicionarPontoCommandHandler>();

        }
    }
}
