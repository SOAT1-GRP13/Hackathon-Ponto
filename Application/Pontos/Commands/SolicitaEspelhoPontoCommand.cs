using FluentValidation;
using Domain.Base.Messages;
using Application.Pontos.Boundaries;

namespace Application.Pontos.Commands
{
    public class SolicitaEspelhoPontoCommand : Command<bool>
    {
        public SolicitaEspelhoPontoInput Input { get; set; }

        public SolicitaEspelhoPontoCommand(SolicitaEspelhoPontoInput input)
        {
            Input = input;
        }

        public override bool EhValido()
        {
            ValidationResult = new SolicitaEspelhoPontoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class SolicitaEspelhoPontoCommandValidation : AbstractValidator<SolicitaEspelhoPontoCommand>
        {
            public SolicitaEspelhoPontoCommandValidation()
            {
                RuleFor(c => c.Input.Mes)
                    .NotEmpty().WithMessage("Mes é obrigatório");

                RuleFor(c => c.Input.Ano)
                    .NotNull().WithMessage("Ano é obrigatório");

                RuleFor(c => c.Input.UserId)
                    .NotEmpty().WithMessage("UserId é obrigatório");
            }
        }
    }
}
