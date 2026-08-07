using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ERP.Application.Common.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer = new();
        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           _timer.Start();
            var response = next();
            _timer.Stop();

            var elapsed = _timer.ElapsedMilliseconds;
            if (elapsed > 500) {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsed, request);
            }
            
            return response;
        }
    }
}
