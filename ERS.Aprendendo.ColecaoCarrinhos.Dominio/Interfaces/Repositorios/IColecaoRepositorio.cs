using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios.Base;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios
{
    public interface IColecaoRepositorio : IRepositorioBase<Colecao>
    {
        IEnumerable<Carrinho> ObterCarrinhos(Guid colecaoId);
        Task<Colecao?> ObterPorDescricaoAsync(
            string descricao,
            CancellationToken cancellationToken
        );
    }
}
