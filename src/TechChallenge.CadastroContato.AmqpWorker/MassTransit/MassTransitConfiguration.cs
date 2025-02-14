using MassTransit;
using TechChallenge.CadastroContato.AmqpWorker.Consumers;
using TechChallenge.CadastroContato.Core.AppSettings;

namespace TechChallenge.CadastroContato.AmqpWorker.MassTransit
{
    internal static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, MassTransitSettings settings)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<IncluirContatoEventConsumer>();
                x.AddConsumer<AlterarContatoEventConsumer>();
                x.AddConsumer<DeletarContatoEventConsumer>();

                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(settings.Host, settings.Port, settings.VirtualHost, h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });

                    config.InsertContactConsumerConfig(context, settings);
                    config.UpdateContactConsumerConfig(context, settings);
                    config.DeleteContactConsumerConfig(context, settings);

                    config.UseJsonSerializer();
                });
            });
        }

        private static void InsertContactConsumerConfig(this IRabbitMqBusFactoryConfigurator config, IBusRegistrationContext context, MassTransitSettings settings)
        {
            config.ReceiveEndpoint(settings.InsertContactQueue.QueueName, e =>
            {
                e.Bind(settings.RegistrationContactExchange.ExchangeName, x =>
                {
                    x.ExchangeType = settings.RegistrationContactExchange.ExchangeType;
                    x.RoutingKey = settings.InsertContactQueue.RoutingKey;
                });
                e.ConfigureConsumer<IncluirContatoEventConsumer>(context);
                e.AutoStart = true;
            });
        }

        private static void UpdateContactConsumerConfig(this IRabbitMqBusFactoryConfigurator config, IBusRegistrationContext context, MassTransitSettings settings)
        {
            config.ReceiveEndpoint(settings.UpdateContactQueue.QueueName, e =>
            {
                e.Bind(settings.RegistrationContactExchange.ExchangeName, x =>
                {
                    x.ExchangeType = settings.RegistrationContactExchange.ExchangeType;
                    x.RoutingKey = settings.UpdateContactQueue.RoutingKey;
                });
                e.ConfigureConsumer<AlterarContatoEventConsumer>(context);
                e.AutoStart = true;
            });
        }

        private static void DeleteContactConsumerConfig(this IRabbitMqBusFactoryConfigurator config, IBusRegistrationContext context, MassTransitSettings settings)
        {
            config.ReceiveEndpoint(settings.DeleteContactQueue.QueueName, e =>
            {
                e.Bind(settings.RegistrationContactExchange.ExchangeName, x =>
                {
                    x.ExchangeType = settings.RegistrationContactExchange.ExchangeType;
                    x.RoutingKey = settings.DeleteContactQueue.RoutingKey;
                });
                e.ConfigureConsumer<DeletarContatoEventConsumer>(context);
                e.AutoStart = true;
            });
        }
    }
}