using SkillslabAssignment.Common.Logger;
using SkillslabAssignment.Interface;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;
using Unity;

namespace SkillslabAssignment.WebApi.App_Start
{
    public class GlobalExceptionhandler : ExceptionFilterAttribute
    {
        private ILogger _logger;
        public GlobalExceptionhandler()
        {
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            _logger = UnityConfig.Container.Resolve<ILogger>();
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
                _logger.LogAsync(context.Exception);
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred.");
            }
        }
    }
}