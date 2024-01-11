﻿using SkillslabAssignment.Common.Permission;
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


namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/webPageElementOld")]
    public class WebPageElementController : ApiController
    {
        private readonly IWebPageElementService _webPageElementService;
        public WebPageElementController(IWebPageElementService webPageElementService)
        {
            _webPageElementService = webPageElementService;
        }
        [HttpGet]
        [Route("getByUserIdAndWebPageName/{webPageName}")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetAllByUserIdAndWebPageIdAsync(string webPageName)
        {
            short userId = JwtManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);
            return Ok(await _webPageElementService.GetAllByUserIdAndWebPageNameAsync(userId, webPageName));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _webPageElementService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("getAllByWebPageId/{id:int}")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetAllByWebPageId(int id)
        {
            return Ok(await _webPageElementService.GetAllByWebPageIdAsync(id));
        }
    }
}