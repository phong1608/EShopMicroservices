using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest:notnull ,IRequest<TResponse>
        where TResponse:notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Start] Handle request={Request}-Response={Response}- RequestData={RequestData}"
                , typeof(TRequest).Name, typeof(TResponse).Name, request);
            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds>3)
            {
                _logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}"
                    , typeof(TRequest).Name, timeTaken.Seconds);
            }
            return response;
        }
    }
}
