using ERS.Aprendendo.TestesFuncionais.Core.Dtos.Cqrs.Query.Resultado;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Handlers.Queries
{
    public class CarrinhosListagemDetalhadaQueryHandler
        : IRequestHandler<CarrinhoListagemDetalhadaQuery, CarrinhoListagemDetalhadaQueryResultado[]>
    {
        private readonly ICarrinhoRepositorio _carrinhoRepositorio;

        public CarrinhosListagemDetalhadaQueryHandler(ICarrinhoRepositorio carrinhoRepositorio)
        {
            _carrinhoRepositorio = carrinhoRepositorio;
        }

        public async Task<CarrinhoListagemDetalhadaQueryResultado[]> Handle(
            CarrinhoListagemDetalhadaQuery request,
            CancellationToken cancellationToken
        )
        {
            var carrinhos = await _carrinhoRepositorio.ObterTodosAsync(cancellationToken);

            if (carrinhos is null)
                return Array.Empty<CarrinhoListagemDetalhadaQueryResultado>();

            return carrinhos
                .Select(c =>
                    new CarrinhoListagemDetalhadaQueryResultado
                    {
                        Id = c.Id,
                        Modelo = c.Modelo,
                        DataLancamento = c.DataLancamento,
                        ColecaoDescricao = "Pendente implementação"
                    })
                .ToArray();
            
        }
    }
}
