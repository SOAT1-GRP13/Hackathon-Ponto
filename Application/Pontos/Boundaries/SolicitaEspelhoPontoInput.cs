using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pontos.Boundaries
{
    public class SolicitaEspelhoPontoInput
    {
        [Required]
        [SwaggerSchema(
            Description = "Id do usuário",
            Format = "Guid")]
        public Guid UserId { get; set; }

        [Required]
        [SwaggerSchema(
            Description = "Mês",
            Format = "int")]
        public int Mes { get; set; }

        [Required]
        [SwaggerSchema(
            Description = "Ano",
            Format = "int")]
        public int Ano { get; set; }

        public SolicitaEspelhoPontoInput(Guid userId, int mes, int ano)
        {
            UserId = userId;
            Mes = mes;
            Ano = ano;
        }

    }
}
