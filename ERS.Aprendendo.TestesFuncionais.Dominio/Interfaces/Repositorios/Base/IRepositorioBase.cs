using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades.Base;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios.Base
{
    public interface IRepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        Task AdicionarAsync(TEntidade entidade, CancellationToken cancellationToken);
        Task AtualizarAsync(TEntidade entidade, CancellationToken cancellationToken);
        Task ExcluirAsync(TEntidade entidade, CancellationToken cancellationToken);
        Task<TEntidade?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntidade>?> ObterTodosAsync(CancellationToken cancellationToken);
        Task SalvarAsync(CancellationToken cancellationToken);
    }
}
