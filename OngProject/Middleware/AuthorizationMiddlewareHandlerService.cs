using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using OngProject.Middleware.Response;

namespace OngProject.Middleware
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
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new 
                    ErrorResponse(Enum.ResponseCode.Unauthorize, "Unauthorize: Access is Deneid due a invalid credentials."));
                return;
            }
            if(authorizeResult.Forbidden)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new 
                    ErrorResponse(Enum.ResponseCode.Forbidden, "Forbidden: You don't have permission to access this resource."));
                return;
            }
            await _defaultHandler.HandleAsync(next,context,policy,authorizeResult);
        }
    }
}
