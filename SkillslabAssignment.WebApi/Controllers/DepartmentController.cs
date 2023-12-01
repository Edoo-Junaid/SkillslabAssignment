using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        // GET: api/Department
        public IEnumerable<Department> GetAll()
        {
            return _departmentService.GetAll();
        }
        // GET: api/Department/5
        public Department Get(int id)
        {
            try
            {
                return _departmentService.GetById(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        // POST: api/Department
        public Department Post([FromBody] Department department)
        {
            _departmentService.Add(department);
            return department;
        }
        // PUT: api/Department/5
        public void Put(int id, [FromBody] Department department)
        {
            department.Id = id;
            _departmentService.Update(department);
        }
        // DELETE: api/Department/5
        public void Delete(int id)
        {
            _departmentService.Delete(id);
        }
    }
}
