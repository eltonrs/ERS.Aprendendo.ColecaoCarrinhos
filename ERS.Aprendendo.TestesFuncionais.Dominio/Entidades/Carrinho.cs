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
            string modelo,
            DateTime dataLancamento
        )
        {
            Modelo = modelo;
            DataLancamento = dataLancamento;
        }
    }
}
