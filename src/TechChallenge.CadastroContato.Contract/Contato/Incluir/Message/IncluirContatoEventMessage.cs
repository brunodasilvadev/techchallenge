using MediatR;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Message
{
    public class IncluirContatoEventMessage : IRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
    }
}