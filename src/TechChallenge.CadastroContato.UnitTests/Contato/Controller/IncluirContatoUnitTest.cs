using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.CadastroContato.Api.Controllers;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Request;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Controller
{
    public class IncluirContatoUnitTest : UnitTestBase
    {
        private readonly Mock<IMediator> _mediatorMock;

        public IncluirContatoUnitTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact(DisplayName = "Dado que informo argumentos corretos para incluir contato então deve retornar o id do registro incluído e o retorno da API deve ser Accepted 202")]
        public async Task DadoArgumentosCorretos_IncluirContato_RetornoStatusAccepted()
        {
            var commandRequest = ContatoMoq.IncluirContatoCommandRequestValido;
            var commandResult = ContatoMoq.CommandResultValido(commandRequest.CorrelationId);

            _mediatorMock.Setup(x => x.Send(commandRequest, CancellationToken.None)).ReturnsAsync(commandResult);

            var contatoController = new ContatosController(_mediatorMock.Object);
            var resultadoOperacao = await contatoController.IncluirContato(commandRequest);
            var objetoRetornado = Assert.IsType<AcceptedResult>(resultadoOperacao);

            Assert.NotNull(objetoRetornado);
            Assert.Equal(objetoRetornado.Value, commandResult);
            Assert.Equal(StatusCodes.Status202Accepted, objetoRetornado.StatusCode);
        }
    }
}