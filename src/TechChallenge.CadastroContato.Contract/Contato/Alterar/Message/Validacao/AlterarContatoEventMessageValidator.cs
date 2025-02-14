using FluentValidation;

namespace TechChallenge.CadastroContato.Contract.Contato.Alterar.Message.Validacao
{
    public partial class AlterarContatoEventMessageValidator : AbstractValidator<AlterarContatoEventMessage>
    {
        public AlterarContatoEventMessageValidator()
        {
            ValidarIdContato();
            ValidarNome();
            ValidarTelefone();
            ValidarEmail();
        }
    }
}