using ERS.Aprendendo.TestesFuncionais.Core.Dtos.Cqrs.Query.Resultado;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Commands;
using ERS.Aprendendo.TestesFuncionais.Dominio.Cqrs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERS.Aprendendo.TestesFuncionais.App.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarrinhoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CarrinhoDetalhadoQueryResultado>> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            var queryRequest = new CarrinhoDetalhadoQuery
            {
                Id = id
            };

            var queryResultado = await _mediator.Send(queryRequest, cancellationToken);

            return Ok(queryResultado);
        }

        [HttpGet]
        public async Task<ActionResult<CarrinhoListagemDetalhadaQueryResultado[]>> ObterTodosAsync(
            CancellationToken cancellationToken = default
        )
        {
            var queryRequest = new CarrinhoListagemDetalhadaQuery();
            
            var queryResultado = await _mediator.Send(
                queryRequest,
                cancellationToken
            );

            if (queryResultado is null)
                return BadRequest();

            return Ok(queryResultado);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> SalvarAsync(
            CarrinhoInserirCommand requestCommand,
            CancellationToken cancellationToken = default
        )
        {
            var carrinhoId = await _mediator.Send(
                requestCommand,
                cancellationToken
            );
            
            if (carrinhoId == Guid.Empty)
                return BadRequest();

            return Ok(carrinhoId);
        }

        // ToDo : implementar um endpoint para testar o cancellationToken
        // Link : https://medium.com/@matias.paulo84/how-to-use-cancellation-tokens-e792bd7aa028
    }
}