using Microsoft.Extensions.DependencyInjection;

namespace TechChallenge.CadastroContato.Query
{
    public static class QueryInitializer
    {
        public static void AddQueryHandlerInjection(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}