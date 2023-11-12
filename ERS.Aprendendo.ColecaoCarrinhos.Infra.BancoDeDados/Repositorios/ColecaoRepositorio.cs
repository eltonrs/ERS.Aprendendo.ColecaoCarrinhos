using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Repositorios
{
    public class ColecaoRepositorio : RepositorioBase<Colecao>, IColecaoRepositorio
    {
        public ColecaoRepositorio(ColecaoCarrinhosContexto contexto) : base(contexto)
        {
        }

        public IEnumerable<Carrinho> ObterCarrinhos(Guid colecaoId)
        {
            var carrinhos = _contexto.Colecoes
                .Include(colecao => colecao.Carrinhos)
                .Where(colecao => colecao.Id == colecaoId)
                .SelectMany(colecao => colecao.Carrinhos?? Enumerable.Empty<Carrinho>()); // ToDo : Testar esse comportamento

            return carrinhos;
        }

        public async Task<Colecao?> ObterPorDescricaoAsync(
            string descricao,
            CancellationToken cancellationToken
        )
        {
            return await _contexto.Colecoes
                .Where(c => c.Descricao == descricao)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
