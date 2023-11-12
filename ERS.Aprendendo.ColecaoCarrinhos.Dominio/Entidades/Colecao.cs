using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades.Base;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Entidades
{
    public class Colecao : EntidadeBase
    {
        public string? Descricao { get; set; }
        public ICollection<Carrinho>? Carrinhos { get; set; }

        public Colecao(
            Guid id,
            string descricao
        )
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
