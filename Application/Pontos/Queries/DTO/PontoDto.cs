namespace Application.Pontos.Queries.DTO
{
    public class PontoDto
    {
        public PontoDto()
        {
            TotalHoras = "00:00";
            Data = string.Empty;
            Batidas = new List<PontosPorData>();
        }

        public string TotalHoras { get; set; }
        public string Data { get; set; }
        public List<PontosPorData> Batidas { get; set; }

        public class PontosPorData
        {
            public PontosPorData()
            {
                Hora = string.Empty;
                TipoPontoDescricao = string.Empty;
                Observacao = string.Empty;
            }

            public DateTime DataHora { get; set; }
            public string Hora { get; set; }
            public int TipoPonto { get; set; }
            public string? TipoPontoDescricao { get; set; }
            public string? Observacao { get; set; }
        }
    }
}
