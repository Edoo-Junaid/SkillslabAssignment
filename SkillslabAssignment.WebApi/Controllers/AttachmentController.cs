using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var attachments = _attachmentService.GetAllByEnrollmentId(enrollmentId);
            if (attachments == null)
            {
                return NotFound();
            }
            return Ok(attachments);
        }
    }
}
