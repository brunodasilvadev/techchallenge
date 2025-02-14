namespace TechChallenge.CadastroContato.Core.AppSettings
{
    public class ConsumerSettings
    {
        public required string RoutingKey { get; set; }
        public required string QueueName { get; set; }
    }
}