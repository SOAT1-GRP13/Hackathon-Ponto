using Domain.Base.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pontos.Commands
{
    public class AdicionarPontoCommand : Command<bool>
    {
        //Um comando representa uma intenção de mudança do estado da entidade no banco e na aplicação.

        public DateTime DataHora { get; set; }

        public int TipoPonto { get; set; }

        public string? Observacao { get; set; }

        public Guid UserId { get; set; }

        public AdicionarPontoCommand(DateTime dataHora, int tipoPonto, string? observacao, Guid userId)
        {
            DataHora = dataHora;
            TipoPonto = tipoPonto;
            Observacao = observacao;
            UserId = userId;
        }
        public override bool EhValido()
        {
            ValidationResult = new AdicionarPontoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarPontoCommandValidation : AbstractValidator<AdicionarPontoCommand>
        {
            public static string DataHoraErroMsg => "DataHora é obrigatório";
            public static string TipoPontoErroMsg => "TipoPonto é obrigatório";

            public static string UserIdErroMsg => "UserId é obrigatório";
            public AdicionarPontoCommandValidation()
            {
                RuleFor(c => c.DataHora)
                    .NotEmpty().WithMessage("DataHora é obrigatório");

                RuleFor(c => c.TipoPonto)
                    .NotNull().WithMessage("TipoPonto é obrigatório");

                RuleFor(c => c.UserId)
                    .NotEmpty().WithMessage("UserId é obrigatório");
            }
        }

    }
}
