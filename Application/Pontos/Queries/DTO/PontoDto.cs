using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pontos.Queries.DTO
{
    public class PontoDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime DataHora { get; set; }
        public int TipoPonto { get; set; }
        public string? TipoPontoDescricao { get; set; }
        public string? Observacao { get; set; }

    }
}
