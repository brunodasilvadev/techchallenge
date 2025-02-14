using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;
using TechChallenge.CadastroContato.Contract.Result;
using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests.Contato.Api
{
    [Collection("Database Collection")]
    public class IncluirContatoIntegrationTest : IntegrationTestBase
    {
        private readonly ContatoMoqIntegrationTest _contatoMoq;

        public IncluirContatoIntegrationTest(CustomApplicationFactory app, DatabaseFixture databaseFixture) : base(app, databaseFixture)
        {
            _contatoMoq = new(app, databaseFixture);
        }

        [Fact]
        public async Task IncluirContato_ReturnsAccepted()
        {
            // Arrange
            var commandRequest = _contatoMoq.IncluirContatoCommandRequestValido;

            // Act
            var response = await _client.PostAsJsonAsync("/api/contatos", commandRequest);
            var responseData = JsonConvert.DeserializeObject<CommandResult>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            Assert.IsAssignableFrom<CommandResult>(responseData);
            Assert.True(responseData?.TraceId != Guid.Empty);
            Assert.True(await _harness.Published.Any<IncluirContatoEventMessage>());
        }
    }
}