using SkillslabAssigment.DAL.Common;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Service;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/trial")]
    public class TrialController : ApiController
    {
        private readonly DbConnection _connection;
        public TrialController(DbConnection dbConnection)
        {
            _connection = dbConnection;
        }


        [HttpGet]
        [Route("")]
        [AllowRole(RoleEnum.Admin, RoleEnum.Manager)]
        public IHttpActionResult Get()
        {
            return Ok("Hello World");
        }
        [HttpGet]
        [Route("{username}/{role}/{userId}")]
        [AllowAnonymous]
        public IHttpActionResult Get(string username, string role, string userId)
        {
            string token = JwtManager.GenerateToken(username, userId, role);
            return Ok(token);
        }

        [HttpGet]
        [Route("test")]
        [Permission(Permissions.ViewPendingAccount)]
        public IHttpActionResult Test()
        {
            return Ok("Hello World");
        }

        [HttpGet]
        [Route("test2")]
        public async Task<IHttpActionResult> Test2()
        {
            int zero = 0;
            int result = 1 / zero;
            await _connection.ExecuteStoredProcedureAsync("DeleteAndAuditTrial", new { userLoggedIn = 2, trial_id = 2 });

            return Ok("Hello World");
        }
    }
}
