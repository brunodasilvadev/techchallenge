using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;
using TechChallenge.CadastroContato.Contract.Result;
using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests.Contato.Api
{
    [Collection("Database Collection")]
    public class AlterarContatoIntegrationTest(CustomApplicationFactory app, DatabaseFixture databaseFixture) : IntegrationTestBase(app, databaseFixture)
    {
        private readonly ContatoMoqIntegrationTest _contatoMoq = new(app, databaseFixture);

        [Fact]
        public async Task AlterarContato_ReturnsAccepted()
        {
            // Arrange
            int idContatoParaAlterar = await _contatoMoq.IncluirContato();

            var commandRequest = _contatoMoq.AlterarContatoCommandRequestValido;

            // Act
            var response = await _client.PutAsJsonAsync($"/api/contatos/{idContatoParaAlterar}", commandRequest);
            var responseData = JsonConvert.DeserializeObject<CommandResult>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            Assert.IsAssignableFrom<CommandResult>(responseData);
            Assert.True(responseData?.TraceId != Guid.Empty);
            Assert.True(await _harness.Published.Any<AlterarContatoEventMessage>());
        }
    }
}