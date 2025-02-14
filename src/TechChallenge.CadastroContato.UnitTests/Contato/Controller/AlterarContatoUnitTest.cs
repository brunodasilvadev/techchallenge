using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.CadastroContato.Api.Controllers;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Controller
{
    public class AlterarContatoUnitTest : UnitTestBase
    {
        private readonly Mock<IMediator> _mediatorMock;

        public AlterarContatoUnitTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact(DisplayName = "Dado que informo argumentos corretos para alterar contato então deve retornar o id do registro alterado e o retorno da API deve ser Accepted 202")]
        public async Task DadoArgumentosCorretos_AlterarContato_RetornoStatusAccepted()
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            var commandResult = ContatoMoq.CommandResultValido(commandRequest.CorrelationId);

            _mediatorMock.Setup(x => x.Send(commandRequest, CancellationToken.None)).ReturnsAsync(commandResult);

            var contatoController = new ContatosController(_mediatorMock.Object);
            var resultadoOperacao = await contatoController.AlterarContato(commandRequest.IdContato, commandRequest);
            var objetoRetornado = Assert.IsType<AcceptedResult>(resultadoOperacao);

            Assert.NotNull(objetoRetornado);
            Assert.Equal(objetoRetornado.Value, commandResult);
            Assert.Equal(StatusCodes.Status202Accepted, objetoRetornado.StatusCode);
        }
    }
}