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
            
            // Mockar manualmente as inje��es que devem ser checadas no cen�rio do teste.
            // Para as inje��es que dever�o ter seu comportamento verificado, deve ser implementado
            var validadorMock = new Mock<IValidator<CarrinhoInserirCommand>>();
            
            validadorMock
                .Setup(mock =>
                    mock.ValidateAsync(
                        It.IsAny<CarrinhoInserirCommand>(),
                        It.IsAny<CancellationToken>()
                    ))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            // Contruir o objeto do servi�o que est� sendo testado;

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