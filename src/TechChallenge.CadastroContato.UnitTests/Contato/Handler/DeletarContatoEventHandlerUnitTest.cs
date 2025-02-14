using Moq;
using TechChallenge.CadastroContato.Command.Contato.Deletar;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Handler
{
    public class DeletarContatoEventHandlerUnitTest : UnitTestBase
    {
        [Fact(DisplayName = "Dado que informo argumentos corretos para deletar então retorna a quantidade de linhas afetadas")]
        public async Task DadoArgumentosCorretos_DeletarContato_RetornaQuantidadeLinhasAfetadas()
        {
            Mock<IContatoCommandStore> contatoCommandStore = new();

            contatoCommandStore.Setup(x => x.DeletarContato(It.IsAny<int>()))
                .ReturnsAsync(1);

            var handler = new DeletarContatoEventHandler(contatoCommandStore.Object);

            await handler.Handle(ContatoMoq.DeletarContatoEventMessageValido, CancellationToken.None);

            //Assert.IsType<CommandResult>(resultado);
        }

        [Fact(DisplayName = "Dado que informo argumentos corretos para deletar mas não encontra registro para deletar então retorna exception")]
        public async Task DadoArgumentosCorretos_DeletarContatoNaoEncontraRegistro_RetornaException()
        {
            var eventMessage = ContatoMoq.DeletarContatoEventMessageValido;

            Mock<IContatoCommandStore> contatoCommandStore = new();

            contatoCommandStore.Setup(x => x.DeletarContato(It.IsAny<int>()))
                .ReturnsAsync(0);

            var handler = new DeletarContatoEventHandler(contatoCommandStore.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(eventMessage, CancellationToken.None));

            Assert.NotNull(exception);
            Assert.Equal(string.Format(ErrosNegocio.ErroAoDeletarRegistro, eventMessage.IdContato), exception.Message);
        }
    }
}