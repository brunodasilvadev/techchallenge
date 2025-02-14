using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;

namespace TechChallenge.CadastroContato.Query.Contato.Interface
{
    public interface IContatoQueryStore
    {
        Task<IEnumerable<PesquisarContatosQueryResult>> PesquisarContatos(PesquisarContatosQueryRequest filtro);
    }
}