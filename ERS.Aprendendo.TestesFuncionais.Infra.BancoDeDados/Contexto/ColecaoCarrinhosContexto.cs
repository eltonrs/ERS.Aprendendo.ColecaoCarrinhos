using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.ConfiguracoesTabelas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto
{
    public class ColecaoCarrinhosContexto : DbContext
    {
        public ColecaoCarrinhosContexto(DbContextOptions<ColecaoCarrinhosContexto> dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // configurações já feitas em: 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarrinhoConfiguracao());
            modelBuilder.ApplyConfiguration(new ColecaoConfiguracao());

            base.OnModelCreating(modelBuilder);
        }

        #region Definições do DbSet
        public virtual DbSet<Carrinho> Carrinhos { get; set; }
        public virtual DbSet<Colecao> Colecoes { get; set; }
        #endregion
    }
}
