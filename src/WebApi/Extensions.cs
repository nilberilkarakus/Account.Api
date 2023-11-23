using Domain.Exceptions;
using FluentValidation;
using GlobalExceptionHandler.WebApi;
using Newtonsoft.Json;

namespace WebApi
{
    public static class Extensions
    {
        public static void AddGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseGlobalExceptionHandler(config => {
                config.ContentType = "application/json";
                config.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    Message = "An unhandled error occurredsdfsg whilst processing your request"
                }));
                config.Map<ValidationException>().ToStatusCode(StatusCodes.Status400BadRequest)
                    .WithBody((ex, context) => JsonConvert.SerializeObject(
                        new
                        {
                            ValidationError = ex.Errors.Select(x => new {
                                x.PropertyName,
                                x.ErrorMessage
                            }).FirstOrDefault()
                        }));
                config.Map<Exception>().ToStatusCode(StatusCodes.Status500InternalServerError).WithBody((ex, context) => JsonConvert.SerializeObject(new
                {
                    ErrorMessage = "An error occurred while processing your request",
                    ErrorDescription = ex.Message
                }));
                config.Map<CustomerNotFoundException>().ToStatusCode(StatusCodes.Status404NotFound).WithBody((ex, context) => JsonConvert.SerializeObject(new
                {
                    ErrorMessage = "An error occurred while processing your request",
                    ErrorDescription = ex.Message
                }));
                config.Map<AccountNotFoundException>().ToStatusCode(StatusCodes.Status404NotFound).WithBody((ex, context) => JsonConvert.SerializeObject(new
                {
                    ErrorMessage = "An error occurred while processing your request",
                    ErrorDescription = ex.Message
                }));
                config.Map<TransactionNotFoundException>().ToStatusCode(StatusCodes.Status404NotFound).WithBody((ex, context) => JsonConvert.SerializeObject(new
                {
                    ErrorMessage = "An error occurred while processing your request",
                    ErrorDescription = ex.Message
                }));
            });
        }
    }
}
