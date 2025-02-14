using Newtonsoft.Json;
using System.Net;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests.Contato.Api
{
    [Collection("Database Collection")]
    public class PesquisarContatoIntegrationTest(CustomApplicationFactory app, DatabaseFixture databaseFixture) : IntegrationTestBase(app, databaseFixture)
    {
        private readonly ContatoMoqIntegrationTest _contatoMoq = new(app, databaseFixture);

        [Fact]
        public async Task PesquisarContato_ReturnsContatos()
        {
            // Arrange
            int idContatoIncluido = await _contatoMoq.IncluirContato();
            var contatoIncluido = await _contatoMoq.BuscarContato(idContatoIncluido);
            Assert.IsAssignableFrom<PesquisarContatosQueryResult>(contatoIncluido);
            Assert.NotNull(contatoIncluido);

            short dddContatoIncluido = Convert.ToInt16(contatoIncluido?.NumeroTelefone[..2]);

            // Act
            var response = await _client.GetAsync($"/api/contatos?dddRegiao={dddContatoIncluido}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var queryResult = JsonConvert.DeserializeObject<List<PesquisarContatosQueryResult>>(responseContent);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.IsAssignableFrom<List<PesquisarContatosQueryResult>>(queryResult);
            Assert.True(queryResult?.Count > 0);
        }
    }
}