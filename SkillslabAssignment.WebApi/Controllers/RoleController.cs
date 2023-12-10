using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
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
        public IHttpActionResult Get() => Ok(_roleService.GetAll());
        // GET: api/Role/5
        public IHttpActionResult Get(byte id) => Ok(_roleService.GetById(id));
        // POST: api/Role
        public void Post([FromBody] string value)
        {
        }
        // PUT: api/Role/5
        public void Put(int id, [FromBody] Role role) => _roleService.Update(role);
        // DELETE: api/Role/5
        public void Delete(byte id) => _roleService.Delete(id);
    }
}
