using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ValidationActionFilter]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        // GET: api/Department
        [Permission(Permissions.ViewDepartment)]
        public async Task<IEnumerable<Department>> GetAll() => await _departmentService.GetAllAsync();

        // GET: api/Department/5
        //[Permission(PermissionEnum.ViewDepartment)]
        public async Task<Department> Get(byte id) => await _departmentService.GetByIdAsync(id);

        // POST: api/Department
        [Permission(Permissions.ViewDepartment)]
        public async Task<IHttpActionResult> Post([FromBody] Department department)
        {
            return Created(Request.RequestUri, await _departmentService.AddAsync(department));
        }

        // PUT: api/Department/5
        [Permission(Permissions.EditDepartment)]
        public async Task Put(byte id, [FromBody] Department department)
        {
            department.Id = id;
            await _departmentService.UpdateAsync(department);
        }

        // DELETE: api/Department/5
        [Permission(Permissions.DeleteDepartment)]
        public async Task Delete(byte id) => await _departmentService.DeleteAsync(id);
    }
}
