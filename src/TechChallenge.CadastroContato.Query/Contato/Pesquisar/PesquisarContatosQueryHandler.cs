using MediatR;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.Query.Contato.Interface;

namespace TechChallenge.CadastroContato.Query.Contato.Pesquisar
{
    public class PesquisarContatosQueryHandler : IRequestHandler<PesquisarContatosQueryRequest, IEnumerable<PesquisarContatosQueryResult>>
    {
        public readonly IContatoQueryStore _contatoQueryStore;

        public PesquisarContatosQueryHandler(IContatoQueryStore contatoQueryStore)
        {
            _contatoQueryStore = contatoQueryStore;
        }

        public async Task<IEnumerable<PesquisarContatosQueryResult>> Handle(PesquisarContatosQueryRequest request, CancellationToken cancellationToken)
        {
            return await _contatoQueryStore.PesquisarContatos(request);
        }
    }
}