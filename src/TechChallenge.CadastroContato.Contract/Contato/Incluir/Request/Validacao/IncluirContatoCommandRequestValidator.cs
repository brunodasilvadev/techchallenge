using FluentValidation;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Request.Validacao
{
    public partial class IncluirContatoCommandRequestValidator : AbstractValidator<IncluirContatoCommandRequest>
    {
        public IncluirContatoCommandRequestValidator()
        {
            ValidarNome();
            ValidarTelefone();
            ValidarEmail();
        }
    }
}