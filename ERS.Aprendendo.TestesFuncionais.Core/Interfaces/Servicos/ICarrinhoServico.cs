using ERS.Aprendendo.TestesFuncionais.Core.Dtos;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;

namespace ERS.Aprendendo.TestesFuncionais.Core.Interfaces.Servicos
{
    public interface ICarrinhoServico
    {
        Task<Carrinho> ArmazenarAsync(CarrinhoArmazenarDto armazenarDto, CancellationToken cancellationToken);
    }
}
