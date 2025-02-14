using MediatR;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;
using TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Core.Contato.Model;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Command.Contato.Incluir
{
    public sealed class IncluirContatoEventHandler(
        IContatoCommandStore contatoCommandStore,
        ICodigoAreaBrasilCommandStore codigoAreaBrasilCommandStore
    ) : IRequestHandler<IncluirContatoEventMessage>
    {
        private readonly IContatoCommandStore _contatoCommandStore = contatoCommandStore;
        private readonly ICodigoAreaBrasilCommandStore _codigoAreaBrasilCommandStore = codigoAreaBrasilCommandStore;

        public async Task Handle(IncluirContatoEventMessage message, CancellationToken cancellationToken)
        {
            var contato = new ContatoEntity(message.Nome, message.Email, message.Telefone);

            bool dddExiste = await _codigoAreaBrasilCommandStore.CodigoAreaExiste(contato.DddTelefone);

            if (dddExiste is false)
            {
                throw new InvalidOperationException(string.Format(ErrosNegocio.DddNaoEncontrado, contato.DddTelefone));
            }

            await _contatoCommandStore.IncluirContato(contato);
        }
    }
}