using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Request.Validacao
{
    public partial class AlterarContatoCommandRequestValidator : AbstractValidator<AlterarContatoCommandRequest>
    {
        private void ValidarTelefone()
        {
            RuleFor(contato => contato.Telefone)
                .NotEmpty()
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoCommandRequest.Telefone)))
                .Matches(@"^\d+$")
                .WithMessage(string.Format(ErrosValidacao.CampoAceitaApenasNumeros, nameof(AlterarContatoCommandRequest.Telefone)))
                .Length(10, 11)
                .WithMessage(string.Format(ErrosValidacao.RangeNumero, nameof(AlterarContatoCommandRequest.Telefone), 10, 11));
        }
    }
}