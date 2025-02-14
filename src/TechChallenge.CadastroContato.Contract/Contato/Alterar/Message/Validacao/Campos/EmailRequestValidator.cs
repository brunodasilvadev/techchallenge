using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Message.Validacao
{
    public partial class AlterarContatoEventMessageValidator : AbstractValidator<AlterarContatoEventMessage>
    {
        private void ValidarEmail()
        {
            RuleFor(contato => contato.Email)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoEventMessage.Email)))
                .EmailAddress()
                .WithMessage(string.Format(ErrosValidacao.EmailInvalido, nameof(AlterarContatoEventMessage.Email)));
        }
    }
}