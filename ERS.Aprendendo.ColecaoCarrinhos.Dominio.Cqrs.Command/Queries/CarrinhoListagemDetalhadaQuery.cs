using ERS.Aprendendo.TestesFuncionais.Core.Dtos.Cqrs.Query.Resultado;
using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries
{
    public class CarrinhoListagemDetalhadaQuery : IRequest<CarrinhoListagemDetalhadaQueryResultado[]>
    {
    }
}
