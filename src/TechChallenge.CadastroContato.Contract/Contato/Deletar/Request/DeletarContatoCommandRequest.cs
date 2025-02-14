using MediatR;
using System.Text.Json.Serialization;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Contract.Contato.Deletar.Request
{
    public class DeletarContatoCommandRequest : IRequest<CommandResult>
    {
        [JsonIgnore]
        public Guid CorrelationId { get; } = Guid.NewGuid();

        public required int IdContato { get; set; }
    }
}