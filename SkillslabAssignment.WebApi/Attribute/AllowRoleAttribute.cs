using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.WebApi.App_Start;
using System;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SkillslabAssignment.WebApi.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AllowRoleAttribute : AuthorizeAttribute
    {
        private readonly RoleEnum _role;
        public AllowRoleAttribute(RoleEnum role)
        {
            _role = role;
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var token = GetTokenFromHeader(actionContext.Request.Headers.Authorization);
            var principal = JwtManager.GetPrincipal(token);
            if (principal != null && principal.IsInRole(_role.ToString()))
            {
                return true;
            }
            return false;
        }

        private string GetTokenFromHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            if (authenticationHeaderValue != null && authenticationHeaderValue.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return authenticationHeaderValue.Parameter;
            }
            return null;
        }
    }
}