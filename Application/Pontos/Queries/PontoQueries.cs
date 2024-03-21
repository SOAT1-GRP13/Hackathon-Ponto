using Application.Pontos.Queries.DTO;
using Domain.Pontos;


namespace Application.Pontos.Queries
{
    public class PontoQueries : IPontoQueries
    {
        private readonly IPontoRepository _pontoRepository;

        public PontoQueries(IPontoRepository pontoRepository)
        {
            _pontoRepository = pontoRepository;
        }

        public async Task<Ponto?> ObterPontoById(Guid id)
        {
            var ponto = await _pontoRepository.ObterPorId(id);
            if (ponto == null) return null;

            return ponto;
        }

        public async Task<PontoDto> ObterPontosByUserId(Guid userId, int dia, int mes, int ano)
        {
            var pontos = await _pontoRepository.ObterPontosPorUsuario(userId, dia, mes, ano);
            if (pontos == null) return new PontoDto();

            var pontosPorData = pontos.Select(p => new PontoDto.PontosPorData
            {
                DataHora = p.DataHora,
                Hora = p.DataHora.ToString("HH:mm"),
                TipoPonto = (int)p.TipoPonto,
                TipoPontoDescricao = ((TipoPontoEnum)p.TipoPonto).ToString(),
                Observacao = p.Observacao
            }).ToList();

            var horasTrabalhadas = calculaHorasTrabalhadas(pontosPorData);

            var pontoDto = new PontoDto
            {
                Data = $"{dia:D2}/{mes:D2}/{ano}",
                TotalHoras = horasTrabalhadas,
                Batidas = pontosPorData
            };

            return pontoDto;
        }


        private string calculaHorasTrabalhadas(List<PontoDto.PontosPorData> pontos)
        {
            TimeSpan totalHorasTrabalhadas = TimeSpan.Zero;


            var pontosDoDia = pontos.OrderBy(p => p.DataHora).ToList();
            TimeSpan horasTrabalhadasNoDia = TimeSpan.Zero;
            DateTime? inicioPeriodo = null;

            foreach (var ponto in pontosDoDia)
            {
                switch (ponto.TipoPonto)
                {
                    case (int)TipoPontoEnum.Entrada:
                    case (int)TipoPontoEnum.Retorno:
                        inicioPeriodo = ponto.DataHora;
                        break;
                    case (int)TipoPontoEnum.Almoco:
                    case (int)TipoPontoEnum.Saida:
                        if (inicioPeriodo.HasValue)
                        {
                            horasTrabalhadasNoDia += ponto.DataHora - inicioPeriodo.Value;
                            inicioPeriodo = null; // Reseta o início do período para o próximo cálculo
                        }
                        break;
                }
            }

            totalHorasTrabalhadas += horasTrabalhadasNoDia;

            int horas = (int)totalHorasTrabalhadas.TotalHours;
            int minutos = (int)((totalHorasTrabalhadas.TotalHours - horas) * 60);

            // Retorna o resultado formatado
            return $"{horas}:{minutos:D2}";
        }
    }
}
