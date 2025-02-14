using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Request.Validacao
{
    public partial class IncluirContatoCommandRequestValidator : AbstractValidator<IncluirContatoCommandRequest>
    {
        private void ValidarTelefone()
        {
            RuleFor(contato => contato.Telefone)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(IncluirContatoCommandRequest.Telefone)))
                .Matches(@"^\d+$")
                .WithMessage(string.Format(ErrosValidacao.CampoAceitaApenasNumeros, nameof(IncluirContatoCommandRequest.Telefone)))
                .Length(10, 11)
                .WithMessage(string.Format(ErrosValidacao.RangeNumero, nameof(IncluirContatoCommandRequest.Telefone), 10, 11));
        }
    }
}