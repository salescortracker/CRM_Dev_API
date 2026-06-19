using System.Diagnostics;

namespace CRM_API.Middleware
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<
            ExecutionTimeMiddleware> _logger;

        public ExecutionTimeMiddleware(
            RequestDelegate next,
            ILogger<ExecutionTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(
            HttpContext context)
        {
            Stopwatch sw =
                Stopwatch.StartNew();

            await _next(context);

            sw.Stop();

            _logger.LogInformation(
                $"API {context.Request.Path} " +
                $"Executed in {sw.ElapsedMilliseconds} ms");
        }
    }
}
