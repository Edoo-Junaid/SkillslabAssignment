using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private readonly IGenericService<Role> _roleService;
        public RoleController(IGenericService<Role> roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role
        public IHttpActionResult Get() => Ok(_roleService.GetAll());

        // GET: api/Role/5
        public IHttpActionResult Get(int id) => Ok(_roleService.GetById(id));
        // POST: api/Role
        public void Post([FromBody] string value)
        {
        }
        // PUT: api/Role/5
        public void Put(int id, [FromBody] Role role) => _roleService.Update(role);
        // DELETE: api/Role/5
        public void Delete(int id) => _roleService.Delete(id);
    }
}
