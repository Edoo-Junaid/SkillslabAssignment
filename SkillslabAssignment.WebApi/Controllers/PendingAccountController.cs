using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/pendingAccount")]
    [ValidationActionFilter]
    public class PendingAccountController : ApiController
    {
        private readonly IPendingAccountService _pendingAccountService;
        public PendingAccountController(IPendingAccountService pendingAccountService)
        {
            _pendingAccountService = pendingAccountService;
        }

        // GET: api/pendingAccount
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get() => Ok(_pendingAccountService.GetAllPendingAccountDTOs());

        // GET: api/pendingAccount/5
        [HttpGet]
        [Route("{id:int}")]
        public PendingAccount Get(int id) => _pendingAccountService.GetById(id);

        // POST: api/pendingAccount
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] PendingAccount account)
        {
            return Created(Request.RequestUri, _pendingAccountService.Add(account));
        }

        // PUT: api/pendingAccount/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody] PendingAccount account)
        {
            account.Id = id;
            _pendingAccountService.Update(account);
        }

        // DELETE: api/pendingAccount/5
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id) => _pendingAccountService.Delete(id);
    }
}
