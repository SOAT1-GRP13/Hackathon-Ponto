using Application.Pontos.Boundaries;
using Application.Pontos.Commands;
using Application.Pontos.Queries;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Pontos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Endpoints relacionados a marcacao de ponto, sendo necessário se autenticar e o clienteId é pego de forma automatica")]
    public class PontosController : ControllerBase
    {
        private readonly IPontoQueries _pontoQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public PontosController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IPontoQueries pontoQueries) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _pontoQueries = pontoQueries;

        }

        [HttpPost("adicionar-ponto")]
        [Authorize]
        [SwaggerOperation(
             Summary = "Adicionar registro de ponto",
             Description = "Adiciona ponto ao registro (0: Entrada - 1: Almoco - 2: Retorno - 3: Saída)")]
       // [SwaggerResponse(200, "Retorna dados do ponto", typeof(Ponto))]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio", typeof(IEnumerable<string>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> AdicionarPonto([FromBody] AdicionarPontoInput input)
        {
            try
            {
                var dataHora = DateTime.UtcNow;
                var command = new AdicionarPontoCommand(dataHora, input.TipoPonto, input.Observacao, ObterUserId());
                await _mediatorHandler.EnviarComando<AdicionarPontoCommand, bool>(command);

                if (!OperacaoValida())
                    return StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());

                return Ok($"Ponto registrado com sucesso às: {TimeZoneInfo.ConvertTimeFromUtc(dataHora, TimeZoneInfo.Local)}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar adicionar ponto ao registro. Erro: {ex.Message}");
            }

        }

        [HttpGet("obter-pontos")]
        [Authorize]
        [SwaggerOperation(
                        Summary = "Obter registros de ponto",
                        Description = "Obtem todos os registros de ponto")]
        [SwaggerResponse(200, "Retorna dados dos pontos", typeof(IEnumerable<Ponto>))]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio", typeof(IEnumerable<string>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> ObterPontosPorUsuario(int dia, int mes, int ano)
        {
            try
            {
                var userId = ObterUserId();
                var pontos = await _pontoQueries.ObterPontosByUserId(userId, dia, mes, ano);
                return Ok(pontos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar obter pontos. Erro: {ex.Message}");
            }
        }

        [HttpGet("solicita-espelho-ponto")]
        [Authorize]
        [SwaggerOperation(
             Summary = "Solicita espelho de ponto",
             Description = "Solicita o espelho de ponto por email")]
        [SwaggerResponse(400, "Caso não obedeça alguma regra de negocio", typeof(IEnumerable<string>))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> SolicitaEspelhoPonto(int mes, int ano)
        {
            try
            {
                var dataHora = DateTime.UtcNow;
                var input = new SolicitaEspelhoPontoInput(ObterUserId(), mes, ano, ObterUserEmail());
                var command = new SolicitaEspelhoPontoCommand(input);
                await _mediatorHandler.EnviarComando<SolicitaEspelhoPontoCommand, bool>(command);

                if (!OperacaoValida())
                    return StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());

                return Ok($"Espelho de ponto solicitado com sucesso às: {TimeZoneInfo.ConvertTimeFromUtc(dataHora, TimeZoneInfo.Local)}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar solicitar espelho de ponto. Erro: {ex.Message}");
            }

        }
    }
}
