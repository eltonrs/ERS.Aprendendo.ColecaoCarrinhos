namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Dtos.Responses
{
    public class ObterCarrinhoDetalheQueryResponse
    {
        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
