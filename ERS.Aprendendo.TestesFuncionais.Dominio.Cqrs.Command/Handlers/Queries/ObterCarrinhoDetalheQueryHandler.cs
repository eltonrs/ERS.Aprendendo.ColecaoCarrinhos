using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Dtos.Responses;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Handlers.Queries
{
    public class ObterCarrinhoDetalheQueryHandler : IRequestHandler<ObterCarrinhoDetalheQuery, ObterCarrinhoDetalheQueryResponse>
    {
        private readonly ICarrinhoRepositorio _carrinhoRepositorio;

        public ObterCarrinhoDetalheQueryHandler(ICarrinhoRepositorio carrinhoRepositorio)
        {
            _carrinhoRepositorio = carrinhoRepositorio;
        }

        public async Task<ObterCarrinhoDetalheQueryResponse> Handle(ObterCarrinhoDetalheQuery request, CancellationToken cancellationToken)
        {
            var carrinhoId = request.Id;

            var carrinho = await _carrinhoRepositorio.ObterPorIdAsync(carrinhoId, cancellationToken);

            if (carrinho is null)
            {
                return new ObterCarrinhoDetalheQueryResponse();
            }

            var response = new ObterCarrinhoDetalheQueryResponse
            {
                Id = carrinhoId,
                Modelo = carrinho.Modelo,
                DataLancamento = carrinho.DataLancamento
            };

            return response;
        }
    }
}
