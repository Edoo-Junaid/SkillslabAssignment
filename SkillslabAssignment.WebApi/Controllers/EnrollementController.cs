using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Invalid request. Expecting a multipart/form-data request.");
            }
            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                EnrollementRequestDTO enrollementRequest = await _enrollementService.ProcessMultipartContent(provider);
                if (enrollementRequest != null)
                {
                    await _enrollementService.ProcessEnrollement(enrollementRequest);
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid request. Unable to process the enrollment data.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error enrolling: {ex.Message}");
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("getByManagerID/{managerId}")]
        public IHttpActionResult GetAllByManagerId(int managerId)
        {
            try
            {
                IEnumerable<EnrollementDTO> enrollements = _enrollementService.GetAllByManagerId(managerId);
                return Ok(enrollements);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting enrollements: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}
