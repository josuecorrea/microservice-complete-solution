using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Log.Information($"Handling {typeof(TRequest).Name}");

            var response = await next();

            Log.Information($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }
}
