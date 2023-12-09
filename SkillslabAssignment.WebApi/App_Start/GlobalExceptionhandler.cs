using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace SkillslabAssignment.WebApi.App_Start
{
    public class GlobalExceptionhandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception.Message);
            }
            else if (context.Exception is AuthenticationException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, context.Exception.Message);
            }
            else
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
        }
    }
}