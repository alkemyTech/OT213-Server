using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OngProject.Middleware
{
    public class AccessDeniedMiddleware
    {
        private readonly RequestDelegate _next;
        public AccessDeniedMiddleware(RequestDelegate next)
        {
            this._next = next;   
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path.Value.Contains("Download/Image"))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Request.ContentType = "text/plain";
                
                await httpContext.Response.WriteAsJsonAsync("Classified Information. You can't access here.");
            }
            else 
            {
                await _next(httpContext);  
            }
        }
    }

}

