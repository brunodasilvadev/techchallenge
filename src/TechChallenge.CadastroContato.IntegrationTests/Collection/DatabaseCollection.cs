using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests.Collection
{
    [CollectionDefinition("Database Collection", DisableParallelization = true)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}