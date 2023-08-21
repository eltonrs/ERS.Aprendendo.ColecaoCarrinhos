using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades.Base;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios.Base;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Repositorios.Base
{
    public abstract class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade>
        where TEntidade : EntidadeBase
    {
        protected readonly ColecaoCarrinhosContexto _contexto;

        protected RepositorioBase(ColecaoCarrinhosContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAsync(
            TEntidade entidade,
            CancellationToken cancellationToken
        )
        {
            await _contexto.Set<TEntidade>()
                .AddAsync(entidade, cancellationToken);
        }

        public async Task AtualizarAsync(
            TEntidade entidade,
            CancellationToken cancellationToken
        )
        {
            // ToDo : estudar a abordagem do Update assíncrono

            _contexto.Set<TEntidade>()
                .Update(entidade);

            await SalvarAsync(cancellationToken);
        }

        public async Task ExcluirAsync(
            TEntidade entidade,
            CancellationToken cancellationToken
        )
        {
            // ToDo : estudar a abordagem do Remove assíncrono

            _contexto.Set<TEntidade>()
                .Remove(entidade);

            await SalvarAsync(cancellationToken);
        }

        public async Task<TEntidade?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            return await _contexto.Set<TEntidade>()
                .FindAsync(id, cancellationToken);       
        }

        public async Task<IEnumerable<TEntidade>?> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await _contexto.Set<TEntidade>()
                .ToListAsync(cancellationToken);
        }

        public async Task SalvarAsync(CancellationToken cancellationToken)
        {
            await _contexto.SaveChangesAsync(cancellationToken);
        }
    }
}
