using ERS.Aprendendo.TestesFuncionais.Core.Contantes;
using ERS.Aprendendo.TestesFuncionais.Core.Dtos;
using FluentValidation;

namespace ERS.Aprendendo.TestesFuncionais.Core.Validador.Dtos
{
    public class CarrinhoArmazenarDtoValidador : AbstractValidator<CarrinhoArmazenarDto>
    {
        public CarrinhoArmazenarDtoValidador()
        {
            RuleFor(dto => dto)
                .NotNull()
                .WithMessage("Dto nulo.");

            RuleFor(dto => dto.Modelo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Modelo está vazio");

            RuleFor(dto => dto.DataLancamento)
                .GreaterThanOrEqualTo(CarrinhoConstantes.DataMinimaParaLancamento)
                .WithMessage("Data de lançamento inferior à data permitida");

            RuleFor(dto => dto.ColecaoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da coleção está vazio");
        }
    }
}