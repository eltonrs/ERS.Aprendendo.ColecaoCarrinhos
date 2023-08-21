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
                .SelectMany(colecao => colecao.Carrinhos) // ToDo : Testar esse comportamento
                .ToList();

            return carrinhos;
        }
    }
}
