using MediatR;

namespace TechChallenge.CadastroContato.Contract.Contato.Deletar.Message
{
    public class DeletarContatoEventMessage : IRequest
    {
        public required int IdContato { get; set; }
    }
}