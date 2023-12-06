using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Validatora;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.App_Start;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                ParameterValidator<LoginRequestDTO>.TryValidateAndThrow(loginRequest);
                LoginResponseDTO loginResponseDTO = _accountService.Authenticate(loginRequest);
                loginResponseDTO.AuthToken = JwtManager.GenerateToken(loginResponseDTO.Email, loginResponseDTO.RoleName);
                return Ok(loginResponseDTO);
            }
            catch (AuthenticationException ex)
            {
                Debug.WriteLine(ex.Message);
                return Unauthorized();
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return InternalServerError(ex);
            }
        }
    }

}
