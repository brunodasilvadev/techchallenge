using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Message.Validacao
{
    public partial class AlterarContatoEventMessageValidator : AbstractValidator<AlterarContatoEventMessage>
    {
        private void ValidarIdContato()
        {
            RuleFor(contato => contato.IdContato)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoEventMessage.IdContato)));
        }
    }
}