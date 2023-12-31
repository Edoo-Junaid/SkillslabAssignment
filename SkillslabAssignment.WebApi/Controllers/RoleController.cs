using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private readonly IGenericService<Role, byte> _roleService;
        public RoleController(IGenericService<Role, byte> roleService)
        {
            _roleService = roleService;
        }
        // GET: api/Role
        public async Task<IHttpActionResult> Get() => Ok(await _roleService.GetAllAsync());
        // GET: api/Role/5
        public async Task<IHttpActionResult> Get(byte id) => Ok(await _roleService.GetByIdAsync(id));
        // POST: api/Role
        public async Task<IHttpActionResult> Post([FromBody] Role role)
        {
            Role addedRole = await _roleService.AddAsync(role);
            return Created(Request.RequestUri + "/" +
                               addedRole.Id, addedRole);
        }
        // PUT: api/Role/5
        public async Task Put(int id, [FromBody] Role role) => await _roleService.UpdateAsync(role);
        // DELETE: api/Role/5
        public async Task Delete(byte id) => await _roleService.DeleteAsync(id);
    }
}
