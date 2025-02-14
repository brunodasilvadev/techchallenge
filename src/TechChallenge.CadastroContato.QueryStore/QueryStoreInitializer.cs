using Microsoft.Extensions.DependencyInjection;
using TechChallenge.CadastroContato.Query.Contato.Interface;
using TechChallenge.CadastroContato.QueryStore.Contato;

namespace TechChallenge.CadastroContato.QueryStore
{
    public static class QueryStoreInitializer
    {
        public static void AddQueryStoreInjection(this IServiceCollection services)
        {
            services.AddTransient<IContatoQueryStore, ContatoQueryStore>();
        }
    }
}