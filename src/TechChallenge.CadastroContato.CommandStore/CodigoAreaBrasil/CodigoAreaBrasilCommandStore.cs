using Dapper;
using System.Data;
using TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface;

namespace TechChallenge.CadastroContato.CommandStore.CodigoAreaBrasil
{
    public sealed class CodigoAreaBrasilCommandStore(IDbConnection context) : ICodigoAreaBrasilCommandStore
    {
        public readonly IDbConnection _context = context;

        public async Task<bool> CodigoAreaExiste(int ddd)
        {
            return await _context
                 .QueryFirstOrDefaultAsync<bool>(CodigoAreaBrasilCommandStoreConsts.CODIGO_AREA_EXISTE,
                 new
                 {
                     pr_ddd = ddd
                 });
        }
    }
}