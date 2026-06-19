using Serilog;

namespace CRM_API.Middleware
{
    //public class RequestResponseMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger<
    //    RequestResponseMiddleware> _logger;

    //    public RequestResponseMiddleware(
    //        RequestDelegate next,
    //        ILogger<RequestResponseMiddleware>
    //        logger)
    //    {
    //        _next = next;
    //        _logger = logger;
    //    }

    //    public async Task Invoke(
    //        HttpContext context)
    //    {
    //        _logger.LogInformation(
    //     $"Request Path: {context.Request.Path}"
    // );

    //        await _next(context);

    //        _logger.LogInformation(
    //            $"Response Status: {context.Response.StatusCode}"
    //        );
    //    }
    //}
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information(
                "Request => {Method} {Url}",
                context.Request.Method,
                context.Request.Path);

            await _next(context);

            Log.Information(
                "Response => {StatusCode}",
                context.Response.StatusCode);
        }
    }
}
