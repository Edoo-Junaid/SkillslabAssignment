using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/enrollement")]
    public class EnrollementController : ApiController
    {
        private readonly IEnrollementService _enrollementService;
        public EnrollementController(IEnrollementService enrollementService)
        {
            _enrollementService = enrollementService;
        }
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Enroll()
        {
            if (!Request.Content.IsMimeMultipartContent()) return BadRequest("Invalid request. Expecting a multipart/form-data request.");

            MultipartMemoryStreamProvider multipartProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(multipartProvider);
            EnrollementRequestDTO enrollementRequest = await _enrollementService.ProcessMultipartContent(multipartProvider);

            if (enrollementRequest is null) return BadRequest("Invalid request. Unable to process the enrollment data.");

            await _enrollementService.ProcessEnrollement(enrollementRequest);
            return Ok();
        }
        [HttpGet]
        [Route("getByManagerID/{managerId}")]
        public IHttpActionResult GetAllByManagerId(int managerId)
        {
            IEnumerable<EnrollementDTO> enrollements = _enrollementService.GetAllByManagerId(managerId);
            return Ok(enrollements);
        }
    }
}
