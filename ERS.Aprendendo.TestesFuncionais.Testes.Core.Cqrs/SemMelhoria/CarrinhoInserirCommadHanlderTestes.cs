using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Handlers.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ERS.Aprendendo.TestesFuncionais.Testes.Core.Cqrs.SemMelhoria
{
    public class CarrinhoInserirCommadHanlderTestes
    {
        [Fact]
        public async Task Handle_RequestInvalida_ResponderComGuidZerado()
        {
            var request = new CarrinhoInserirCommand
            {
                Id = Guid.NewGuid(),
            }
            
            // Mockar manualmente as injeções que devem ser checadas no cenário do teste.
            // Para as injeções que deverão ter seu comportamento verificado, deve ser implementado
            var validadorMock = new Mock<IValidator<CarrinhoInserirCommand>>();
            
            validadorMock
                .Setup(mock =>
                    mock.ValidateAsync(
                        It.IsAny<CarrinhoInserirCommand>(),
                        It.IsAny<CancellationToken>()
                    ))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            // Contruir o objeto do serviço que está sendo testado;

            var handler = new CarrinhoInserirCommadHanlder(
                new Mock<ICarrinhoRepositorio>().Object,
                new Mock<IColecaoRepositorio>().Object,
                validadorMock.Object
            );



            var carrinhoId = await handler.Handle()
            // 
        }
    }
}