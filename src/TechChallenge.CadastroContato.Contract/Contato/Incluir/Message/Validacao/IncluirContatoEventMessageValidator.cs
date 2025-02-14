using FluentValidation;

namespace TechChallenge.CadastroContato.Contract.Contato.Incluir.Message.Validacao
{
    public partial class IncluirContatoEventMessageValidator : AbstractValidator<IncluirContatoEventMessage>
    {
        public IncluirContatoEventMessageValidator()
        {
            ValidarNome();
            ValidarTelefone();
            ValidarEmail();
        }
    }
}