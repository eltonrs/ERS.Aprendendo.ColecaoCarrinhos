using ERS.Aprendendo.TestesFuncionais.Core.Dtos;
using ERS.Aprendendo.TestesFuncionais.Core.Interfaces.Servicos;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using FluentValidation;

namespace ERS.Aprendendo.TestesFuncionais.Core.Servicos
{
    public class CarrinhoServico : ICarrinhoServico
    {
        private readonly ICarrinhoRepositorio _carrinhoRepositorio;
        private readonly IValidator<CarrinhoArmazenarDto> _dtoValidador;

        public CarrinhoServico(
            ICarrinhoRepositorio carrinhoRepositorio,
            IValidator<CarrinhoArmazenarDto> dtoValidador
        )
        {
            _carrinhoRepositorio = carrinhoRepositorio;
            _dtoValidador = dtoValidador;
        }

        public async Task<Carrinho> ArmazenarAsync(
            CarrinhoArmazenarDto armazenarDto, 
            CancellationToken cancellationToken
        )
        {
            var dtoValido = _dtoValidador.Validate(armazenarDto);

            if (!dtoValido.IsValid)
            {
                throw new Exception(
                    JuntarMensagensErros(
                        dtoValido.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                );
            }

            var carrinho = new Carrinho(
                armazenarDto.Modelo!,
                armazenarDto.DataLancamento
            );

            await _carrinhoRepositorio.AdicionarAsync(carrinho, cancellationToken);
            await _carrinhoRepositorio.SalvarAsync(cancellationToken);

            return carrinho;
        }

        private static string JuntarMensagensErros(string[] mensagensErro)
        {
            return string.Join(";", mensagensErro);
        }
    }
}
