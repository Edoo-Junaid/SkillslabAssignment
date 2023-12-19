using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    [ValidationActionFilter]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/user
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _userService.GetAllAsync());
        }

        // GET: api/user/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<User> Get(short id) => await _userService.GetByIdAsync(id);

        // POST: api/user
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CreateUserDTO user)
        {
            bool isUserCreated = await _userService.CreateUserAndAccountAsync(user);
            if (isUserCreated)
            {
                return Ok();
            }
            return BadRequest("Failed to create user account.");
        }

        // PUT: api/user/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task Put(short id, [FromBody] User user)
        {
            user.Id = id;
            await _userService.UpdateAsync(user);
        }

        // DELETE: api/user/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task Delete(short id) => await _userService.DeleteAsync(id);

        // GET: api/user/getAllManagerByDepartment/5
        [HttpGet]
        [Route("getAllManagerByDepartment/{id:int}")]
        public async Task<IHttpActionResult> GetManagerByDepartmentId(byte id)
        {
            IEnumerable<ManagerDTO> managers = await _userService.GetAllManagerByDepartmentAsync(id);
            return Ok(managers);
        }
    }
}
