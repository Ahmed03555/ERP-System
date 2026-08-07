using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ERP.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = (await Task.WhenAll(_validators
                .Select(v => v.ValidateAsync(context, cancellationToken))))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (validationResults.Count != 0)
                {
                    throw new ValidationException(validationResults);
                }
            
            return await next();
        }
    }
}
