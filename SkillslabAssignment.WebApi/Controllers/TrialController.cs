using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/trial")]
    public class TrialController : ApiController
    {
        [HttpGet]
        [Route("")]
        [AllowRole(RoleEnum.Admin)]
        public IHttpActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpGet]
        [Route("{username}/{role}")]
        [AllowAnonymous]
        public IHttpActionResult Get(string username, string role)
        {
            string token = JwtManager.GenerateToken(username, role);
            return Ok(token);
        }
    }
}
