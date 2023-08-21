using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto;
using ERS.Aprendendo.TestesFuncionais.Infra.IoC;

namespace ERS.Aprendendo.TestesFuncionais.App.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // https://stackoverflow.com/questions/52085806/what-is-the-difference-between-iwebhost-webhostbuilder-buildwebhost

            var builder = WebApplication.CreateBuilder(args);

            ConfigurarServicos(
                builder.Services,
                builder.Configuration
            );

            var app = builder.Build();

            ConfigurarAplicacao(app);

            app.Run();
        }

        private static void ConfigurarServicos(
            IServiceCollection servicos,
            IConfiguration configuracao
        )
        {
            // Adiciona os serviços necessários no container (host) da aplicação

            servicos.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            servicos.AddEndpointsApiExplorer();
            servicos.AddSwaggerGen();
            servicos.AddControllers();

            servicos.ConfigurarRepositorios();
            //services.ConfigurarServices(); // ToDo : implementar configurações do IoC para serviços do Core
            servicos.AdicionarColecaoCarrinhosContexto(configuracao);

            servicos.ConfigurarCqrs();
        }

        private static void ConfigurarAplicacao(WebApplication app)
        {
            // Configura o pipeline das requisições HTTP

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}