using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SkillslabAssignment.WebApi.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var parameters = actionContext.ActionArguments.Values;
            foreach (var parameter in parameters)
            {
                if (parameter != null)
                {
                    Type parameterType = parameter.GetType();
                    Type validatorType = typeof(IValidatorService<>).MakeGenericType(parameterType);
                    dynamic validatorService = GlobalConfiguration.Configuration.DependencyResolver.GetService(validatorType);
                    if (validatorService != null)
                    {
                        IEnumerable<ValidationResult> validationResults = await validatorService.ValidateAsync(parameter);
                        if (validationResults != null && validationResults.Any())
                        {
                            var errorMessage = string.Join(", ", validationResults.Select(result => result.ErrorMessage));
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                            return;
                        }
                    }
                }
            }
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}