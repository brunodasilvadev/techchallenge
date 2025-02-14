using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Request.Validacao
{
    public partial class IncluirContatoCommandRequestValidator : AbstractValidator<IncluirContatoCommandRequest>
    {
        private void ValidarNome()
        {
            RuleFor(contato => contato.Nome)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(IncluirContatoCommandRequest.Nome)))
                .MaximumLength(100)
                .WithMessage(string.Format(ErrosValidacao.ExcedeuTamanhoMaximoCaracteres, nameof(IncluirContatoCommandRequest.Nome), 100));
        }
    }
}