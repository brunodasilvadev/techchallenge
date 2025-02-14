using FluentValidation;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.Contract.Contato.Deletar.Request.Validacao
{
    public partial class DeletarContatoCommandRequestValidator : AbstractValidator<DeletarContatoCommandRequest>
    {
        private void ValidarIdContato()
        {
            RuleFor(contato => contato.IdContato)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrosValidacao.CampoObrigatorio, nameof(DeletarContatoCommandRequest.IdContato)));
        }
    }
}