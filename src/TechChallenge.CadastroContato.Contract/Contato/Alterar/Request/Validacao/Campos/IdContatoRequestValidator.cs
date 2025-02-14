using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Request.Validacao
{
    public partial class AlterarContatoCommandRequestValidator : AbstractValidator<AlterarContatoCommandRequest>
    {
        private void ValidarIdContato()
        {
            RuleFor(contato => contato.IdContato)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(AlterarContatoCommandRequest.IdContato)));
        }
    }
}