using MassTransit;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;
using TechChallenge.CadastroContato.Core.AppSettings;

namespace TechChallenge.CadastroContato.AmqpWorker.MassTransit
{
    internal static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, [NotNull] MassTransitSettings settings)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(settings.Host, settings.Port, settings.VirtualHost, h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });

                    config.IncluirContatoProducerConfig(settings);
                    config.AlterarContatoProducerConfig(settings);
                    config.DeletarContatoProducerConfig(settings);
                });
            });
        }

        private static void IncluirContatoProducerConfig(this IRabbitMqBusFactoryConfigurator config, MassTransitSettings settings)
        {
            config.Message<IncluirContatoEventMessage>(m =>
            {
                m.SetEntityName(settings.RegistrationContactExchange.ExchangeName);
            });

            config.Publish<IncluirContatoEventMessage>(x =>
            {
                x.ExchangeType = settings.RegistrationContactExchange.ExchangeType;
            });

            config.Send<IncluirContatoEventMessage>(s =>
            {
                s.UseRoutingKeyFormatter(context => settings.InsertContactQueue.RoutingKey);
            });
        }

        private static void AlterarContatoProducerConfig(this IRabbitMqBusFactoryConfigurator config, MassTransitSettings settings)
        {
            config.Message<AlterarContatoEventMessage>(m =>
            {
                m.SetEntityName(settings.RegistrationContactExchange.ExchangeName);
            });

            config.Publish<AlterarContatoEventMessage>(x =>
            {
                x.ExchangeType = settings.RegistrationContactExchange.ExchangeType;
            });

            config.Send<AlterarContatoEventMessage>(s =>
            {
                s.UseRoutingKeyFormatter(context => settings.UpdateContactQueue.RoutingKey);
            });
        }

        private static void DeletarContatoProducerConfig(this IRabbitMqBusFactoryConfigurator config, MassTransitSettings settings)
        {
            config.Message<DeletarContatoEventMessage>(m =>
            {
                m.SetEntityName(settings.RegistrationContactExchange.ExchangeName);
            });

            config.Publish<DeletarContatoEventMessage>(x =>
            {
                x.ExchangeType = settings.RegistrationContactExchange.ExchangeType;
            });

            config.Send<DeletarContatoEventMessage>(s =>
            {
                s.UseRoutingKeyFormatter(context => settings.DeleteContactQueue.RoutingKey);
            });
        }
    }
}