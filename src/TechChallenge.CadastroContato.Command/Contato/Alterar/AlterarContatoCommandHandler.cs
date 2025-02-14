using MassTransit;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Request;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Command.Contato.Alterar
{
    public sealed class AlterarContatoCommandHandler(IBus bus) : IRequestHandler<AlterarContatoCommandRequest, CommandResult>
    {
        private readonly IBus _bus = bus;

        public async Task<CommandResult> Handle([NotNull] AlterarContatoCommandRequest request, CancellationToken cancellationToken)
        {
            var message = new AlterarContatoEventMessage() { IdContato = request.IdContato, Email = request.Email, Nome = request.Nome, Telefone = request.Telefone };

            await _bus.Publish(message, context =>
            {
                context.CorrelationId = request.CorrelationId;
            }, cancellationToken);

            return new(request.CorrelationId);
        }
    }
}