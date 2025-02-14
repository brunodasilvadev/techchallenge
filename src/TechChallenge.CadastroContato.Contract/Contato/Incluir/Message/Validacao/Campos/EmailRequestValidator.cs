using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Message.Validacao
{
    public partial class IncluirContatoEventMessageValidator : AbstractValidator<IncluirContatoEventMessage>
    {
        private void ValidarEmail()
        {
            RuleFor(contato => contato.Email)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(IncluirContatoEventMessage.Email)))
                .EmailAddress()
                .WithMessage(string.Format(ErrosValidacao.EmailInvalido, nameof(IncluirContatoEventMessage.Email)));
        }
    }
}