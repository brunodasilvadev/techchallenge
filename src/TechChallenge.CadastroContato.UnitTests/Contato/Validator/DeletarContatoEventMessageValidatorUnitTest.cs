using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message.Validacao;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Validator
{
    public class DeletarContatoEventMessageValidatorUnitTest
    {
        [Fact(DisplayName = "Dado que informo dados válidos para validar um eventMessage de contato então retorna que é valido")]
        public async Task DadoArgumentosCorretos_ValidarEventMessageContato_RetornoValido()
        {
            var eventMessage = ContatoMoq.DeletarContatoEventMessageValido;

            var validator = new DeletarContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.True(resultado.IsValid);
        }

        #region Validação Campo Id

        [Theory(DisplayName = "Dado que informo id com valor 0 ou valor negativo para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task DadoArgumentoIdZeroOuNegativoOuNulo_ValidarEventMessageContato_RetornoInvalido(int idZeroOuNegativo)
        {
            var eventMessage = ContatoMoq.DeletarContatoEventMessageValido;
            eventMessage.IdContato = idZeroOuNegativo;

            var validator = new DeletarContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(eventMessage.IdContato));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Id
    }
}