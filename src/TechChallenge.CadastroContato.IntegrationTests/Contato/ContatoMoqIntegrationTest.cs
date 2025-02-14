using Bogus;
using Dapper;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Alterar.Request;
using TechChallenge.CadastroContato.Contract.Contato.Deletar.Message;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Message;
using TechChallenge.CadastroContato.Contract.Contato.Incluir.Request;
using TechChallenge.CadastroContato.Contract.Contato.Pesquisar.Request;
using TechChallenge.CadastroContato.Contract.Result;
using TechChallenge.CadastroContato.Core.Contato.Model;
using TechChallenge.CadastroContato.IntegrationTests.Fixture;

namespace TechChallenge.CadastroContato.IntegrationTests.Contato
{
    public class ContatoMoqIntegrationTest(CustomApplicationFactory app, DatabaseFixture databaseFixture) : IntegrationTestBase(app, databaseFixture)
    {
        public static readonly List<int> listaDddValido = [11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 24, 27, 28, 31, 32, 33, 34, 35, 37, 38, 41, 42, 43, 44, 45, 46, 47, 48, 49, 51, 53, 54, 55, 61, 62, 63, 64, 65, 66, 67, 68, 69, 71, 73, 74, 75, 77, 79, 81, 82, 83, 84, 85, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99];

        public PesquisarContatosQueryRequest PesquisarContatosQueryRequestComDdd(int? dddRegiao) =>
            new Faker<PesquisarContatosQueryRequest>().CustomInstantiator(x => new PesquisarContatosQueryRequest()
            {
                DddRegiao = dddRegiao ?? x.PickRandom(listaDddValido)
            }).Generate();

        public IncluirContatoCommandRequest IncluirContatoCommandRequestValido =>
             new Faker<IncluirContatoCommandRequest>().CustomInstantiator(x => new IncluirContatoCommandRequest()
             {
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = $"{x.PickRandom(listaDddValido)}940028922"
             }).Generate();

        public IncluirContatoEventMessage IncluirContatoEventMessageValido =>
             new Faker<IncluirContatoEventMessage>().CustomInstantiator(x => new IncluirContatoEventMessage()
             {
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = $"{x.PickRandom(listaDddValido)}940028922"
             }).Generate();

        public AlterarContatoCommandRequest AlterarContatoCommandRequestValido =>
             new Faker<AlterarContatoCommandRequest>().CustomInstantiator(x => new AlterarContatoCommandRequest()
             {
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = $"{x.PickRandom(listaDddValido)}940028922"
             }).Generate();

        public AlterarContatoEventMessage AlterarContatoEventMessageValido =>
             new Faker<AlterarContatoEventMessage>().CustomInstantiator(x => new AlterarContatoEventMessage()
             {
                 IdContato = x.Random.Number(1, 999),
                 Email = x.Person.Email,
                 Nome = x.Person.FullName,
                 Telefone = $"{x.PickRandom(listaDddValido)}940028922"
             }).Generate();

        public DeletarContatoEventMessage DeletarContatoEventMessageValido =>
             new Faker<DeletarContatoEventMessage>().CustomInstantiator(x => new DeletarContatoEventMessage()
             {
                 IdContato = x.Random.Number(1, 999)
             }).Generate();

        internal static CommandResult CommandResult(Guid correlationId) =>
            new Faker<CommandResult>().CustomInstantiator(x => new CommandResult(
                correlationId
            )).Generate();

        public ContatoEntity ContatoEntity =>
             new Faker<ContatoEntity>().CustomInstantiator(x => new ContatoEntity(
                 nome: x.Person.FullName,
                 email: x.Person.Email,
                 telefoneCompleto: $"{x.PickRandom(listaDddValido)}940028922"
             )).Generate();

        public async Task<int> IncluirContato()
        {
            var entity = ContatoEntity;

            string scriptSql = @"
                INSERT INTO CONTATO
                (NMUSUARIO, NRDDD, NRTELEFONE, DSEMAIL, DHREGISTRO)
                    VALUES
                (@pr_nome, @pr_ddd, @pr_telefone, @pr_email, getdate());

                SELECT CAST(scope_identity() AS INT)
            ";

            int idContatoCadastrado = await _databaseFixture.Connection.ExecuteScalarAsync<int>(scriptSql, new
            {
                pr_nome = entity.Nome,
                pr_ddd = entity.DddTelefone,
                pr_telefone = entity.NumeroTelefone,
                pr_email = entity.Email
            });

            return idContatoCadastrado;
        }

        public async Task<PesquisarContatosQueryResult?> BuscarContato(int idContato)
        {
            string scriptSql = $@"
                SELECT ID Id,
                   NMUSUARIO Nome,
                   NRDDD + NRTELEFONE AS NumeroTelefone,
                   DSEMAIL Email
                FROM CONTATO
                WHERE ID = @pr_idcontato
            ";

            var queryResult = await _databaseFixture.Connection.QueryFirstOrDefaultAsync<PesquisarContatosQueryResult>(scriptSql, new
            {
                pr_idcontato = idContato
            });

            return queryResult;
        }
    }
}