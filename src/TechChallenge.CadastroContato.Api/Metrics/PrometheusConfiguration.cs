using OpenTelemetry.Metrics;

namespace TechChallenge.CadastroContato.Api.Metrics
{
    public static class PrometheusConfiguration
    {
        public static void AddPrometheusConfiguration(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .WithMetrics(b =>
                {
                    b.AddPrometheusExporter(o => o.DisableTotalNameSuffixForCounters = true);
                    b.AddAspNetCoreInstrumentation(); // Adiciona instrumentação para ASP.NET Core, permitindo a coleta de métricas específicas do framework ASP.NET Core, como requisições HTTP.
                    b.AddRuntimeInstrumentation(); // Coleta métricas de tempo de execução, como uso de CPU, memória, coleta de lixo, entre outras.
                    b.AddProcessInstrumentation(); // Adiciona instrumentação para coletar métricas do processo, como uso de CPU e memória do processo específico da aplicação.
                    b.AddMeter(
                        "TechChallenge.CadastroContato.Api",
                        "Microsoft.AspNetCore.Hosting",
                        "Microsoft.AspNetCore.Server.Kestrel"
                    );
                    b.AddView(
                        "http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries =
                            [
                                0, 0.1, 0.5, 1, 2, 5, 7.5, 10
                            ]
                        });
                });
        }
    }
}