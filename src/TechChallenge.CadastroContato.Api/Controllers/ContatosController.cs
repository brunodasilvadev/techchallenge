using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Request;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Request;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Request;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.Contract.Exceptions;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IMediator _requestHandler;

        /// <summary>
        /// Controller Contatos Pipe
        /// </summary>
        /// <param name="requestHandler"></param>
        public ContatosController(IMediator requestHandler)
        {
            _requestHandler = requestHandler;
        }

        /// <summary>
        /// Busca os dados de contato.
        /// </summary>
        /// <param name="queryRequest">Filtros para realizar consulta dos nossos contatos</param>
        /// <returns>Dados de contato</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PesquisarContatosQueryResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PesquisarContatos([FromQuery] PesquisarContatosQueryRequest queryRequest)
        {
            var result = await _requestHandler.Send(queryRequest);

            return Ok(result);
        }

        /// <summary>
        /// Incluir um novo contato.
        /// </summary>
        /// <param name="commandRequest">Dados de contato</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> IncluirContato([FromBody] IncluirContatoCommandRequest commandRequest)
        {
            var result = await _requestHandler.Send(commandRequest);

            return Accepted(result);
        }

        /// <summary>
        /// Alterar um contato previamente cadastrado.
        /// </summary>
        /// <param name="commandRequest">Dados de contato</param>
        /// <returns></returns>
        [HttpPut("{idContato}")]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AlterarContato([FromRoute] int idContato, [FromBody] AlterarContatoCommandRequest commandRequest)
        {
            commandRequest.IdContato = idContato;

            var result = await _requestHandler.Send(commandRequest);

            return Accepted(result);
        }

        /// <summary>
        /// Deletar um contato previamente cadastrado.
        /// </summary>
        /// <param name="idContato">Id do contato</param>
        /// <returns></returns>
        [HttpDelete("{idContato}")]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeletarContato([FromRoute] int idContato)
        {
            var result = await _requestHandler.Send(new DeletarContatoCommandRequest { IdContato = idContato });

            return Accepted(result);
        }
    }
}