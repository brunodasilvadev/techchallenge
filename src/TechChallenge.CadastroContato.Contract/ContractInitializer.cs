using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TechChallenge.CadastroContato.Contract
{
    public static class ContractInitializer
    {
        public static void AddFluentValidatorInjection(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}