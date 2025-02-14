using FluentValidation;
using MassTransit;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;

namespace TechChallenge.CadastroContato.AmqpWorker.Consumers
{
    public class AlterarContatoEventConsumer : IConsumer<AlterarContatoEventMessage>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AlterarContatoEventMessage> _validator;

        public AlterarContatoEventConsumer(IMediator mediator, IValidator<AlterarContatoEventMessage> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task Consume([NotNull] ConsumeContext<AlterarContatoEventMessage> context)
        {
            try
            {
                var message = context.Message;

                await _validator.ValidateAndThrowAsync(message);

                await _mediator.Send(message);
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ValidationException)
            {
                throw;
            }
        }
    }
}