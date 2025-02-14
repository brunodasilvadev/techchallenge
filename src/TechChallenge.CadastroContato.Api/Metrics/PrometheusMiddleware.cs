using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace TechChallenge.CadastroContato.Api.Metrics
{
    public static class PrometheusMiddleware
    {
        public static void UsePrometheusMiddleware(this WebApplication app)
        {
            var meter = new Meter("TechChallenge.CadastroContato.Api");

            var requestCounter = meter.CreateCounter<long>("http_requests_total", description: "Contagem de requisições HTTP por status de resposta");

            meter.CreateObservableGauge("process_memory_usage", () =>
            {
                var process = Process.GetCurrentProcess();
                return new Measurement<long>(process.WorkingSet64, new KeyValuePair<string, object?>("http_status", "200"));
            }, "bytes", "Total memory usage in bytes");

            var cpuUsagePercentageMeter = meter.CreateObservableGauge("process_cpu_usage_percentage", () =>
            {
                var process = Process.GetCurrentProcess();
                var totalCpuTime = process.TotalProcessorTime.TotalMilliseconds;
                var elapsedTime = Environment.TickCount64; // Tempo total desde o início do sistema em milissegundos
                var cpuUsagePercentage = (totalCpuTime / (elapsedTime * Environment.ProcessorCount)) * 100;
                return new Measurement<double>(cpuUsagePercentage);
            }, "percentage", "CPU usage percentage");

            var requestDurationMeter = meter.CreateHistogram<double>("http_request_duration", "ms", "Duration of HTTP requests in milliseconds");

            app.Use(async (context, next) =>
            {
                await next();

                var statusCode = context.Response.StatusCode;
                requestCounter.Add(1, new KeyValuePair<string, object?>("http_status", statusCode.ToString()));
            });
        }
    }
}