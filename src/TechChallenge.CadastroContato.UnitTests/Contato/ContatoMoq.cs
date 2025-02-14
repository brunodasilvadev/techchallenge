using Bogus;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Request;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Request;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Request;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.Contract.Result;

namespace TechChallenge.CadastroContato.UnitTests.Contato
{
    internal static class ContatoMoq
    {
        internal static PesquisarContatosQueryRequest PesquisarContatosQueryRequestSemDdd =>
            new Faker<PesquisarContatosQueryRequest>().CustomInstantiator(x => new PesquisarContatosQueryRequest()
            {
                DddRegiao = null
            }).Generate();

        internal static PesquisarContatosQueryRequest PesquisarContatosQueryRequestComDdd =>
            new Faker<PesquisarContatosQueryRequest>().CustomInstantiator(x => new PesquisarContatosQueryRequest()
            {
                DddRegiao = x.Random.Int(11, 99)
            }).Generate();

        internal static IEnumerable<PesquisarContatosQueryResult> PesquisarContatosQueryResultValido =>
            new Faker<IEnumerable<PesquisarContatosQueryResult>>().CustomInstantiator(x =>
            [
                new () {
                    Id = x.Random.Int(1, 10),
                    Nome = x.Person.FullName,
                    NumeroTelefone = x.Person.Phone,
                    Email = x.Person.Email
                }
            ]).Generate();

        internal static IEnumerable<PesquisarContatosQueryResult> PesquisarContatosQueryResultVazio => [];

        internal static IncluirContatoCommandRequest IncluirContatoCommandRequestValido =>
             new Faker<IncluirContatoCommandRequest>().CustomInstantiator(x => new IncluirContatoCommandRequest()
             {
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = x.Phone.PhoneNumber("11#########")
             }).Generate();

        internal static IncluirContatoEventMessage IncluirContatoEventMessageValido =>
             new Faker<IncluirContatoEventMessage>().CustomInstantiator(x => new IncluirContatoEventMessage()
             {
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = x.Phone.PhoneNumber("11#########")
             }).Generate();

        internal static AlterarContatoCommandRequest AlterarContatoCommandRequestValido =>
             new Faker<AlterarContatoCommandRequest>().CustomInstantiator(x => new AlterarContatoCommandRequest()
             {
                 IdContato = x.Random.Number(1, 999),
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = x.Phone.PhoneNumber("11#########")
             }).Generate();

        internal static AlterarContatoEventMessage AlterarContatoEventMessageValido =>
             new Faker<AlterarContatoEventMessage>().CustomInstantiator(x => new AlterarContatoEventMessage()
             {
                 IdContato = x.Random.Number(1, 999),
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = x.Phone.PhoneNumber("11#########")
             }).Generate();

        internal static DeletarContatoCommandRequest DeletarContatoCommandRequestValido =>
            new Faker<DeletarContatoCommandRequest>().CustomInstantiator(x => new DeletarContatoCommandRequest()
            {
                IdContato = x.Random.Int(1, 100)
            }).Generate();

        internal static DeletarContatoEventMessage DeletarContatoEventMessageValido =>
            new Faker<DeletarContatoEventMessage>().CustomInstantiator(x => new DeletarContatoEventMessage()
            {
                IdContato = x.Random.Int(1, 100)
            }).Generate();

        internal static CommandResult CommandResultValido(Guid traceId) =>
            new Faker<CommandResult>().CustomInstantiator(x => new CommandResult(
                traceId
            )).Generate();
    }
}