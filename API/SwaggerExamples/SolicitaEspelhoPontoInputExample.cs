using Application.Pontos.Boundaries;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace API.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
    public class SolicitaEspelhoPontoInputExample : IExamplesProvider<SolicitaEspelhoPontoInput>
    {
        public SolicitaEspelhoPontoInput GetExamples()
        {
            return new SolicitaEspelhoPontoInput(
                new Guid("dd1edfcd-1425-4cd8-a8a4-421967940eb5"),
                2,
                2024
            );
        }
    }
}
