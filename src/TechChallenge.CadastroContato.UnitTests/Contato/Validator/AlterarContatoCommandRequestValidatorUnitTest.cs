using TechChallenge.CadastroContato.Contract.Contato.Alterar.Request.Validacao;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Validator
{
    public class AlterarContatoCommandRequestValidatorUnitTest : UnitTestBase
    {
        [Fact(DisplayName = "Dado que informo dados válidos para validar um commandRequest de contato então retorna que é valido")]
        public async Task DadoArgumentosCorretos_ValidarCommandRequestContato_RetornoValido()
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.True(resultado.IsValid);
        }

        #region Validação Campo Id

        [Theory(DisplayName = "Dado que informo id com valor 0 ou valor negativo para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task DadoArgumentoIdZeroOuNegativoOuNulo_ValidarCommandRequestContato_RetornoInvalido(int idZeroOuNegativo)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.IdContato = idZeroOuNegativo;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(commandRequest.IdContato));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Id

        #region Validação Campo Nome

        [Theory(DisplayName = "Dado que informo nome vazio ou nulo para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData("")]
        [InlineData(null)]
        public async Task DadoArgumentoNomeVazioOuNulo_ValidarCommandRequestContato_RetornoInvalido(string nomeVazioOuNulo)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.Nome = nomeVazioOuNulo;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(commandRequest.Nome));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Fact(DisplayName = "Dado que informo nome com tamanho que excede 100 caracteres para validar um commandRequest de contato então retorna que é invalido")]
        public async Task DadoArgumentoNomeTamanhoIncorreto_ValidarCommandRequestContato_RetornoInvalido()
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            string nomeComMaisDe100Caracteres = _faker.Random.String2(101);
            commandRequest.Nome = nomeComMaisDe100Caracteres;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.ExcedeuTamanhoMaximoCaracteres, nameof(commandRequest.Nome), 100);
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Nome

        #region Validação Campo Telefone

        [Theory(DisplayName = "Dado que informo telefone vazio ou nulo para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData("")]
        [InlineData(null)]
        public async Task DadoArgumentoTelefoneVazioOuNulo_ValidarCommandRequestContato_RetornoInvalido(string telefoneVazioOuNulo)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.Telefone = telefoneVazioOuNulo;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(commandRequest.Telefone));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Theory(DisplayName = "Dado que informo telefone inválido que não tem apenas números para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData("114002-9999")]
        [InlineData("(11)4002000")]
        public async Task DadoArgumentoTelefoneNaoTemApenasNumero_ValidarCommandRequestContato_RetornoInvalido(string telefoneInvalido)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.Telefone = telefoneInvalido;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoAceitaApenasNumeros, nameof(commandRequest.Telefone));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Theory(DisplayName = "Dado que informo telefone com tamanho inválido para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData("123456789")]
        [InlineData("123456789123")]
        public async Task DadoArgumentoTelefoneTamanhoIncorreto_ValidarCommandRequestContato_RetornoInvalido(string telefoneTamanhoInvalido)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.Telefone = telefoneTamanhoInvalido;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.RangeNumero, nameof(commandRequest.Telefone), 10, 11);
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Telefone

        #region Validação Campo Email

        [Theory(DisplayName = "Dado que informo email vazio ou nulo para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData("")]
        [InlineData(null)]
        public async Task DadoArgumentoEmailVazioOuNulo_ValidarCommandRequestContato_RetornoInvalido(string emailVazioOuNulo)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.Email = emailVazioOuNulo;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(commandRequest.Email));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Theory(DisplayName = "Dado que informo email inválido para validar um commandRequest de contato então retorna que é invalido")]
        [InlineData("email")]
        [InlineData("email@")]
        [InlineData("email.com")]
        public async Task DadoArgumentoEmailIncorreto_ValidarCommandRequestContato_RetornoInvalido(string emailInvalido)
        {
            var commandRequest = ContatoMoq.AlterarContatoCommandRequestValido;
            commandRequest.Email = emailInvalido;

            var validator = new AlterarContatoCommandRequestValidator();
            var resultado = await validator.ValidateAsync(commandRequest);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.EmailInvalido, nameof(commandRequest.Email));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Email
    }
}