using Bogus;

namespace TechChallenge.CadastroContato.UnitTests
{
    public abstract class UnitTestBase
    {
        protected readonly Faker _faker = new();
    }
}