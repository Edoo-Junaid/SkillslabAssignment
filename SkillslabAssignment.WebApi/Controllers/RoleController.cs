using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                return Created(Request.RequestUri, roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Role/5
        public string Get(int id)
        {
            return "value";
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
