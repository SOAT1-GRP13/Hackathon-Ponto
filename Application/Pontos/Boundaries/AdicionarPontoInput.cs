using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pontos.Boundaries
{
    public class AdicionarPontoInput
    {

        //[Required]
        //[SwaggerSchema(
        //    Description = "Data e hora do ponto",
        //    Format = "date-time")]
        //public DateTime DataHora { get; set; }

        [Required]
        [SwaggerSchema(
            Description = "Tipo do ponto",
            Format = "int")]
        public int TipoPonto { get; set; }

        [Required]
        [SwaggerSchema(
            Title = "Observacao",
            Format = "string")]
        public string? Observacao { get; set; }

    }
}
