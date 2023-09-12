using ProductApi.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductApi.Middlewares
{
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var exModel = new ExceptionResponse();

            switch (exception)
            {
                case FluentValidation.ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.responseCode = (int)HttpStatusCode.BadRequest;
                    exModel.responseMessage = ex.Errors?.FirstOrDefault()?.ErrorMessage ?? ex.Message;
                    break;
                case CustomException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.responseCode = ex.ErrorCode;
                    exModel.responseMessage = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exModel.responseCode = (int)HttpStatusCode.InternalServerError;
                    exModel.responseMessage = $"Internal Server Error. Please retry later. ({exception.Message})";
                    break;
            }

            var exResult = JsonSerializer.Serialize(exModel);
            await context.Response.WriteAsync(exResult);
        }
    }

    public class ExceptionResponse
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
    }
}
