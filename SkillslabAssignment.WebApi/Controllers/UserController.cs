using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Collections.Generic;
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
        public IHttpActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        // GET: api/user/5
        [HttpGet]
        [Route("{id:int}")]
        public User Get(int id) => _userService.GetById(id);

        // POST: api/user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] CreateUserDTO user)
        {
            bool isUserCreated = _userService.CreateUserAndAccount(user);
            if (isUserCreated)
            {
                return Ok();
            }
            return BadRequest("Failed to create user account.");
        }

        // PUT: api/user/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody] User user)
        {
            user.Id = id;
            _userService.Update(user);
        }

        // DELETE: api/user/5
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id) => _userService.Delete(id);

        // GET: api/user/getAllManagerByDepartment/5
        [HttpGet]
        [Route("getAllManagerByDepartment/{id:int}")]
        public IHttpActionResult GetManagerByDepartmentId(int id)
        {
            IEnumerable<ManagerDTO> managers = _userService.GetAllManagerByDepartment(id);
            return Ok(managers);
        }
    }
}
