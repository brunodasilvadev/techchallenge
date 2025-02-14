using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Message.Validacao
{
    public partial class AlterarContatoEventMessageValidator : AbstractValidator<AlterarContatoEventMessage>
    {
        private void ValidarNome()
        {
            RuleFor(contato => contato.Nome)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoEventMessage.Nome)))
                .MaximumLength(100)
                .WithMessage(string.Format(ErrosValidacao.ExcedeuTamanhoMaximoCaracteres, nameof(AlterarContatoEventMessage.Nome), 100));
        }
    }
}