using Application.Pontos.Boundaries;
using Application.Pontos.Commands;
using Application.Pontos.Handlers;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Configuration;
using Domain.RabbitMQ;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.Tests
{
    public class SolicitaEspelhoPontoCommandHandlerTests
    {
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
        private readonly Mock<IRabbitMQService> _rabbitMQServiceMock;
        private readonly Mock<IOptions<Secrets>> _optionsMock;
        private readonly SolicitaEspelhoPontoCommandHandler _handler;

        public SolicitaEspelhoPontoCommandHandlerTests()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _rabbitMQServiceMock = new Mock<IRabbitMQService>();
            _optionsMock = new Mock<IOptions<Secrets>>();
            _optionsMock.Setup(x => x.Value).Returns(new Secrets {  });

            _handler = new SolicitaEspelhoPontoCommandHandler(_mediatorHandlerMock.Object, _rabbitMQServiceMock.Object, _optionsMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_Success()
        {
            // Arrange
            var validCommand = new SolicitaEspelhoPontoCommand(new SolicitaEspelhoPontoInput(Guid.NewGuid(), 1, 2020));

            // Act
            var result = await _handler.Handle(validCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _rabbitMQServiceMock.Verify(r => r.PublicaMensagem(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_PublishesNotificationsAndReturnsFalse()
        {
            // Arrange
            var invalidCommand = new SolicitaEspelhoPontoCommand(new SolicitaEspelhoPontoInput(Guid.Empty, 0, 0)); // Propositalmente inválido

            // Act
            var result = await _handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mediatorHandlerMock.Verify(m => m.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Handle_DomainException_PublishesNotificationAndReturnsFalse()
        {
            // Arrange
            var validCommand = new SolicitaEspelhoPontoCommand(new SolicitaEspelhoPontoInput(Guid.NewGuid(), 1, 2020));
            _rabbitMQServiceMock.Setup(r => r.PublicaMensagem(It.IsAny<string>(), It.IsAny<string>()))
                                .Throws(new DomainException("Erro de domínio simulado"));

            // Act
            var result = await _handler.Handle(validCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mediatorHandlerMock.Verify(m => m.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Exception_ReturnsFalse()
        {
            // Arrange
            var validCommand = new SolicitaEspelhoPontoCommand(new SolicitaEspelhoPontoInput(Guid.NewGuid(), 1, 2020));
            _rabbitMQServiceMock.Setup(r => r.PublicaMensagem(It.IsAny<string>(), It.IsAny<string>()))
                                .Throws(new Exception("Erro inesperado"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(validCommand, CancellationToken.None));
        }

    }
}
