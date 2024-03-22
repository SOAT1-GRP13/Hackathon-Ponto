using Application.Pontos.Commands;
using Application.Pontos.Handlers;
using Application.Pontos.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using Moq;

namespace Application.Tests
{
    public class AdicionarPontoCommandHandlerTests
    {
        private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
        private readonly Mock<IPontoUseCase> _pontoUseCaseMock;
        private readonly AdicionarPontoCommandHandler _handler;

        public AdicionarPontoCommandHandlerTests()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _pontoUseCaseMock = new Mock<IPontoUseCase>();
            _handler = new AdicionarPontoCommandHandler(_mediatorHandlerMock.Object, _pontoUseCaseMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_AddsPontoSuccessfully()
        {
            // Arrange
            var command = new AdicionarPontoCommand(DateTime.Now, 1, "Observação", Guid.NewGuid());
            _pontoUseCaseMock.Setup(p => p.AdicionarPonto(command.DataHora, command.TipoPonto, command.Observacao, command.UserId)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_PublishesNotifications()
        {
            // Arrange
            var command = new AdicionarPontoCommand(default, 0, null, Guid.Empty); // Intentionally invalid

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mediatorHandlerMock.Verify(m => m.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Handle_FailureToAddPonto_PublishesErrorNotification()
        {
            // Arrange
            var command = new AdicionarPontoCommand(DateTime.Now, 1, "Observação", Guid.NewGuid());
            _pontoUseCaseMock.Setup(p => p.AdicionarPonto(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mediatorHandlerMock.Verify(m => m.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Once);
        }
    }
}
