using MediatR;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;
using TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Core.Contato.Model;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Command.Contato.Alterar
{
    public sealed class AlterarContatoEventHandler(
        IContatoCommandStore contatoCommandStore,
        ICodigoAreaBrasilCommandStore codigoAreaBrasilCommandStore
    ) : IRequestHandler<AlterarContatoEventMessage>
    {
        private readonly IContatoCommandStore _contatoCommandStore = contatoCommandStore;
        private readonly ICodigoAreaBrasilCommandStore _codigoAreaBrasilCommandStore = codigoAreaBrasilCommandStore;

        public async Task Handle(AlterarContatoEventMessage request, CancellationToken cancellationToken)
        {
            var contato = new ContatoEntity(request.IdContato, request.Nome, request.Email, request.Telefone);

            bool contatoExiste = await _contatoCommandStore.ContatoExiste(contato.Id);

            if (contatoExiste is false)
            {
                throw new InvalidOperationException(string.Format(ErrosNegocio.ErroAoAtualizarRegistro, contato.Id));
            }

            bool dddExiste = await _codigoAreaBrasilCommandStore.CodigoAreaExiste(contato.DddTelefone);

            if (dddExiste is false)
            {
                throw new InvalidOperationException(string.Format(ErrosNegocio.DddNaoEncontrado, contato.DddTelefone));
            }

            await _contatoCommandStore.AlterarContato(contato);
        }
    }
}