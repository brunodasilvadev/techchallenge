using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Request.Validacao
{
    public partial class AlterarContatoCommandRequestValidator : AbstractValidator<AlterarContatoCommandRequest>
    {
        private void ValidarNome()
        {
            RuleFor(contato => contato.Nome)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoCommandRequest.Nome)))
                .MaximumLength(100)
                .WithMessage(string.Format(ErrosValidacao.ExcedeuTamanhoMaximoCaracteres, nameof(AlterarContatoCommandRequest.Nome), 100));
        }
    }
}