using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using FluentValidation;
using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Handlers.Commands
{
    public class CarrinhoInserirCommadHandler
        : IRequestHandler<CarrinhoInserirCommand, Guid>
    {
        private readonly ICarrinhoRepositorio _carrinhoRepositorio;
        private readonly IColecaoRepositorio _colecaoRepositorio;
        private readonly IValidator<CarrinhoInserirCommand> _commandValidador;

        public CarrinhoInserirCommadHandler(
            ICarrinhoRepositorio carrinhoRepositorio,
            IColecaoRepositorio colecaoRepositorio,
            IValidator<CarrinhoInserirCommand> commandValidador
        )
        {
            _carrinhoRepositorio = carrinhoRepositorio;
            _colecaoRepositorio = colecaoRepositorio;
            _commandValidador = commandValidador;
        }

        public async Task<Guid> Handle(
            CarrinhoInserirCommand request,
            CancellationToken cancellationToken
        )
        {
            var resultadoValidacao = await _commandValidador.ValidateAsync(
                request,
                cancellationToken
            );

            if (!resultadoValidacao.IsValid)
                return Guid.Empty;

            var colecao = await ObterColecaoAsync(
                request.ColecaoDescricao,
                cancellationToken
            );

            var carrinho = new Carrinho(
                Guid.NewGuid(),
                request.Modelo,
                request.DataLancamento,
                colecao.Id
            );

            await _carrinhoRepositorio.AdicionarAsync(
                carrinho,
                cancellationToken
            );

            await _carrinhoRepositorio.SalvarAsync(cancellationToken);

            return carrinho.Id;
        }

        public async Task<Guid> HandleSimples(
            CarrinhoInserirCommand request,
            CancellationToken cancellationToken
        )
        {
            var resultadoValidacao = await _commandValidador.ValidateAsync(
                request,
                cancellationToken
            );

            if (!resultadoValidacao.IsValid)
                return Guid.Empty;

            var colecao = new Colecao(
                Guid.NewGuid(),
                request.ColecaoDescricao
            );

            var carrinho = new Carrinho(
                Guid.NewGuid(),
                request.Modelo,
                request.DataLancamento,
                colecao.Id
            );

            return carrinho.Id;
        }

        private async Task<Colecao> ObterColecaoAsync(
            string descricao,
            CancellationToken cancellationToken
        )
        {
            var colecao = await _colecaoRepositorio.ObterPorDescricaoAsync(
                descricao,
                cancellationToken
            );

            if (colecao is not null)
                return colecao;
            
            return new Colecao(
                Guid.NewGuid(),
                descricao
            );
        }
    }
}
