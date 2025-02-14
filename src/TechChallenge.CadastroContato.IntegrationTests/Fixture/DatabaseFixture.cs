using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TechChallenge.CadastroContato.IntegrationTests.Fixture
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private SqlConnection _connection;

        public SqlConnection Connection => _connection ??= new SqlConnection(ObterConnectionString());

        public async Task InitializeAsync()
        {
            await Connection.OpenAsync();
            await InicializarBancoDeDados();
        }

        public async Task DisposeAsync()
        {
            await Connection.CloseAsync();
        }

        private static string ObterConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.IntegrationTests.json")
                .Build()
                .GetValue<string>("AppSettings:ConnectionString:SqlServer") ?? string.Empty;
        }

        private async Task InicializarBancoDeDados()
        {
            await ExcluirTabelas();
            await CriarTabelas();
            await PopularTabelas();
        }

        private async Task CriarTabelas()
        {
            await ExecutarScriptSql("sql/01_DDL.sql");
        }

        private async Task PopularTabelas()
        {
            await ExecutarScriptSql("sql/02_Seed.sql");
        }

        private async Task ExcluirTabelas()
        {
            await ExecutarScriptSql("sql/04_DropTables.sql");
        }

        private async Task ExecutarScriptSql(string caminhoArquivoSql)
        {
            try
            {
                if (Connection == null || Connection.State != ConnectionState.Open)
                {
                    return;
                }

                string caminhoDiretorioProjeto = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

                string caminhoCompleto = Path.Combine(caminhoDiretorioProjeto, caminhoArquivoSql);

                if (!File.Exists(caminhoCompleto))
                {
                    throw new FileNotFoundException($"O arquivo SQL não foi encontrado: {caminhoCompleto}");
                }

                var scriptSql = File.ReadAllText(caminhoCompleto);

                await Connection.ExecuteAsync(scriptSql);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao executar o script SQL: {caminhoArquivoSql}", ex);
            }
        }
    }
}