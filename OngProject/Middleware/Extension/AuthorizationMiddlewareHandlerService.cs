using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using OngProject.Middleware.Response;

namespace OngProject.Middleware.Extension
{
    public class AuthorizationMiddlewareHandlerService : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new AuthorizationMiddlewareResultHandler();        
        public async Task HandleAsync(RequestDelegate next, 
                                HttpContext context, 
                                AuthorizationPolicy policy, 
                                PolicyAuthorizationResult authorizeResult)
        {
            if(authorizeResult.Challenged)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new ErrorResponse()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Unauthorize: Access is Deneid due a invalid credentials."
                }.ToString());
                return;
            }
            if(authorizeResult.Forbidden)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new ErrorResponse ()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Forbidden: You don't have permission to access this resource."
                }.ToString());
                return;
            }
            await _defaultHandler.HandleAsync(next,context,policy,authorizeResult);
        }
    }
}
