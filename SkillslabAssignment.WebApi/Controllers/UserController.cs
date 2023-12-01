using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Validatora;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
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
        public IEnumerable<User> Get()
        {
            return _userService.GetAll();
        }

        // GET: api/user/5
        [HttpGet]
        [Route("{id:int}")]
        public User Get(int id)
        {
            return _userService.GetById(id);
        }

        // POST: api/user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] CreateUserDTO user)
        {
            try
            {
                ParameterValidator<CreateUserDTO>.TryValidateAndThrow(user);
                _userService.CreateUserAndAccount(user);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

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
        public void Delete(int id)
        {
            _userService.Delete(id);
        }

        // Custom action with a different route
        [HttpGet]
        [Route("getAllManagerByDepartment/{id:int}")]
        public IHttpActionResult getManagerByDepartmentId(int id)
        {
            try
            {
                IEnumerable<ManagerDTO> managers = _userService.GetAllManagerByDepartment(id);
                return Created(Request.RequestUri, managers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
