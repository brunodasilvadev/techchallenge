using FluentValidation;

namespace TechChallenge.CadastroContato.Contract.Contato.Deletar.Message.Validacao
{
    public partial class DeletarContatoEventMessageValidator : AbstractValidator<DeletarContatoEventMessage>
    {
        public DeletarContatoEventMessageValidator()
        {
            ValidarIdContato();
        }
    }
}