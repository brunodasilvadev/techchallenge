using Microsoft.Data.SqlClient;
using System.Data;
using TechChallenge.CadastroContato.AmqpWorker.MassTransit;
using TechChallenge.CadastroContato.Command;
using TechChallenge.CadastroContato.CommandStore;
using TechChallenge.CadastroContato.Contract;
using TechChallenge.CadastroContato.Core.AppSettings;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");

IConfiguration configuration = configurationBuilder.Build();

var appSettings = configuration.GetSection("AppSettings").Get<AmqpSettings>() ?? throw new NullReferenceException("Appsettings não está configurado.");

services.AddSingleton(appSettings);

services.AddMassTransitConfiguration(appSettings.MassTransit);
services.AddCommandHandlerInjection();
services.AddFluentValidatorInjection();
services.AddCommandStoreInjection();

services.AddScoped<IDbConnection>((connection) => new SqlConnection(appSettings.ConnectionString.SqlServer));

var host = builder.Build();

host.Run();

public partial class Program
{ }