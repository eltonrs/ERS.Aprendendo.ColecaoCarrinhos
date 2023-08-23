using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands
{
    public class CarrinhoInserirCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public DateTime DataLancamento { get; set; }
        public string ColecaoDescricao { get; set; } // Se não existir, insere!!!

        public CarrinhoInserirCommand()
        {
            ColecaoDescricao = string.Empty;
        }
    }
}
