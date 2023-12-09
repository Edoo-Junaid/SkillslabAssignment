using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/attachment")]
    public class AttachmentController : ApiController
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        [Route("getByEnrollmentID/{enrollmentId}")]
        public IHttpActionResult GetAllByEnrollmentId(int enrollmentId)
        {
            IEnumerable<AttachementDTO> attachments = _attachmentService.GetAllByEnrollmentId(enrollmentId);
            return Ok(attachments);
        }
    }
}
