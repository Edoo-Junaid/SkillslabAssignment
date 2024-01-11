using System.Collections.Generic;
using System;
using System.Web.Http;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Services.Description;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service;
using SkillslabAssignment.Common.Entities;
using System.Threading.Tasks;
using SkillslabAssignment.WebApi.App_Start;

namespace SkillslabAssignment.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            HangfireAspNet.Use(GetHangfireServers);

            //BackgroundJob.Schedule<IEnrollementService>(x => x.RunAutomaticProcessing(), TimeSpan.FromMinutes(1));
        }

        private IEnumerable<IDisposable> GetHangfireServers()
        {
            Hangfire.GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseActivator(new ContainerJobActivator(UnityConfig.Container))
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Server=localhost; Database=Hangfire; Integrated Security=True;");
            yield return new BackgroundJobServer();
        }
    }
}
