using Dapper;
using System.Data;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.Query.Contato.Interface;

namespace TechChallenge.CadastroContato.QueryStore.Contato
{
    public sealed class ContatoQueryStore(IDbConnection context) : IContatoQueryStore
    {
        public readonly IDbConnection _context = context;

        public async Task<IEnumerable<PesquisarContatosQueryResult>> PesquisarContatos(PesquisarContatosQueryRequest filtro)
        {
            return await _context.QueryAsync<PesquisarContatosQueryResult>(ContatoQueryStoreConsts.PESQUISAR_CONTATOS,
                new
                {
                    pr_nrddd = filtro.DddRegiao
                });
        }
    }
}