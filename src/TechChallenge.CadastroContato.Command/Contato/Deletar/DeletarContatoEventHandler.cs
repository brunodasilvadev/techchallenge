using MediatR;
using System.Diagnostics.CodeAnalysis;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;
using TechChallenge.CadastroContato.Core.Contato.Interface;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Command.Contato.Deletar
{
    public sealed class DeletarContatoEventHandler(IContatoCommandStore contatoCommandStore) : IRequestHandler<DeletarContatoEventMessage>
    {
        private readonly IContatoCommandStore _contatoCommandStore = contatoCommandStore;

        public async Task Handle([NotNull] DeletarContatoEventMessage request, CancellationToken cancellationToken)
        {
            int retorno = await _contatoCommandStore.DeletarContato(request.IdContato);

            if (retorno is 0)
            {
                throw new InvalidOperationException(string.Format(ErrosNegocio.ErroAoDeletarRegistro, request.IdContato));
            }
        }
    }
}