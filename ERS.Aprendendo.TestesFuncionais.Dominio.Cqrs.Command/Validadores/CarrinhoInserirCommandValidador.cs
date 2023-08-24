using ERS.Aprendendo.TestesFuncionais.Core.Contantes;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using FluentValidation;

namespace ERS.Aprendendo.TestesFuncionais.Core.Validadores.Dtos.Cqrs.Command
{
    public class CarrinhoInserirCommandValidador : AbstractValidator<CarrinhoInserirCommand>
    {
        public CarrinhoInserirCommandValidador()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Não pode enviar id válido para inserir. O Id é gerado automaticamente!");
            
            RuleFor(c => c)
                .NotNull()
                .WithMessage("Command nulo!");

            RuleFor(c => c.Modelo)
                .NotNull()
                .NotEmpty()
                .WithMessage("O modelo do carrinho está vazio!");

            RuleFor(c => c.DataLancamento)
                .GreaterThanOrEqualTo(CarrinhoConstantes.DataMinimaParaLancamento)
                .WithMessage("Data de lançamento inferior à data permitida!");

            RuleFor(c => c.ColecaoDescricao)
                .NotEqual(string.Empty)
                .NotNull()
                .WithMessage("A coleção está vazia!");
        }
    }
}