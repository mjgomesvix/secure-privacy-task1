using ApplicationLayer.Base.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Support.ExceptionsManagement.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Base.Middlewares
{
    internal class RequestExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                static Exception last(Exception e) => e.InnerException == null ? e : last(e.InnerException);
                await HandleExceptionAsync(context, last(ex)).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var error = new ErrorResponse(context.Response.StatusCode);

            if (!(ex is ExceptionsWrapper))
            {
                error.Add(ex.Message);
            }
            else
            {
                var exceptionsWrapper = ex as ExceptionsWrapper;

                if (ex != null) error.Add(exceptionsWrapper.Messages);
            }

            var result = JsonConvert.SerializeObject(error);

            return context.Response.WriteAsync(result);
        }
    }
}
