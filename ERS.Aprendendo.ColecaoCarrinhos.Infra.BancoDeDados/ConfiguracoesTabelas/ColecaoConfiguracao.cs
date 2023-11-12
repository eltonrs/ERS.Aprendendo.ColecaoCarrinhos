using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.ConfiguracoesTabelas
{
    public class ColecaoConfiguracao : IEntityTypeConfiguration<Colecao>
    {
        public void Configure(EntityTypeBuilder<Colecao> builder)
        {
            builder.ToTable(nameof(Colecao));

            builder.HasKey(entidade => entidade.Id);

            builder.Property(entidade => entidade.Descricao)
                .IsRequired()
                .HasMaxLength(1500);

            builder.HasMany(entidadePai => entidadePai.Carrinhos)
                .WithOne(entidadeFilho => entidadeFilho.Colecao)
                .HasForeignKey(entidadePai => entidadePai.ColecaoId)
                .IsRequired();
        }
    }
}
