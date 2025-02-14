using MediatR;
using System.Text.Json.Serialization;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Request
{
    public class IncluirContatoCommandRequest : IRequest<CommandResult>
    {
        [JsonIgnore]
        public Guid CorrelationId { get; } = Guid.NewGuid();

        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
    }
}