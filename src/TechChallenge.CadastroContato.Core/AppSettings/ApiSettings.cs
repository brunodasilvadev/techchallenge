namespace TechChallenge.CadastroContato.Core.AppSettings
{
    public class ApiSettings
    {
        public required string ApplicationName { get; set; }
        public required ConnectionStringSettings ConnectionString { get; set; }
        public required MassTransitSettings MassTransit { get; set; }
    }
}