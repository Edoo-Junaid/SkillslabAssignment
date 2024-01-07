using SkillslabAssignment.Interface;
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
    [RoutePrefix("api/webpage")]
    public class WebPageElementController : ApiController
    {
        private readonly IWebPageElementService _webPageElementService;

        public WebPageElementController(IWebPageElementService webPageElementService)
        {
            _webPageElementService = webPageElementService;
        }

        [HttpGet]
        [Route("getByUserIdAndWebPageId/{webPageId:int}")]
        public Task<IHttpActionResult> GetAllByUserIdAndWebPageIdAsync(int webPageId)
        {
            throw new NotImplementedException();
        }

    }
}