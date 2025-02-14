using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.CadastroContato.Api.Controllers;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Controller
{
    public class DeletarContatoUnitTest : UnitTestBase
    {
        private readonly Mock<IMediator> _mediatorMock;

        public DeletarContatoUnitTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact(DisplayName = "Dado que informo argumentos corretos para deletar um contato então o retorno da API deve ser Accepted 202")]
        public async Task DadoArgumentosCorretos_DeletarContato_RetornoStatusAccepted()
        {
            var commandRequest = ContatoMoq.DeletarContatoCommandRequestValido;
            var commandResult = ContatoMoq.CommandResultValido(commandRequest.CorrelationId);

            _mediatorMock.Setup(x => x.Send(commandRequest, CancellationToken.None)).ReturnsAsync(commandResult);

            var contatoController = new ContatosController(_mediatorMock.Object);
            var retorno = await contatoController.DeletarContato(1);
            var objectResult = Assert.IsType<AcceptedResult>(retorno);

            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status202Accepted, objectResult.StatusCode);
        }
    }
}