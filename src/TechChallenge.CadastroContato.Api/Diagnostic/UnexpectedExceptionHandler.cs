using Microsoft.AspNetCore.Diagnostics;
using TechChallenge.CadastroContato.Contract.Exceptions;

namespace TechChallenge.CadastroContato.Api.Diagnostic
{
    internal sealed class UnexpectedExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<UnexpectedExceptionHandler> _logger;

        public UnexpectedExceptionHandler(ILogger<UnexpectedExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception,
                $"Um erro ocorreu inesperado: {exception.Message}");

            var errorResponse = new ErrorResponse(StatusCodes.Status500InternalServerError.ToString(), exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}