namespace TechChallenge.CadastroContato.Core.AppSettings
{
    public class AmqpSettings
    {
        public required string ApplicationName { get; set; }
        public required ConnectionStringSettings ConnectionString { get; set; }
        public required MassTransitSettings MassTransit { get; set; }
    }
}