using System;
using System.Threading.Tasks;
using Api.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Util.Ex;
using Utilidades;

namespace Api.Common.MiddleException
{
    public class MiddleHandlerException
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MiddleHandlerException> _logger;

        public MiddleHandlerException(RequestDelegate next, ILogger<MiddleHandlerException> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionResponseApi(context, ex);
            }
        }

        protected Task ExceptionResponseApi(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = Constantes.ContentType;
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ResponseApi<object> {Estado=false,
                                                    Datos= $"{exception?.Message} --inner-- {exception?.InnerException?.ToString() ?? string.Empty}",
                                                    Mensaje= Constantes.MessageFail};
            if(exception.GetType()== typeof(DomainException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var ex = (DomainException)exception;
                response = new ResponseApi<object> { Estado = false, Datos = new object(), Mensaje = ex.Message };
            }

            return  httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
