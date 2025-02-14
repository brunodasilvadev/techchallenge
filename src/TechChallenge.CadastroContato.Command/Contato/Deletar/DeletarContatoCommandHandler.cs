using MassTransit;
using MediatR;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Request;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Command.Contato.Deletar
{
    public sealed class DeletarContatoCommandHandler(IBus bus) : IRequestHandler<DeletarContatoCommandRequest, CommandResult>
    {
        private readonly IBus _bus = bus;

        public async Task<CommandResult> Handle(DeletarContatoCommandRequest request, CancellationToken cancellationToken)
        {
            var message = new DeletarContatoEventMessage() { IdContato = request.IdContato };

            await _bus.Publish(message, context =>
            {
                context.CorrelationId = request.CorrelationId;
            }, cancellationToken);

            return new(request.CorrelationId);
        }
    }
}