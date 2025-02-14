using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using TechChallenge.CadastroContato.Contract.Exceptions;

namespace TechChallenge.CadastroContato.Api.Diagnostic
{
    internal sealed class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionHandler> _logger;

        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ValidationException badRequestException)
            {
                return false;
            }

            _logger.LogError(
                badRequestException,
                $"Um erro ocorreu: {badRequestException.Message}");

            var errors = badRequestException.Errors.Select(err => new ErrorProperty(err.PropertyName, err.ErrorMessage));

            var errorResponse = new ErrorResponse(StatusCodes.Status400BadRequest.ToString(), "Ocorreram erros de validação.", errors);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response
                .WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}