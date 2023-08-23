﻿namespace ERS.Aprendendo.TestesFuncionais.Core.Dtos.Cqrs.Query.Resultado
{
    public class CarrinhoDetalhadoQueryResultado
    {
        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public DateTime DataLancamento { get; set; }
        public string? ColecaoDescricao { get; set; }
    }
}
