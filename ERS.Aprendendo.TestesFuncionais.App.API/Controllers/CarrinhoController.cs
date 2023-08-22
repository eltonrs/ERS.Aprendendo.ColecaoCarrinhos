using ERS.Aprendendo.TestesFuncionais.Core.Dtos;
using ERS.Aprendendo.TestesFuncionais.Core.Interfaces.Servicos;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Command.Dtos.Responses;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries;
using ERS.Aprendendo.TestesFuncionais.Dominio.Interfaces.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERS.Aprendendo.TestesFuncionais.App.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepositorio _repositorio;
        private readonly IMediator _mediatr;

        public CarrinhoController(
            ICarrinhoRepositorio repositorio
,
            IMediator mediatr)
        {
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
        public async Task<ActionResult<CarrinhoListagemDto[]>> ObterTodosAsync(
            [FromServices] ICarrinhoServico carrinhoServico,
            CancellationToken cancellationToken = default
        )
        {
            var carrinhos = await carrinhoServico.ObterTodosAsync(cancellationToken);

            if (carrinhos is null || !carrinhos.Any())
            {
                return NotFound();
            }

            return Ok(carrinhos);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> SalvarAsync(
            CarrinhoArmazenarDto dto,
            [FromServices] ICarrinhoServico carrinhoServico,
            CancellationToken cancellationToken = default
        )
        {
            var carrinhoId = await carrinhoServico.ArmazenarAsync(dto, cancellationToken);

            if (carrinhoId is null)
                return BadRequest();

            return Ok(carrinhoId);
        }

        // ToDo : implementar um endpoint para testar o cancellationToken
        // Link : https://medium.com/@matias.paulo84/how-to-use-cancellation-tokens-e792bd7aa028
    }
}