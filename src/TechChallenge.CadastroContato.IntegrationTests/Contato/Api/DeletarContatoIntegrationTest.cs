using System.Net;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;
using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests.Contato.Api
{
    [Collection("Database Collection")]
    public class DeletarContatoIntegrationTest(CustomApplicationFactory app, DatabaseFixture databaseFixture) : IntegrationTestBase(app, databaseFixture)
    {
        private readonly ContatoMoqIntegrationTest _contatoMoq = new(app, databaseFixture);

        [Fact]
        public async Task DeletarContato_ReturnsAccepted()
        {
            // Arrange
            int idContatoIncluido = await _contatoMoq.IncluirContato();
            Assert.True(idContatoIncluido > 0);

            // Act
            var response = await _client.DeleteAsync($"/api/contatos/{idContatoIncluido}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            Assert.True(await _harness.Published.Any<DeletarContatoEventMessage>());
        }
    }
}