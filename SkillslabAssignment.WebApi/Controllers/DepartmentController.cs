using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Collections.Generic;
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
        public IEnumerable<Department> GetAll() => _departmentService.GetAll();

        // GET: api/Department/5
        public Department Get(byte id) => _departmentService.GetById(id);

        // POST: api/Department
        public IHttpActionResult Post([FromBody] Department department)
        {
            return Created(Request.RequestUri, _departmentService.Add(department));
        }

        // PUT: api/Department/5
        public void Put(byte id, [FromBody] Department department)
        {
            department.Id = id;
            _departmentService.Update(department);
        }

        // DELETE: api/Department/5
        public void Delete(byte id) => _departmentService.Delete(id);
    }
}
