namespace ERS.Aprendendo.TestesFuncionais.Core.Dtos
{
    public class CarrinhoArmazenarDto
    {
        public string? Modelo { get; init; }
        public DateTime DataLancamento { get; init; }
        public Guid ColecaoId { get; init; }

        public CarrinhoArmazenarDto(
            string modelo,
            DateTime dataLancamento,
            Guid colecaoId
        )
        {
            Modelo = modelo;
            DataLancamento = dataLancamento;
            ColecaoId = colecaoId;
        }
    }
}
