using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Deletar.Message.Validacao
{
    public partial class DeletarContatoEventMessageValidator : AbstractValidator<DeletarContatoEventMessage>
    {
        private void ValidarIdContato()
        {
            RuleFor(contato => contato.IdContato)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(DeletarContatoEventMessage.IdContato)));
        }
    }
}