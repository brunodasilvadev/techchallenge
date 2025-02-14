using MediatR;

namespace TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request
{
    public class PesquisarContatosQueryRequest : IRequest<IEnumerable<PesquisarContatosQueryResult>>
    {
        public int? DddRegiao { get; set; }
    }
}