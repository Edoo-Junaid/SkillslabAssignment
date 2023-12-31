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
            EnrollementRequestDTO enrollementRequest = await _enrollementService.ProcessMultipartContentAsync(multipartProvider);
            if (enrollementRequest is null) return BadRequest("Invalid request. Unable to process the enrollment data.");
            await _enrollementService.ProcessEnrollementAsync(enrollementRequest);
            return Ok();
        }
        [HttpGet]
        [Route("getByManagerID/{managerId}")]
        public async Task<IHttpActionResult> GetAllByManagerId(short managerId)
        {
            IEnumerable<EnrollementDTO> enrollements = await _enrollementService.GetAllByManagerIdAsync(managerId);
            return Ok(enrollements);
        }

        [HttpGet]
        [Route("runAutomaticProcessing")]
        public async Task<IHttpActionResult> RunAutomaticProcessing()
        {
            await _enrollementService.RunAutomaticProcessing();
            return Ok();
        }

        [HttpPut]
        [Route("approve/{enrollmentId:int}")]
        public async Task<IHttpActionResult> ApproveEnrollement(int enrollmentId)
        {
            if (await _enrollementService.ApproveEnrollementAsync(enrollmentId))
            {
                return Ok("Enrollment Declined Successfully");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("decline")]
        public async Task<IHttpActionResult> DeclineEnrollement([FromBody]DeclineEnrollmentRequestDTO declineEnrollmentRequestDTO)
        {
            if (await _enrollementService.DeclineEnrollementAsync(declineEnrollmentRequestDTO))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
