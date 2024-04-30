using System.Net;
using System.Text.Json;
using TalabtG08.APIs.Errors;

namespace TalabtG08.APIs.Middelwares
{
    public class ExceptionMiddelwares
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelwares> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddelwares(RequestDelegate next,ILogger<ExceptionMiddelwares> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await next.Invoke(context);    


            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);

                context.Response.ContentType = "application/json";    
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var Response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                                  : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };   

                var json = JsonSerializer.Serialize(Response,options);

                await context.Response.WriteAsync(json);

            }


        }
            

    }
}
