using Moq;
using TechChallenge.CadastroContato.Command.Contato.Incluir;
using TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Core.Contato.Model;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Handler
{
    public class IncluirContatoEventHandlerUnitTest : UnitTestBase
    {
        [Fact(DisplayName = "Dado que informo dados válidos para incluir um contato então retorna id do contato cadastrado")]
        public async Task DadoArgumentosCorretos_IncluirContato_RetornoId()
        {
            Mock<IContatoCommandStore> contatoCommandStore = new();
            Mock<ICodigoAreaBrasilCommandStore> codigoAreaBrasilCommandStore = new();

            int idContatoCadastrado = 1;
            bool codigoAreaInformadoExiste = true;

            contatoCommandStore.Setup(x => x.IncluirContato(It.IsAny<ContatoEntity>())).ReturnsAsync(idContatoCadastrado);
            codigoAreaBrasilCommandStore.Setup(x => x.CodigoAreaExiste(It.IsAny<int>())).ReturnsAsync(codigoAreaInformadoExiste);

            var handler = new IncluirContatoEventHandler(contatoCommandStore.Object, codigoAreaBrasilCommandStore.Object);

            await handler.Handle(ContatoMoq.IncluirContatoEventMessageValido, CancellationToken.None);

            //Assert.IsType<CommandResult>(resultado);
            //Assert.True(resultado.Id > 0);
        }

        [Fact(DisplayName = "Dado que informo dados válidos para incluir um contato mas o ddd não existe na base então retorna exceção")]
        public async Task DadoArgumentosCorretosDddNaoExiste_IncluirContato_RetornoExcecao()
        {
            Mock<IContatoCommandStore> contatoCommandStore = new();
            Mock<ICodigoAreaBrasilCommandStore> codigoAreaBrasilCommandStore = new();
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;

            bool codigoAreaInformadoExiste = false;

            codigoAreaBrasilCommandStore.Setup(x => x.CodigoAreaExiste(It.IsAny<int>())).ReturnsAsync(codigoAreaInformadoExiste);

            var handler = new IncluirContatoEventHandler(contatoCommandStore.Object, codigoAreaBrasilCommandStore.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(eventMessage, CancellationToken.None));

            Assert.NotNull(exception);
            Assert.Equal(string.Format(ErrosNegocio.DddNaoEncontrado, eventMessage.Telefone[..2]), exception.Message);
        }
    }
}