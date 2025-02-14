using Moq;
using TechChallenge.CadastroContato.Command.Contato.Alterar;
using TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Core.Contato.Model;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Handler
{
    public class AlterarContatoEventHandlerUnitTest : UnitTestBase
    {
        [Fact(DisplayName = "Dado que informo dados válidos para alterar um contato então retorna id do contato alterado")]
        public async Task DadoArgumentosCorretos_AlterarContato_RetornoId()
        {
            Mock<IContatoCommandStore> contatoCommandStore = new();
            Mock<ICodigoAreaBrasilCommandStore> codigoAreaBrasilCommandStore = new();
            var eventMessage = ContatoMoq.AlterarContatoEventMessageValido;

            bool contatoParaAlterarExiste = true;
            bool codigoAreaInformadoExiste = true;

            contatoCommandStore.Setup(x => x.AlterarContato(It.IsAny<ContatoEntity>()));
            contatoCommandStore.Setup(x => x.ContatoExiste(It.IsAny<int>())).ReturnsAsync(contatoParaAlterarExiste);
            codigoAreaBrasilCommandStore.Setup(x => x.CodigoAreaExiste(It.IsAny<int>())).ReturnsAsync(codigoAreaInformadoExiste);

            var handler = new AlterarContatoEventHandler(contatoCommandStore.Object, codigoAreaBrasilCommandStore.Object);

            await handler.Handle(eventMessage, CancellationToken.None);

            //Assert.IsType<AlterarContatoCommandResult>(resultado);
            //Assert.Equal(resultado.Id, eventMessage.IdContato);
        }

        [Fact(DisplayName = "Dado que informo dados válidos para alterar um contato mas o id contato informado não existe na base então retorna exceção")]
        public async Task DadoArgumentosCorretosContatoNaoExiste_AlterarContato_RetornoExcecao()
        {
            Mock<IContatoCommandStore> contatoCommandStore = new();
            Mock<ICodigoAreaBrasilCommandStore> codigoAreaBrasilCommandStore = new();
            var eventMessage = ContatoMoq.AlterarContatoEventMessageValido;

            bool contatoParaAlterarExiste = false;

            contatoCommandStore.Setup(x => x.AlterarContato(It.IsAny<ContatoEntity>()));
            contatoCommandStore.Setup(x => x.ContatoExiste(It.IsAny<int>())).ReturnsAsync(contatoParaAlterarExiste);

            var handler = new AlterarContatoEventHandler(contatoCommandStore.Object, codigoAreaBrasilCommandStore.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(eventMessage, CancellationToken.None));
            Assert.NotNull(exception);
            Assert.Equal(string.Format(ErrosNegocio.ErroAoAtualizarRegistro, eventMessage.IdContato), exception.Message);
        }

        [Fact(DisplayName = "Dado que informo dados válidos para alterar um contato mas o ddd não existe na base então retorna exceção")]
        public async Task DadoArgumentosCorretosDddNaoExiste_AlterarContato_RetornoExcecao()
        {
            Mock<IContatoCommandStore> contatoCommandStore = new();
            Mock<ICodigoAreaBrasilCommandStore> codigoAreaBrasilCommandStore = new();
            var eventMessage = ContatoMoq.AlterarContatoEventMessageValido;

            bool contatoParaAlterarExiste = true;
            bool codigoAreaInformadoExiste = false;

            contatoCommandStore.Setup(x => x.ContatoExiste(It.IsAny<int>())).ReturnsAsync(contatoParaAlterarExiste);
            codigoAreaBrasilCommandStore.Setup(x => x.CodigoAreaExiste(It.IsAny<int>())).ReturnsAsync(codigoAreaInformadoExiste);

            var handler = new AlterarContatoEventHandler(contatoCommandStore.Object, codigoAreaBrasilCommandStore.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(eventMessage, CancellationToken.None));

            Assert.NotNull(exception);
            Assert.Equal(string.Format(ErrosNegocio.DddNaoEncontrado, eventMessage.Telefone[..2]), exception.Message);
        }
    }
}