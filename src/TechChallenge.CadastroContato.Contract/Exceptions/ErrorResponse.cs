namespace TechChallenge.CadastroContato.Contract.Exceptions
{
    public sealed class ErrorResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public IEnumerable<ErrorProperty>? Details { get; }

        public ErrorResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public ErrorResponse(string code, string message, IEnumerable<ErrorProperty> details) : this(code, message)
        {
            Details = details;
        }
    }
}