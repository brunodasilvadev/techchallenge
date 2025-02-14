using FluentValidation;
using MassTransit;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;

namespace TechChallenge.CadastroContato.AmqpWorker.Consumers
{
    public class DeletarContatoEventConsumer : IConsumer<DeletarContatoEventMessage>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<DeletarContatoEventMessage> _validator;

        public DeletarContatoEventConsumer(IMediator mediator, IValidator<DeletarContatoEventMessage> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task Consume([NotNull] ConsumeContext<DeletarContatoEventMessage> context)
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