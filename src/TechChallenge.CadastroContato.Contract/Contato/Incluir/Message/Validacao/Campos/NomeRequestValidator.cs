using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Message.Validacao
{
    public partial class IncluirContatoEventMessageValidator : AbstractValidator<IncluirContatoEventMessage>
    {
        private void ValidarNome()
        {
            RuleFor(contato => contato.Nome)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(IncluirContatoEventMessage.Nome)))
                .MaximumLength(100)
                .WithMessage(string.Format(ErrosValidacao.ExcedeuTamanhoMaximoCaracteres, nameof(IncluirContatoEventMessage.Nome), 100));
        }
    }
}