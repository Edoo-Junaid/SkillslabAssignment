using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SkillslabAssignment.WebApi.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AllowRoleAttribute : AuthorizeAttribute
    {
        private readonly List<RoleEnum> _roles;

        public AllowRoleAttribute(params RoleEnum[] roles)
        {
            _roles = roles.ToList();
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var token = GetTokenFromHeader(actionContext.Request.Headers.Authorization);
            var principal = JwtManager.GetPrincipal(token);
            if (principal != null)
            {
                foreach (var allowedRole in _roles)
                {
                    if (principal.IsInRole(allowedRole.ToString()))
                    {
                        return true;
                    }
                }
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