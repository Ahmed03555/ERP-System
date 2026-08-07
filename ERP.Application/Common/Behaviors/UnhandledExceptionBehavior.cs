
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ERP.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(
        ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
        => _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) when (ex is not ValidationException) 
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "ERP Unhandled Exception for Request {Name}", requestName);
            throw;
        }
    }
}