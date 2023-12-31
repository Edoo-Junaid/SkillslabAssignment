using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Threading.Tasks;
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
        [Permission(Permissions.ViewPendingAccount)]
        public async Task<IHttpActionResult> Get() => Ok(await _pendingAccountService.GetAllPendingAccountDTOsAsync());

        // GET: api/pendingAccount/5
        [HttpGet]
        [Route("{id:int}")]
        [Permission(Permissions.ViewPendingAccount)]
        public Task<PendingAccount> Get(short id) => _pendingAccountService.GetByIdAsync(id);

        // POST: api/pendingAccount
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] PendingAccount account)
        {
            bool IsCreated = await _pendingAccountService.CreatePendingAccountAsync(account);
            if (IsCreated)
            {
                return Created(Request.RequestUri, account);
            }
            else
            {
                return BadRequest("Something went wrong. Please try again later");
            }
        }

        // PUT: api/pendingAccount/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task Put(short id, [FromBody] PendingAccount account)
        {
            account.Id = id;
            await _pendingAccountService.UpdateAsync(account);
        }

        // DELETE: api/pendingAccount/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task Delete(short id) => await _pendingAccountService.DeleteAsync(id);
    }
}
