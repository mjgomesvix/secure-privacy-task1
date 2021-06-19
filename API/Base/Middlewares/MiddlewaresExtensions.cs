using Microsoft.AspNetCore.Builder;

namespace API.Base.Middlewares
{
    public static class MiddlewaresExtensions
    {
        public static IApplicationBuilder UseRequestExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestExceptionHandlingMiddleware>();
        }

        //Implement from here others UseRequest middlewares...
    }
}
