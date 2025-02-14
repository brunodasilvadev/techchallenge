using Dapper;
using System.Data;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Core.Contato.Model;

namespace TechChallenge.CadastroContato.CommandStore.Contato
{
    public sealed class ContatoCommandStore(IDbConnection context) : IContatoCommandStore
    {
        public readonly IDbConnection _context = context;

        public async Task<int> IncluirContato(ContatoEntity contato)
        {
            int idContatoCadastrado = await _context.ExecuteScalarAsync<int>(ContatoCommandStoreConsts.INCLUIR_CONTATO, new
            {
                pr_nome = contato.Nome,
                pr_ddd = contato.DddTelefone,
                pr_telefone = contato.NumeroTelefone,
                pr_email = contato.Email
            });

            return idContatoCadastrado;
        }

        public async Task AlterarContato(ContatoEntity contato)
        {
            await _context.ExecuteScalarAsync<int>(ContatoCommandStoreConsts.ALTERAR_CONTATO, new
            {
                pr_idcontato = contato.Id,
                pr_nome = contato.Nome,
                pr_ddd = contato.DddTelefone,
                pr_telefone = contato.NumeroTelefone,
                pr_email = contato.Email
            });
        }

        public async Task<bool> ContatoExiste(int idContato)
        {
            return await _context.QueryFirstOrDefaultAsync<bool>(ContatoCommandStoreConsts.CONTATO_EXISTE, new
            {
                pr_idcontato = idContato
            });
        }

        public async Task<int> DeletarContato(int id)
        {
            return await _context.ExecuteAsync(ContatoCommandStoreConsts.DELETAR_CONTATO, new
            {
                pr_id = id
            });
        }
    }
}