using Microsoft.Extensions.DependencyInjection;
using TechChallenge.CadastroContato.CommandStore.CodigoAreaBrasil;
using TechChallenge.CadastroContato.CommandStore.Contato;
using TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface;
using TechChallenge.CadastroContato.Core.Contato.Interface;

namespace TechChallenge.CadastroContato.CommandStore
{
    public static class CommandStoreInitializer
    {
        public static void AddCommandStoreInjection(this IServiceCollection services)
        {
            services.AddTransient<IContatoCommandStore, ContatoCommandStore>();
            services.AddTransient<ICodigoAreaBrasilCommandStore, CodigoAreaBrasilCommandStore>();
        }
    }
}