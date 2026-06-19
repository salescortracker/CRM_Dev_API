using Serilog;
using Shared;
using Shared.CommonModels;
namespace CRM_API.Middleware
{
    //public class ExceptionMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    public ExceptionMiddleware(
    //        RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public async Task InvokeAsync(
    //        HttpContext context)
    //    {
    //        try
    //        {
    //            await _next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //            context.Response.StatusCode = 500;
    //            context.Response.ContentType =
    //            "application/json";

    //            var response = new ApiResponse<string>
    //            {
    //                Success = false,
    //                Message = ex.Message
    //            };

    //            await context.Response
    //                .WriteAsJsonAsync(response);
    //        }
    //    }
    //}

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Unhandled Exception");

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync(
                    "Internal Server Error");
            }
        }
    }
}
