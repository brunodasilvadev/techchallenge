using Moq;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.Query.Contato.Interface;
using TechChallenge.CadastroContato.Query.Contato.Pesquisar;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Handler
{
    public class PesquisarContatosQueryHandlerUnitTest : UnitTestBase
    {
        [Fact(DisplayName = "Dado que não informo DDD como filtro para pesquisa, retorna todos contatos cadastrados")]
        public async Task DadoArgumentosSemDDD_PesquisarContato_RetornoTodosContatos()
        {
            Mock<IContatoQueryStore> contatoQueryStore = new();

            contatoQueryStore.Setup(x => x.PesquisarContatos(It.IsAny<PesquisarContatosQueryRequest>()))
                .ReturnsAsync(ContatoMoq.PesquisarContatosQueryResultValido);

            var handler = new PesquisarContatosQueryHandler(contatoQueryStore.Object);

            var resultado = await handler.Handle(ContatoMoq.PesquisarContatosQueryRequestSemDdd, CancellationToken.None);

            Assert.IsAssignableFrom<IEnumerable<PesquisarContatosQueryResult>>(resultado);
            Assert.True(resultado.Select(contato => contato.Id) != null);
            Assert.True(resultado.Select(contato => contato.Nome) != null);
            Assert.True(resultado.Select(contato => contato.NumeroTelefone) != null);
            Assert.True(resultado.Select(contato => contato.Email) != null);
        }

        [Fact(DisplayName = "Dado que informo DDD como filtro para pesquisa, com registro existente, retorna todos contatos referentes ao DDD informado")]
        public async Task DadoArgumentosComDDD_PesquisarContato_RetornoTodosContatos()
        {
            Mock<IContatoQueryStore> contatoQueryStore = new();

            contatoQueryStore.Setup(x => x.PesquisarContatos(It.IsAny<PesquisarContatosQueryRequest>()))
                .ReturnsAsync(ContatoMoq.PesquisarContatosQueryResultValido);

            var handler = new PesquisarContatosQueryHandler(contatoQueryStore.Object);

            var resultado = await handler.Handle(ContatoMoq.PesquisarContatosQueryRequestComDdd, CancellationToken.None);

            Assert.IsAssignableFrom<IEnumerable<PesquisarContatosQueryResult>>(resultado);
            Assert.True(resultado.Select(contato => contato.Id) != null);
            Assert.True(resultado.Select(contato => contato.Nome) != null);
            Assert.True(resultado.Select(contato => contato.NumeroTelefone) != null);
            Assert.True(resultado.Select(contato => contato.Email) != null);
        }

        [Fact(DisplayName = "Dado que informo dados válidos para pesquisar contatos, mas não é retornado nenhum registro, então retorna lista vazia de contatos")]
        public async Task DadoArgumentosComDDD_PesquisarContato_RetornoListaVazio()
        {
            Mock<IContatoQueryStore> contatoQueryStore = new();

            contatoQueryStore.Setup(x => x.PesquisarContatos(It.IsAny<PesquisarContatosQueryRequest>()))
                .ReturnsAsync(ContatoMoq.PesquisarContatosQueryResultVazio);

            var handler = new PesquisarContatosQueryHandler(contatoQueryStore.Object);

            var resultado = await handler.Handle(ContatoMoq.PesquisarContatosQueryRequestComDdd, CancellationToken.None);

            Assert.IsAssignableFrom<IEnumerable<PesquisarContatosQueryResult>>(resultado);
            Assert.True(resultado.Count() is 0);
        }
    }
}