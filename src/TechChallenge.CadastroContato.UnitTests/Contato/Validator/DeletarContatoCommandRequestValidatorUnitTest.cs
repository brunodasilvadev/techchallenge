using TechChallenge.CadastroContato.Contract.Contato.Deletar.Request.Validacao;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Validator
{
    public class DeletarContatoCommandRequestValidatorUnitTest
    {
        [Fact(DisplayName = "Dado que informo dados válidos para validar um commandRequest de contato então retorna que é valido")]
        public async Task DadoArgumentosCorretos_ValidarCommandRequestContato_RetornoValido()
        {
            var commandRequest = ContatoMoq.DeletarContatoCommandRequestValido;

            var validator = new DeletarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.True(resultado.IsValid);
        }

        #region Validação Campo Id

        [Theory(DisplayName = "Dado que informo id com valor 0 ou valor negativo para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task DadoArgumentoIdZeroOuNegativoOuNulo_ValidarCommandRequestContato_RetornoInvalido(int idZeroOuNegativo)
        {
            var commandRequest = ContatoMoq.DeletarContatoCommandRequestValido;
            commandRequest.IdContato = idZeroOuNegativo;

            var validator = new DeletarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(commandRequest.IdContato));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Id
    }
}