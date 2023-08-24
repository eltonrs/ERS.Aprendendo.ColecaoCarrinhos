using ERS.Aprendendo.TestesFuncionais.Core.Validadores.Dtos.Cqrs.Command;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Handlers.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using FluentAssertions;
using Moq;

namespace ERS.Aprendendo.TestesFuncionais.Testes.Core.Cqrs.SemMelhoria
{
    public class CarrinhoInserirCommadHandlerTestes
    {
        private readonly Guid idValidoParaInserir = Guid.Empty;
        private readonly Guid idInvalidoParaInserir = Guid.NewGuid();

        #region Testes do handle oficial, sem testar chamadas das injeções.

        [Fact]
        public async Task HandleOficialSemChamadas_RequestInvalida_ResponderCarrinhoIdZerado()
        {
            var requestInvalida = new CarrinhoInserirCommand
            {
                Id = idInvalidoParaInserir,
                Modelo = "Modelo J",
                DataLancamento = DateTime.Now,
                ColecaoDescricao = "V8"
            };

            // Mockar manualmente as injeções que devem ser checadas no cenário do teste.
            // Para as injeções que deverão ter seu comportamento verificado, deve ser implementado

            // Sobre mockar o validador (FluentValidation):
            // https://docs.fluentvalidation.net/en/latest/testing.html#mocking
            var validador = new CarrinhoInserirCommandValidador();

            // Contruir o objeto do serviço que está sendo testado;

            var handler = new CarrinhoInserirCommadHandler(
                new Mock<ICarrinhoRepositorio>().Object,
                new Mock<IColecaoRepositorio>().Object,
                validador
            );

            var carrinhoId = await handler.Handle(
                requestInvalida,
                CancellationToken.None
            );
            
            carrinhoId
                .Should()
                .Be(Guid.Empty);
        }

        [Fact]
        public async Task HandleOficialSemChamadas_RequestValida_ResponderComCarrinhoId()
        {
            var requestValida = new CarrinhoInserirCommand
            {
                Id = idValidoParaInserir,
                Modelo = "Modelo X",
                DataLancamento = DateTime.Now,
                ColecaoDescricao = "Super HT"
            };

            var validador = new CarrinhoInserirCommandValidador();

            var handler = new CarrinhoInserirCommadHandler(
                new Mock<ICarrinhoRepositorio>().Object,
                new Mock<IColecaoRepositorio>().Object,
                validador
            );

            var carrinhoId = await handler.Handle(
                requestValida,
                CancellationToken.None
            );

            carrinhoId
                .Should()
                .NotBe(Guid.Empty);
        }

        #endregion

        #region Testes do handle "simples" (NÃO chama os repositórios), sem testar as chamadas das injeções.

        [Fact]
        public async Task HandleSimplesSemChamadas_RequestInvalida_ResponderCarrinhoIdZerado()
        {
            var requestInvalida = new CarrinhoInserirCommand
            {
                Id = idInvalidoParaInserir,
                Modelo = "Modelo J",
                DataLancamento = DateTime.Now,
                ColecaoDescricao = "V8"
            };

            var validador = new CarrinhoInserirCommandValidador();

            var handler = new CarrinhoInserirCommadHandler(
                new Mock<ICarrinhoRepositorio>().Object,
                new Mock<IColecaoRepositorio>().Object,
                validador
            );

            var carrinhoId = await handler.HandleSimples(
                requestInvalida,
                CancellationToken.None
            );

            carrinhoId
                .Should()
                .Be(Guid.Empty);
        }

        [Fact]
        public async Task HandleSimplesSemChamadas_RequestValida_ResponderComCarrinhoId()
        {
            var requestValida = new CarrinhoInserirCommand
            {
                Id = idValidoParaInserir,
                Modelo = "Modelo X",
                DataLancamento = DateTime.Now,
                ColecaoDescricao = "Super HT"
            };

            var validador = new CarrinhoInserirCommandValidador();

            var handler = new CarrinhoInserirCommadHandler(
                new Mock<ICarrinhoRepositorio>().Object,
                new Mock<IColecaoRepositorio>().Object,
                validador
            );

            var carrinhoId = await handler.HandleSimples(
                requestValida,
                CancellationToken.None
            );

            carrinhoId
                .Should()
                .NotBe(Guid.Empty);
        }

        #endregion

        #region Testes do handle oficial, mas agora testando as chamadas das injeções.

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

            // Configuração dos mocks. Nesse caso, onde não serão chamadas, só instanciar.

            var mockCarrihnoRepositorio = new Mock<ICarrinhoRepositorio>();
            var mockColecaoRepositorio = new Mock<IColecaoRepositorio>();
            var validador = new CarrinhoInserirCommandValidador();

            var handler = new CarrinhoInserirCommadHandler(
                mockCarrihnoRepositorio.Object,
                mockColecaoRepositorio.Object,
                validador
            );

            var carrinhoId = await handler.Handle(
                requestInvalida,
                cancellationToken
            );

            carrinhoId
                .Should()
                .Be(Guid.Empty);

            // Garantir que NÃO tenham sido feitas requisições ao banco desnecessariamente.

            mockCarrihnoRepositorio
                .Verify(mock =>
                    mock.AdicionarAsync(
                        It.IsAny<Carrinho>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );

            mockCarrihnoRepositorio
                .Verify(mock =>
                    mock.SalvarAsync(It.IsAny<CancellationToken>()),
                    Times.Never
                );

            mockColecaoRepositorio
                .Verify(mock =>
                    mock.ObterPorDescricaoAsync(
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );
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

            var mockCarrihnoRepositorio = new Mock<ICarrinhoRepositorio>();
            var mockColecaoRepositorio = new Mock<IColecaoRepositorio>();
            var validador = new CarrinhoInserirCommandValidador();

            var handler = new CarrinhoInserirCommadHandler(
                mockCarrihnoRepositorio.Object,
                mockColecaoRepositorio.Object,
                validador
            );

            var carrinhoId = await handler.Handle(
                requestValida,
                cancellationToken
            );

            carrinhoId
                .Should()
                .NotBe(Guid.Empty);

            // Garantir que TENHAM sido feitas requisições ao banco

            mockCarrihnoRepositorio
                .Verify(mock =>
                    mock.AdicionarAsync(
                        It.Is<Carrinho>(carrinhoParametro =>
                            carrinhoParametro.Id != Guid.Empty
                            && carrinhoParametro.Modelo == requestValida.Modelo
                            && carrinhoParametro.DataLancamento == requestValida.DataLancamento
                            && carrinhoParametro.ColecaoId != Guid.Empty
                        ),
                        It.Is<CancellationToken>(tokenParametro => tokenParametro == cancellationToken)
                    ),
                    Times.Once
                );

            mockCarrihnoRepositorio
                .Verify(mock =>
                    mock.SalvarAsync(
                        It.Is<CancellationToken>(tokenParametro => tokenParametro == cancellationToken)
                    ),
                    Times.Once
                );

            mockColecaoRepositorio
                .Verify(mock =>
                    mock.ObterPorDescricaoAsync(
                        It.Is<string>(descricaoParametro => descricaoParametro == requestValida.ColecaoDescricao),
                        It.Is<CancellationToken>(tokenParametro => tokenParametro == cancellationToken)
                    ),
                    Times.Once
                );
        }

        #endregion
    }
}