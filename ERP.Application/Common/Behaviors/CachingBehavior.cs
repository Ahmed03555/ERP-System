using ERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>, ICacheableQuery
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

        public CachingBehavior(ICacheService cacheService, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);

            if (cached is not null)
            {
                _logger.LogInformation("Cache HIT for key: {Key}", request.CacheKey);
                return cached;
            }

            _logger.LogInformation("Cache MISS for key: {Key}", request.CacheKey);

            var response = await next();

            await _cacheService.SetAsync(request.CacheKey, response, request.Expiration, cancellationToken);

            return response;
        }
    }
}
