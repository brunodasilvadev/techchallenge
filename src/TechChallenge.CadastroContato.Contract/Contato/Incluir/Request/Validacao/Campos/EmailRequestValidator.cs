using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Request.Validacao
{
    public partial class IncluirContatoCommandRequestValidator : AbstractValidator<IncluirContatoCommandRequest>
    {
        private void ValidarEmail()
        {
            RuleFor(contato => contato.Email)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(IncluirContatoCommandRequest.Email)))
                .EmailAddress()
                .WithMessage(string.Format(ErrosValidacao.EmailInvalido, nameof(IncluirContatoCommandRequest.Email)));
        }
    }
}