

using FluentValidation;
using MediatR;

namespace ZenBlog.Application.Behaviors
{
    // Validation is a middileware in mediatr pipline which performed before the handling
    /*
     example usage: 
    mediator.Send(command)
    → ValidationBehavior.Handle()   ← you are here
        → if invalid: throw, stop
        → if valid: call next()
            → CreateCategoryCommandHandler.Handle()
     */
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                   // var errorDetails = failures.GroupBy(x=>x.PropertyName).ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());
                    throw new ValidationException(failures);
                }
            }
            return await next(cancellationToken); // next => continue to the next behavior or handler
        }
    
    }
}
