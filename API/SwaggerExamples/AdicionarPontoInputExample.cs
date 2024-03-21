
using Application.Pontos.Boundaries;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace API.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
    public class AdicionarPontoInputExample : IExamplesProvider<AdicionarPontoInput>
    {
        public AdicionarPontoInput GetExamples()
        {
            return new AdicionarPontoInput
            {               
                TipoPonto = 0,
                Observacao = "Teste"

            };
        }
    }
}
