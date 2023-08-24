using ERS.Aprendendo.TestesFuncionais.Core.Validadores.Dtos.Cqrs.Command;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Handlers.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace ERS.Aprendendo.TestesFuncionais.Testes.Core.Cqrs.ComMelhoria
{
    public class CarrinhoInserirCommandHandler_ComMelhorias
    {
        private readonly Guid idValidoParaInserir = Guid.Empty;
        private readonly Guid idInvalidoParaInserir = Guid.NewGuid();

        [Fact]
        public async Task HandleOficialComChamadas_RequestInvalida_ResponderComIdZerado()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            var requestInvalida = new CarrinhoInserirCommand
            {
                Id = idInvalidoParaInserir,
                Modelo = string.Empty,
                DataLancamento = DateTime.Now,
                ColecaoDescricao = string.Empty
            };

            var (handler, assertions) = new HandlerBuilder(cancellationToken)
                .Build();

            var carrinhoId = await handler.Handle(
                requestInvalida,
                cancellationToken
            );

            carrinhoId
                .Should()
                .Be(Guid.Empty);

            assertions
                .NaoDeveTerValidadoRequest(requestInvalida)
                .NaoDeveTerChamadoRepositorioCarrinhoParaAdicionar()
                .NaoDeveTerChamadoRepositorioCarrinhoParaSalvar()
                .NaoDeveTerChamadoRepositorioColecaoParaObterPorDescricao();
        }

        [Fact]
        public async Task HandleOficialComChamadas_RequestValida_ResponderComId()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            var requestValida = new CarrinhoInserirCommand
            {
                Id = idValidoParaInserir,
                Modelo = "Modelo X",
                DataLancamento = DateTime.Now,
                ColecaoDescricao = "Super HT"
            };

            var (handler, assertions) = new HandlerBuilder(cancellationToken)
                .Build();

            var carrinhoId = await handler.Handle(
                requestValida,
                cancellationToken
            );

            carrinhoId
                .Should()
                .NotBe(Guid.Empty);

            assertions
                .DeveTerValidadoRequest(requestValida)
                .DeveTerChamadoRepositorioCarrinhoParaAdicionarComBaseNaRequest(requestValida)
                .DeveTerChamadoRepositorioCarrinhoParaPersistir()
                .DeveTerChamadoRepositorioColecaoParaObterPorDescricao(requestValida.ColecaoDescricao);
        }
    }

    internal class HandlerBuilder
    {
        private readonly Mock<ICarrinhoRepositorio> _mockCarrihnoRepositorio;
        private readonly Mock<IColecaoRepositorio> _mockColecaoRepositorio;
        private readonly CarrinhoInserirCommandValidador _validadorCommand;
        private readonly CancellationToken _cancellationToken;

        public HandlerBuilder(CancellationToken cancellationToken)
        {
            _mockCarrihnoRepositorio = new();
            _mockColecaoRepositorio = new();
            _validadorCommand = new CarrinhoInserirCommandValidador();

            _cancellationToken = cancellationToken;
        }

       public CarrinhoInserirCommadHandler BuildHandler()
            => new(
                _mockCarrihnoRepositorio.Object,
                _mockColecaoRepositorio.Object,
                _validadorCommand
            );

        public (CarrinhoInserirCommadHandler, HandlerAssertions) Build()
            => (
                BuildHandler(),
                new HandlerAssertions(
                    _mockCarrihnoRepositorio,
                    _mockColecaoRepositorio,
                    _validadorCommand,
                    _cancellationToken
                )
            );
    }

    internal class HandlerAssertions
    {
        private readonly Mock<ICarrinhoRepositorio> _mockCarrinhoRepositorio;
        private readonly Mock<IColecaoRepositorio> _mockColecaoRepositorio;
        private readonly CarrinhoInserirCommandValidador _validadorCommand;
        private readonly CancellationToken _cancellationToken;

        public HandlerAssertions(
            Mock<ICarrinhoRepositorio> mockCarrihnoRepositorio,
            Mock<IColecaoRepositorio> mockColecaoRepositorio,
            CarrinhoInserirCommandValidador validadorCommand,
            CancellationToken cancellationToken
        )
        {
            _mockCarrinhoRepositorio = mockCarrihnoRepositorio;
            _mockColecaoRepositorio = mockColecaoRepositorio;
            _validadorCommand = validadorCommand;

            _cancellationToken = cancellationToken;
        }

        #region Deve ter chamado...

        public HandlerAssertions DeveTerValidadoRequest(CarrinhoInserirCommand requestCommand)
        {
            _validadorCommand
                .TestValidateAsync(
                    objectToTest: requestCommand,
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

        #endregion

        #region Não deve ter chamado...

        public HandlerAssertions NaoDeveTerValidadoRequest(CarrinhoInserirCommand requestCommand)
        {
            _validadorCommand
                .TestValidateAsync(
                    objectToTest: requestCommand,
                    cancellationToken: _cancellationToken
                )
                .Result
                .ShouldHaveAnyValidationError();

            return this;
        }

        public HandlerAssertions NaoDeveTerChamadoRepositorioCarrinhoParaAdicionar()
        {
            _mockCarrinhoRepositorio
                .Verify(mock =>
                    mock.AdicionarAsync(
                        It.IsAny<Carrinho>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );

            return this;
        }

        public HandlerAssertions NaoDeveTerChamadoRepositorioCarrinhoParaSalvar()
        {
            _mockCarrinhoRepositorio
                .Verify(mock =>
                    mock.SalvarAsync(It.IsAny<CancellationToken>()),
                    Times.Never
                );

            return this;
        }

        public HandlerAssertions NaoDeveTerChamadoRepositorioColecaoParaObterPorDescricao()
        {
            _mockColecaoRepositorio
                .Verify(mock =>
                    mock.ObterPorDescricaoAsync(
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );

            return this;
        }

        #endregion
    }
}
