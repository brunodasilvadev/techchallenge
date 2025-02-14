using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Request.Validacao
{
    public partial class AlterarContatoCommandRequestValidator : AbstractValidator<AlterarContatoCommandRequest>
    {
        private void ValidarEmail()
        {
            RuleFor(contato => contato.Email)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoCommandRequest.Email)))
                .EmailAddress()
                .WithMessage(string.Format(ErrosValidacao.EmailInvalido, nameof(AlterarContatoCommandRequest.Email)));
        }
    }
}