using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto
{
    public static class ColecaoCarrinhosContextoExtensions
    {
        const string ColecaoCarrinhosContextoEmMemoriaPadrao = "ColecaoDB";

        public static void AdicionarColecaoCarrinhosContexto(
            this IServiceCollection services,
            IConfiguration configuracao
        )
        {
            var nomeBancoEmMemoria = configuracao.GetConnectionString("ColecaoCarrinhosContextoEmMemoria");

            services.AddDbContext<ColecaoCarrinhosContexto>(builder =>
            {
                builder.UseInMemoryDatabase(nomeBancoEmMemoria ?? ColecaoCarrinhosContextoEmMemoriaPadrao);
            });
        }
    }
}
