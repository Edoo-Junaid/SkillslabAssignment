using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/webPage")]
    public class WebPageController : ApiController
    {
        private readonly IWebPageService _webPageService;
        public WebPageController(IWebPageService webPageService)
        {
            _webPageService = webPageService;
        }
        [HttpGet]
        [Route("getByUserId")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetByUserId()
        {
            short userId = JwtManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);
            return Ok(await _webPageService.GetAllByUserIdAsync(userId));
        }

        [HttpGet]
        [Route("")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetAll()
        {
            return Ok(await _webPageService.GetAllAsync());
        }
    }
}