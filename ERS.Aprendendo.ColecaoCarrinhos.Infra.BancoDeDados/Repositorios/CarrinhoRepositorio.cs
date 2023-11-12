using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Repositorios.Base;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Repositorios
{
    public class CarrinhoRepositorio : RepositorioBase<Carrinho>, ICarrinhoRepositorio
    {
        public CarrinhoRepositorio(ColecaoCarrinhosContexto contexto) : base(contexto)
        { }
    }
}
