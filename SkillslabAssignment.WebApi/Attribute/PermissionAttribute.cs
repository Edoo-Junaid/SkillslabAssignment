using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Unity;
using System.Web.Http.Filters;
using System.Threading;

namespace SkillslabAssignment.WebApi.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        private readonly string _permission;

        public PermissionAttribute(string permission)
        {
            _permission = permission;
        }
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var token = GetTokenFromHeader(actionContext.Request.Headers.Authorization);
            var principal = JwtManager.GetPrincipal(token);

            if (principal == null || !await HasPermissionAsync(principal, _permission))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            base.OnActionExecuting(actionContext);
        }
        private string GetTokenFromHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            if (authenticationHeaderValue != null && authenticationHeaderValue.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return authenticationHeaderValue.Parameter;
            }
            return null;
        }
        private async Task<bool> HasPermissionAsync(ClaimsPrincipal principal, string permission)
        {
            var permissionService = UnityConfig.Container.Resolve<IPermissionService>();
            var userId = principal.FindFirst("UserId").Value;
            return await permissionService.HasPermissionAsync(userId, permission);
        }
    }
}