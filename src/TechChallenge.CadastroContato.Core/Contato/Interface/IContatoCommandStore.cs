using TechChallenge.CadastroContato.Core.Contato.Model;

namespace TechChallenge.CadastroContato.Core.Contato.Interface
{
    public interface IContatoCommandStore
    {
        public Task<int> IncluirContato(ContatoEntity contato);

        public Task AlterarContato(ContatoEntity contato);

        public Task<bool> ContatoExiste(int idContato);

        public Task<int> DeletarContato(int id);
    }
}