using FluentValidation;
using MassTransit;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;

namespace TechChallenge.CadastroContato.AmqpWorker.Consumers
{
    public class IncluirContatoEventConsumer : IConsumer<IncluirContatoEventMessage>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<IncluirContatoEventMessage> _validator;

        public IncluirContatoEventConsumer(IMediator mediator, IValidator<IncluirContatoEventMessage> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task Consume([NotNull] ConsumeContext<IncluirContatoEventMessage> context)
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