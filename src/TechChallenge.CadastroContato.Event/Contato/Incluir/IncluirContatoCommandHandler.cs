using MediatR;
using TechChallenge.IncluirContato.Contract.Contato.Incluir;
using TechChallenge.IncluirContato.Core.CodigoAreaBrasil.Interface;
using TechChallenge.IncluirContato.Core.Contato.Interface;

namespace TechChallenge.IncluirContato.Command.Contato.Incluir
{
    public sealed class IncluirContatoCommandHandler(
        IContatoCommandStore contatoCommandStore,
        ICodigoAreaBrasilCommandStore codigoAreaBrasilCommandStore
    ) : IRequestHandler<IncluirContatoCommandRequest, IncluirContatoCommandResult>
    {
        private readonly IContatoCommandStore _contatoCommandStore = contatoCommandStore;
        private readonly ICodigoAreaBrasilCommandStore _codigoAreaBrasilCommandStore = codigoAreaBrasilCommandStore;

        public async Task<IncluirContatoCommandResult> Handle(IncluirContatoCommandRequest request, CancellationToken cancellationToken)
        {
            var contato = new ContatoEntity(request.Nome, request.Email, request.Telefone);

            bool dddExiste = await _codigoAreaBrasilCommandStore.CodigoAreaExiste(contato.DddTelefone);

            if (dddExiste is false)
            {
                throw new InvalidOperationException(string.Format(ErrosNegocio.DddNaoEncontrado, contato.DddTelefone));
            }

            int idContatoCadastrado = await _contatoCommandStore.IncluirContato(contato);

            return new IncluirContatoCommandResult(idContatoCadastrado);
        }
    }
}