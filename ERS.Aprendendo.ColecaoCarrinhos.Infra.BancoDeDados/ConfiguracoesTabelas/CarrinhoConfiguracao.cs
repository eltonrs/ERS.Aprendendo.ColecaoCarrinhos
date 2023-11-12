using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.ConfiguracoesTabelas
{
    public class CarrinhoConfiguracao : IEntityTypeConfiguration<Carrinho>
    {
        public void Configure(EntityTypeBuilder<Carrinho> builder)
        {
            builder.ToTable(nameof(Carrinho));

            builder.HasKey(entidade => entidade.Id);

            builder.Property(entidade => entidade.Modelo)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(entidade => entidade.DataLancamento)
                .IsRequired();

            builder.HasOne(entidadeFilho => entidadeFilho.Colecao)
                .WithMany(entidadePai => entidadePai.Carrinhos)
                .HasForeignKey(entidadeFilho => entidadeFilho.ColecaoId)
                .IsRequired();
        }
    }
}
