using FluentValidation;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Request.Validacao
{
    public partial class AlterarContatoCommandRequestValidator : AbstractValidator<AlterarContatoCommandRequest>
    {
        public AlterarContatoCommandRequestValidator()
        {
            ValidarIdContato();
            ValidarNome();
            ValidarTelefone();
            ValidarEmail();
        }
    }
}