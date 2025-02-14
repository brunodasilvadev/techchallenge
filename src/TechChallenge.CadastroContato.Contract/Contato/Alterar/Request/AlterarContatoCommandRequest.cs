using MediatR;
using System.Text.Json.Serialization;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Request
{
    public class AlterarContatoCommandRequest : IRequest<CommandResult>
    {
        [JsonIgnore]
        public Guid CorrelationId { get; } = Guid.NewGuid();

        [JsonIgnore]
        public int IdContato { get; set; }

        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
    }
}