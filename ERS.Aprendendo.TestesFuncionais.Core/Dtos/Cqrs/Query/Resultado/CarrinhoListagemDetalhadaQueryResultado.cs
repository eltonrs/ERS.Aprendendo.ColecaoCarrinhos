namespace ERS.Aprendendo.TestesFuncionais.Core.Dtos.Cqrs.Query.Resultado
{
    public class CarrinhoListagemDetalhadaQueryResultado
    {
        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public DateTime DataLancamento { get; set; }
        public string? ColecaoDescricao { get; set; }
    }
}
