using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.CadastroContato.Api.Controllers;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Controller
{
    public class PesquisarContatosUnitTest : UnitTestBase
    {
        private readonly Mock<IMediator> _mediatorMock;

        public PesquisarContatosUnitTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact(DisplayName = "Dado que informo argumentos corretos para pesquisar contatos então deve retornar registros e o retorno da API deve ser Ok 200")]
        public async Task DadoArgumentosCorretos_PesquisarContatos_RetornoStatusOk()
        {
            var queryResult = ContatoMoq.PesquisarContatosQueryResultValido;
            var queryRequest = ContatoMoq.PesquisarContatosQueryRequestComDdd;

            _mediatorMock.Setup(x => x.Send(queryRequest, CancellationToken.None)).ReturnsAsync(queryResult);

            var contatoController = new ContatosController(_mediatorMock.Object);
            var contatos = await contatoController.PesquisarContatos(queryRequest);
            var objectResult = Assert.IsType<OkObjectResult>(contatos);

            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.Value, queryResult);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Dado que informo argumentos corretos para pesquisar contatos e não encontra dados então não deve retornar registros e o retorno da API deve ser OK 200")]
        public async Task DadoArgumentosCorretos_PesquisarContatos_RetornoStatusNotFound()
        {
            var queryResultVazio = ContatoMoq.PesquisarContatosQueryResultVazio;
            var queryRequest = ContatoMoq.PesquisarContatosQueryRequestComDdd;

            _mediatorMock.Setup(x => x.Send(queryRequest, CancellationToken.None)).ReturnsAsync(queryResultVazio);

            var contatoController = new ContatosController(_mediatorMock.Object);
            var contatos = await contatoController.PesquisarContatos(queryRequest);
            var objectResult = Assert.IsType<OkObjectResult>(contatos);

            Assert.NotNull(objectResult);

            var erroRetornado = Assert.IsAssignableFrom<IEnumerable<PesquisarContatosQueryResult>>(objectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
    }
}