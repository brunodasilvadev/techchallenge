using FluentValidation;

namespace TechChallenge.CadastroContato.Contract.Contato.Deletar.Request.Validacao
{
    public partial class DeletarContatoCommandRequestValidator : AbstractValidator<DeletarContatoCommandRequest>
    {
        public DeletarContatoCommandRequestValidator()
        {
            ValidarIdContato();
        }
    }
}