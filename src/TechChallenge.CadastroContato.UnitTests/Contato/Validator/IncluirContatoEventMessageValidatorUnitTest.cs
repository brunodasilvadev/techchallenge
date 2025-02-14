using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message.Validacao;
using TechChallenge.CadastroContato.Language;

namespace TechChallenge.CadastroContato.UnitTests.Contato.Validator
{
    public class IncluirContatoEventMessageValidatorUnitTest : UnitTestBase
    {
        [Fact(DisplayName = "Dado que informo dados válidos para validar um eventMessage de contato então retorna que é valido")]
        public async Task DadoArgumentosCorretos_ValidarEventMessageContato_RetornoValido()
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.True(resultado.IsValid);
        }

        #region Validação Campo Nome

        [Theory(DisplayName = "Dado que informo nome vazio ou nulo para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData("")]
        [InlineData(null)]
        public async Task DadoArgumentoNomeVazioOuNulo_ValidarEventMessageContato_RetornoInvalido(string nomeVazioOuNulo)
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            eventMessage.Nome = nomeVazioOuNulo;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(eventMessage.Nome));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Fact(DisplayName = "Dado que informo nome com tamanho que excede 100 caracteres para validar um eventMessage de contato então retorna que é invalido")]
        public async Task DadoArgumentoNomeTamanhoIncorreto_ValidarEventMessageContato_RetornoInvalido()
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            string nomeComMaisDe100Caracteres = _faker.Random.String2(101);
            eventMessage.Nome = nomeComMaisDe100Caracteres;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.ExcedeuTamanhoMaximoCaracteres, nameof(eventMessage.Nome), 100);
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Nome

        #region Validação Campo Telefone

        [Theory(DisplayName = "Dado que informo telefone vazio ou nulo para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData("")]
        [InlineData(null)]
        public async Task DadoArgumentoTelefoneVazioOuNulo_ValidarEventMessageContato_RetornoInvalido(string telefoneVazioOuNulo)
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            eventMessage.Telefone = telefoneVazioOuNulo;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(eventMessage.Telefone));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Theory(DisplayName = "Dado que informo telefone inválido que não tem apenas números para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData("114002-9999")]
        [InlineData("(11)4002000")]
        public async Task DadoArgumentoTelefoneNaoTemApenasNumero_ValidarEventMessageContato_RetornoInvalido(string telefoneInvalido)
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            eventMessage.Telefone = telefoneInvalido;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoAceitaApenasNumeros, nameof(eventMessage.Telefone));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Theory(DisplayName = "Dado que informo telefone com tamanho inválido para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData("123456789")]
        [InlineData("123456789123")]
        public async Task DadoArgumentoTelefoneTamanhoIncorreto_ValidarEventMessageContato_RetornoInvalido(string telefoneTamanhoInvalido)
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            eventMessage.Telefone = telefoneTamanhoInvalido;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.RangeNumero, nameof(eventMessage.Telefone), 10, 11);
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Telefone

        #region Validação Campo Email

        [Theory(DisplayName = "Dado que informo email vazio ou nulo para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData("")]
        [InlineData(null)]
        public async Task DadoArgumentoEmailVazioOuNulo_ValidarEventMessageContato_RetornoInvalido(string emailVazioOuNulo)
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            eventMessage.Email = emailVazioOuNulo;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.CampoObrigatorio, nameof(eventMessage.Email));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        [Theory(DisplayName = "Dado que informo email inválido para validar um eventMessage de contato então retorna que é invalido")]
        [InlineData("email")]
        [InlineData("email@")]
        [InlineData("email.com")]
        public async Task DadoArgumentoEmailIncorreto_ValidarEventMessageContato_RetornoInvalido(string emailInvalido)
        {
            var eventMessage = ContatoMoq.IncluirContatoEventMessageValido;
            eventMessage.Email = emailInvalido;

            var validator = new IncluirContatoEventMessageValidator();
            var resultado = await validator.ValidateAsync(eventMessage);

            Assert.False(resultado.IsValid);

            string mensagemErroEsperada = string.Format(ErrosValidacao.EmailInvalido, nameof(eventMessage.Email));
            var mensagemErroRetornada = resultado.Errors.First().ErrorMessage;

            Assert.Equal(mensagemErroRetornada, mensagemErroEsperada);
        }

        #endregion Validação Campo Email
    }
}