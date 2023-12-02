using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<Role> roles = _roleService.GetAll();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        // GET: api/Role/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_roleService.GetById(id));
        }
        // POST: api/Role
        public void Post([FromBody] string value)
        {
        }
        // PUT: api/Role/5
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE: api/Role/5
        public void Delete(int id)
        {
        }
    }
}
