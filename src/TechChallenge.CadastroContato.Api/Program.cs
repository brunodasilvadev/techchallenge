using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;
using TechChallenge.CadastroContato.AmqpWorker.MassTransit;
using TechChallenge.CadastroContato.Api.Diagnostic;
using TechChallenge.CadastroContato.Api.Metrics;
using TechChallenge.CadastroContato.Api.PipelineBehavior;
using TechChallenge.CadastroContato.Command;
using TechChallenge.CadastroContato.CommandStore;
using TechChallenge.CadastroContato.Contract;
using TechChallenge.CadastroContato.Core.AppSettings;
using TechChallenge.CadastroContato.Query;
using TechChallenge.CadastroContato.QueryStore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddPrometheusConfiguration();

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");

IConfiguration configuration = configurationBuilder.Build();

var appSettings = configuration.GetSection("AppSettings").Get<ApiSettings>() ?? throw new NullReferenceException("Appsettings não está configurado.");

services.AddSingleton(appSettings);

// Add services to the container.
services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
services.AddMassTransitConfiguration(appSettings.MassTransit);
services.AddCommandStoreInjection();
services.AddQueryStoreInjection();
services.AddCommandHandlerInjection();
services.AddQueryHandlerInjection();
services.AddFluentValidatorInjection();

services.AddExceptionHandler<ValidationExceptionHandler>();
services.AddExceptionHandler<UnexpectedExceptionHandler>();
services.AddProblemDetails();

services.AddScoped<IDbConnection>((connection) => new SqlConnection(appSettings.ConnectionString.SqlServer));

// Add CORS policy
services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseRouting();

app.MapPrometheusScrapingEndpoint(); // Exponha as métricas em /metrics

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use the CORS policy
app.UseCors("AllowAllOrigins");

app.UsePrometheusMiddleware();

app.MapControllers();

app.Run();

public partial class Program
{ }