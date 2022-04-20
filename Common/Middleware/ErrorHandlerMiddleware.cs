using Common.Contract;
using Contract;
using System.Net;
using System.Text.Json;

namespace Common.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.OK;

                ApiResponse apiResponse = new ApiResponse();

                if (error is GlobalException)
                {
                    GlobalException exception = (GlobalException)error;
                    apiResponse.Code = exception.Code;
                    apiResponse.Message = exception.Message;
                } else
                {
                    apiResponse.Code = ResponseCode.InternalServerError;
                    apiResponse.Message = error.Message;
                }

                var result = JsonSerializer.Serialize(apiResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
