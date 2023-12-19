using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/account")]
    [ValidationActionFilter]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IHttpActionResult> Authenticate([FromBody] LoginRequestDTO loginRequest)
        {
            LoginResponseDTO loginResponseDTO = await _accountService.AuthenticateAsync(loginRequest);
            if (loginResponseDTO == null)
            {
                return Unauthorized();
            }
            loginResponseDTO.AuthToken = JwtManager.GenerateToken(loginResponseDTO.Email, loginResponseDTO.RoleName);
            return Ok(loginResponseDTO);
        }
    }
}
