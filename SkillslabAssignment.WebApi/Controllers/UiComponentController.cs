using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [RoutePrefix("api/webPageElement")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UiComponentController : ApiController
    {
        private readonly IUiComponentService _uiComponentService;
        public UiComponentController(IUiComponentService uiComponentService)
        {
            _uiComponentService = uiComponentService;
        }
        [HttpGet]
        [Route("getByUserIdAndWebPageName/{webPageName}")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetAllByUserIdAndWebPageIdAsync(string webPageName)
        {
            short userId = JwtManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);
            return Ok(await _uiComponentService.GetAllByUserIdAndWebPageNameAsync(userId, webPageName));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _uiComponentService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("getAllByWebPageId/{id:int}")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetAllByWebPageId(int id)
        {
            return Ok(await _uiComponentService.GetAllByWebPageIdAsync(id));
        }
    }
}
