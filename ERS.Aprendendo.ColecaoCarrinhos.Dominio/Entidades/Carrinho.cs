using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades.Base;

namespace ERS.Aprendendo.TestesFuncionais.Dominio.Entidades
{
    public class Carrinho : EntidadeBase
    {
        public string? Modelo { get; set; }
        public DateTime DataLancamento { get; set; }

        public Guid ColecaoId { get; set; }
        public virtual Colecao? Colecao { get; set; }

        public Carrinho(
            Guid id,
            string modelo,
            DateTime dataLancamento,
            Guid colecaoId
        )
        {
            Id = id;
            Modelo = modelo;
            DataLancamento = dataLancamento;
            ColecaoId = colecaoId;
        }
    }
}
