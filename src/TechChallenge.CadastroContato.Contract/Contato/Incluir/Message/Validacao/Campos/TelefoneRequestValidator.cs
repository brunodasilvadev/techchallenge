using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Message.Validacao
{
    public partial class IncluirContatoEventMessageValidator : AbstractValidator<IncluirContatoEventMessage>
    {
        private void ValidarTelefone()
        {
            RuleFor(contato => contato.Telefone)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(IncluirContatoEventMessage.Telefone)))
                .Matches(@"^\d+$")
                .WithMessage(string.Format(ErrosValidacao.CampoAceitaApenasNumeros, nameof(IncluirContatoEventMessage.Telefone)))
                .Length(10, 11)
                .WithMessage(string.Format(ErrosValidacao.RangeNumero, nameof(IncluirContatoEventMessage.Telefone), 10, 11));
        }
    }
}