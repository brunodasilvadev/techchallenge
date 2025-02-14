using MassTransit;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Request;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Command.Contato.Incluir
{
    public sealed class IncluirContatoCommandHandler(IBus bus) : IRequestHandler<IncluirContatoCommandRequest, CommandResult>
    {
        private readonly IBus _bus = bus;

        public async Task<CommandResult> Handle([NotNull] IncluirContatoCommandRequest request, CancellationToken cancellationToken)
        {
            var message = new IncluirContatoEventMessage() { Email = request.Email, Nome = request.Nome, Telefone = request.Telefone };

            await _bus.Publish(message, context =>
            {
                context.CorrelationId = request.CorrelationId;
            }, cancellationToken);

            return new(request.CorrelationId);
        }
    }
}