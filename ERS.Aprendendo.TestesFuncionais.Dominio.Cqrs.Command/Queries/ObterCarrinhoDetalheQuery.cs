using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Dtos.Responses;
using MediatR;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries
{
    public class ObterCarrinhoDetalheQuery : IRequest<ObterCarrinhoDetalheQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
