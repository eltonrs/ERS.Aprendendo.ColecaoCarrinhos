using ERS.Aprendendo.TestesFuncionais.Core.Dtos;
using ERS.Aprendendo.TestesFuncionais.Core.Interfaces.Servicos;
using ERS.Aprendendo.TestesFuncionais.Core.Servicos;
using ERS.Aprendendo.TestesFuncionais.Core.Validador.Dtos;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Handlers.Queries;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Repositorios;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ERS.Aprendendo.TestesFuncionais.Infra.IoC
{
    public static class IoCConfiguracao
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<ICarrinhoRepositorio, CarrinhoRepositorio>();
            services.AddScoped<IColecaoRepositorio, ColecaoRepositorio>();
        }

        public static void ConfigurarServicos(this IServiceCollection services)
        {
            services.AddScoped<ICarrinhoServico, CarrinhoServico>();
        }

        public static void ConfigurarValidadores(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CarrinhoArmazenarDto>, CarrinhoArmazenarDtoValidador>();
        }

        public static void ConfigurarCqrs(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblyContaining(typeof(ObterCarrinhoDetalheQueryHandler));
            });
        }
    }
}