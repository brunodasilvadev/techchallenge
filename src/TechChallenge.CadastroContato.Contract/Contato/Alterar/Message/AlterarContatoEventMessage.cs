using MediatR;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Message
{
    public class AlterarContatoEventMessage : IRequest
    {
        public required int IdContato { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
    }
}