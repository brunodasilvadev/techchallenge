using Bogus;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests
{
    public abstract class IntegrationTestBase(
        CustomApplicationFactory app,
        DatabaseFixture databaseFixture)
        : IClassFixture<CustomApplicationFactory>,
        IClassFixture<DatabaseFixture>
    {
        protected readonly CustomApplicationFactory _app = app;
        protected readonly DatabaseFixture _databaseFixture = databaseFixture;
        protected readonly HttpClient _client = app.CreateClient();
        protected readonly Faker _faker = new();
        protected readonly ITestHarness _harness = app.Services.GetRequiredService<ITestHarness>();
        protected readonly IBusControl _bus = app.Services.GetRequiredService<IBusControl>();
    }
}