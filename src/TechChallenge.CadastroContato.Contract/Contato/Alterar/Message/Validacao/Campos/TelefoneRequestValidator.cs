using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Message.Validacao
{
    public partial class AlterarContatoEventMessageValidator : AbstractValidator<AlterarContatoEventMessage>
    {
        private void ValidarTelefone()
        {
            RuleFor(contato => contato.Telefone)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoEventMessage.Telefone)))
                .Matches(@"^\d+$")
                .WithMessage(string.Format(ErrosValidacao.CampoAceitaApenasNumeros, nameof(AlterarContatoEventMessage.Telefone)))
                .Length(10, 11)
                .WithMessage(string.Format(ErrosValidacao.RangeNumero, nameof(AlterarContatoEventMessage.Telefone), 10, 11));
        }
    }
}