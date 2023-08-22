namespace ERS.Aprendendo.TestesFuncionais.Core.Dtos
{
    public class CarrinhoListagemDto
    {
        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public DateTime DataLancamento { get; set; }
        public string? ColecaoDescricao { get; set;}
    }
}
