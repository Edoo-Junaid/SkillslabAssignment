using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service;
using SkillslabAssignment.WebApi.Attribute;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
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

        [HttpPost]
        [Route("api/role/createRole")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> CreateRole([FromBody] RolePagePermissionDTO rolePagePermission)
        {
            Role role = await _roleService.CreateRoleWithPageAndPageElementPermissionAsync(rolePagePermission);
            return Created(Request.RequestUri + "/" +
                                              role.Id, role);
        }
        [HttpGet]
        [Route("api/role/getAllWithPermissionDetails")]
        public async Task<IHttpActionResult> GetAllWithPermissionDetails()
        {
            return Ok(await _roleService.GetAllRolePagePermission());
        }
        [HttpGet]
        [Route("api/role/getAllWithPermissionDetailsByRoleId/{roleId:int}")]
        public async Task<IHttpActionResult> GetAllWithPermissionDetailsByRoleId(int roleId)
        {
            return Ok((await _roleService.GetAllRolePagePermission()).Where(dto => dto.RoleId == roleId).FirstOrDefault().WebPageElementPermissionDTO);
        }
    }

}
