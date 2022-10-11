using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Stone_Desafio.Businesss;
using System.Net;

namespace Stone_Desafio.Configuration
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HandleExceptionFilter> logger;
        
        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger)
        {
            this.logger = logger;
        }


        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException apiException)
            {
                
                logger.LogWarning(apiException, "An API exception was caught");

                context.Result = new JsonResult(apiException.Message, JsonConvert.DefaultSettings)
                {
                    StatusCode = (int) HttpStatusCode.UnprocessableEntity,
                };

                context.ExceptionHandled = true;

            }
            else
            {
                var codigo = new Random().Next().ToString();
                
                logger.LogError(context.Exception, "Unhandled exception -- code: { ExceptionCode }", codigo);
                
                var erro =
                    new
                    {
                        Message = $"Um erro inesperado aconteceu. Codigo {codigo}",
                        ExceptionCode = codigo,
                    };

                context.Result = new JsonResult(erro, JsonConvert.DefaultSettings)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}