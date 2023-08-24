using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Handlers.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;

namespace ERS.Aprendendo.TestesFuncionais.Testes.Core.Cqrs.ComMelhoria
{
    public class CarrinhoInserirCommandHandler_ComMelhorias
    {
        [Fact]
        public async Task HandleOficialComChamadas_RequestInvalida_ResponderComIdZerado()
        {
            // Escrever com base no de baixo e com base no equivalente da SemMelhorias
        }

        [Fact]
        public async Task HandleOficialComChamadas_RequestValida_ResponderComId()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            var requestValido = new CarrinhoInserirCommand
            {
                Id = Guid.Empty,
                Modelo = "Modelo Y",
                DataLancamento = DateTime.Now,
                ColecaoDescricao = "HyperCars"
            };

            var (handler, assertions) = new HandlerBuilder(cancellationToken)
                .Build();

            var carrinhoId = await handler.Handle(
                requestValido,
                cancellationToken
            );

            carrinhoId
                .Should()
                .NotBe(Guid.Empty);

            assertions
                .DeveTerValidadoRequest(requestValido)
                .DeveTerChamadoRepositorioCarrinhoParaAdicionarComBaseNaRequest(requestValido)
                .DeveTerChamadoRepositorioCarrinhoParaPersistir()
                .DeveTerChamadoRepositorioColecaoParaObterPorDescricao(requestValido.ColecaoDescricao);
        }
    }

    internal class HandlerBuilder
    {
        private readonly Mock<ICarrinhoRepositorio> _mockCarrihnoRepositorio;
        private readonly Mock<IColecaoRepositorio> _mockColecaoRepositorio;
        private readonly InlineValidator<CarrinhoInserirCommand> _mockValidadorCommand;
        private readonly CancellationToken _cancellationToken;

        public HandlerBuilder(CancellationToken cancellationToken)
        {
            _mockCarrihnoRepositorio = new();
            _mockColecaoRepositorio = new();
            _mockValidadorCommand = new();
            _mockValidadorCommand = new InlineValidator<CarrinhoInserirCommand>();

            _cancellationToken = cancellationToken;
        }

       public CarrinhoInserirCommadHandler BuildHandler()
            => new(
                _mockCarrihnoRepositorio.Object,
                _mockColecaoRepositorio.Object,
                _mockValidadorCommand
            );

        public (CarrinhoInserirCommadHandler, HandlerAssertions) Build()
            => (
                BuildHandler(),
                new HandlerAssertions(
                    _mockCarrihnoRepositorio,
                    _mockColecaoRepositorio,
                    _mockValidadorCommand,
                    _cancellationToken
                )
            );
    }

    internal class HandlerAssertions
    {
        private readonly Mock<ICarrinhoRepositorio> _mockCarrinhoRepositorio;
        private readonly Mock<IColecaoRepositorio> _mockColecaoRepositorio;
        private readonly InlineValidator<CarrinhoInserirCommand> _mockValidadorCommand;
        private readonly CancellationToken _cancellationToken;

        public HandlerAssertions(
            Mock<ICarrinhoRepositorio> mockCarrihnoRepositorio,
            Mock<IColecaoRepositorio> mockColecaoRepositorio,
            InlineValidator<CarrinhoInserirCommand> mockValidadorCommand,
            CancellationToken cancellationToken
        )
        {
            _mockCarrinhoRepositorio = mockCarrihnoRepositorio;
            _mockColecaoRepositorio = mockColecaoRepositorio;
            _mockValidadorCommand = mockValidadorCommand;

            _cancellationToken = cancellationToken;
        }

        public HandlerAssertions DeveTerValidadoRequest(CarrinhoInserirCommand requestCommand)
        {
            _mockValidadorCommand
                .TestValidateAsync(
                    requestCommand,
                    cancellationToken: _cancellationToken
                )
                .Result
                .ShouldNotHaveAnyValidationErrors();
            
            return this;
        }

        public HandlerAssertions DeveTerChamadoRepositorioCarrinhoParaAdicionarComBaseNaRequest(
            CarrinhoInserirCommand requestCommand
        )
        {
            _mockCarrinhoRepositorio
                .Verify(mock =>
                    mock.AdicionarAsync(    
                        It.Is<Carrinho>(carrinhoParametro =>
                            carrinhoParametro.Id != Guid.Empty
                            && carrinhoParametro.Modelo == requestCommand.Modelo
                            && carrinhoParametro.DataLancamento == requestCommand.DataLancamento
                            && carrinhoParametro.ColecaoId != Guid.Empty
                        ),
                        It.Is<CancellationToken>(tokenParametro => tokenParametro == _cancellationToken)
                    ),
                    Times.Once
                );

            return this;
        }

        public HandlerAssertions DeveTerChamadoRepositorioCarrinhoParaPersistir()
        {
            _mockCarrinhoRepositorio
                .Verify(mock =>
                    mock.SalvarAsync(
                        It.Is<CancellationToken>(tokenParametro => tokenParametro == _cancellationToken)
                    ),
                    Times.Once
                );

            return this;
        }

        public HandlerAssertions DeveTerChamadoRepositorioColecaoParaObterPorDescricao(string descricao)
        {
            _mockColecaoRepositorio
                .Verify(mock =>
                    mock.ObterPorDescricaoAsync(
                        It.Is<string>(descricaoParametro => descricaoParametro == descricao),
                        It.Is<CancellationToken>(tokenParametro => tokenParametro == _cancellationToken)
                    ),
                    Times.Once
                );

            return this;
        }
    }
}
