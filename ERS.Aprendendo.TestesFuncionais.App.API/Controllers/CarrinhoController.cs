using ERS.Aprendendo.TestesFuncionais.Core.Dtos;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Dtos.Responses;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries;
using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERS.Aprendendo.TestesFuncionais.App.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ILogger<CarrinhoController> _logger;
        private readonly ICarrinhoRepositorio _repositorio;
        private readonly IMediator _mediatr;

        public CarrinhoController(
            ILogger<CarrinhoController> logger,
            ICarrinhoRepositorio repositorio
,
            IMediator mediatr)
        {
            _logger = logger;
            _repositorio = repositorio;
            _mediatr = mediatr;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ObterCarrinhoDetalheQueryResponse>> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            var command = new ObterCarrinhoDetalheQuery
            {
                Id = id
            };

            var response = await _mediatr.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrinho>>> ObterTodosAsync(CancellationToken cancellationToken = default)
        {
            var carrinhos = await _repositorio.ObterTodosAsync(cancellationToken);

            if (carrinhos is null || !carrinhos.Any())
            {
                return NotFound();
            }

            return Ok(carrinhos);
        }

        [HttpPost]
        public async Task<ActionResult<Carrinho>> SalvarAsync(
            CarrinhoArmazenarDto dto,
            CancellationToken cancellationToken = default
        )
        {
            // ToDo : Vai tudo para um serviço
            
            var carrinho = new Carrinho(
                dto.Modelo!,
                dto.DataLancamento
            );

            await _repositorio.AdicionarAsync(carrinho, cancellationToken);
            await _repositorio.SalvarAsync(cancellationToken); // ToDo : por enqto, sem try catch

            return Ok(carrinho);
        }

        // ToDo : implementar um endpoint para testar o cancellationToken
        // Link : https://medium.com/@matias.paulo84/how-to-use-cancellation-tokens-e792bd7aa028
    }
}