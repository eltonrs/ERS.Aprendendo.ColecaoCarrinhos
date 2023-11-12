using ERS.Aprendendo.TestesFuncionais.Core.Dtos.Cqrs.Query.Resultado;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Handlers.Queries
{
    public class ObterCarrinhoDetalheQueryHandler
        : IRequestHandler<CarrinhoDetalhadoQuery, CarrinhoDetalhadoQueryResultado>
    {
        private readonly ICarrinhoRepositorio _carrinhoRepositorio;

        public ObterCarrinhoDetalheQueryHandler(ICarrinhoRepositorio carrinhoRepositorio)
        {
            _carrinhoRepositorio = carrinhoRepositorio;
        }

        public async Task<CarrinhoDetalhadoQueryResultado> Handle(
            CarrinhoDetalhadoQuery request,
            CancellationToken cancellationToken
        )
        {
            var carrinhoId = request.Id;

            var carrinho = await _carrinhoRepositorio.ObterPorIdAsync(
                carrinhoId,
                cancellationToken
            );

            if (carrinho is null)
                return new CarrinhoDetalhadoQueryResultado();

            var response = new CarrinhoDetalhadoQueryResultado
            {
                Id = carrinhoId,
                Modelo = carrinho.Modelo,
                DataLancamento = carrinho.DataLancamento
            };

            return response;
        }
    }
}
