using ERS.Aprendendo.TestesFuncionais.Core.Dtos;

namespace ERS.Aprendendo.TestesFuncionais.Core.Interfaces.Servicos
{
    public interface ICarrinhoServico
    {
        Task<Guid?> ArmazenarAsync(CarrinhoArmazenarDto armazenarDto, CancellationToken cancellationToken);
    }
}
