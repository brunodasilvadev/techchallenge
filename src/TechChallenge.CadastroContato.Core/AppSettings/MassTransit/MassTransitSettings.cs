namespace TechChallenge.CadastroContato.Core.AppSettings
{
    public class MassTransitSettings
    {
        public required string Host { get; set; }
        public required ushort Port { get; set; }
        public required string VirtualHost { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required RegistrationContactExchangeSettings RegistrationContactExchange { get; set; }
        public required InsertContactQueueSettings InsertContactQueue { get; set; }
        public required UpdateContactQueueSettings UpdateContactQueue { get; set; }
        public required DeleteContactQueueSettings DeleteContactQueue { get; set; }
    }
}