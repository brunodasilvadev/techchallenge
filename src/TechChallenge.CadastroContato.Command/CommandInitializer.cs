using Microsoft.Extensions.DependencyInjection;

namespace TechChallenge.CadastroContato.Command
{
    public static class CommandInitializer
    {
        public static void AddCommandHandlerInjection(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}