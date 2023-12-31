using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/permission")]
    [ValidationActionFilter]
    public class PermissionController : ApiController
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        // GET: api/Permission
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _permissionService.GetAllAsync());
        }
        // GET: api/Permission/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(short permissionId)
        {
            return Ok(await _permissionService.GetByIdAsync(permissionId));
        }
        // POST: api/Permission
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Permission permission)
        {
            return CreatedAtRoute("DefaultApi", new { id = permission.Id }, await _permissionService.AddAsync(permission));
        }
        // PUT: api/Permission/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(short id, [FromBody] Permission permission)
        {
            return Ok(await _permissionService.UpdateAsync(permission));
        }

        // DELETE: api/Permission/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(short id)
        {
            return Ok(await _permissionService.DeleteAsync(id));
        }

        [HttpGet]
        [Route("user/{userId:int}")]
        public async Task<IHttpActionResult> GetAllByUserIdAsync(short userId)
        {
            return Ok(await _permissionService.GetAllByUserIdAsync(userId));
        }
    }
}
