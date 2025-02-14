using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;
using TechChallenge.CadastroContato.Core.AppSettings;

namespace TechChallenge.CadastroContato.IntegrationTests
{
    public class CustomApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IDbConnection));

                var appSettings = ObterAppSettings();

                services.AddSingleton(appSettings);

                services.AddScoped<IDbConnection>(_ => new SqlConnection(appSettings.ConnectionString.SqlServer));

                services.AddMassTransitTestHarness(x =>
                {
                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });
            });

            base.ConfigureWebHost(builder);
        }

        private static ApiSettings ObterAppSettings()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.IntegrationTests.json");

            IConfiguration configuration = configurationBuilder.Build();

            var appSettings = configuration.GetSection("AppSettings").Get<ApiSettings>() ?? throw new NullReferenceException("Appsettings não está configurado.");

            return appSettings;
        }
    }
}