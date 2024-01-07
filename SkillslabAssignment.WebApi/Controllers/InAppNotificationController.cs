using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/inAppNotification")]
    [ValidationActionFilter]
    public class InAppNotificationController : ApiController
    {
        private readonly IInAppNotificationService _inAppNotificationService;

        public InAppNotificationController(IInAppNotificationService inAppNotificationService)
        {
            _inAppNotificationService = inAppNotificationService;
        }

        [HttpGet]
        [Route("")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetByUserId()
        {
            short userId = JwtManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);
            var notifications = await _inAppNotificationService.GetAllByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPut]
        [Route("markAsRead/{id:int}")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> MarkNotificationAsRead(int id)
        {
            if (await _inAppNotificationService.MarkNotificationAsRead(id))
            {
                return Ok("Notification marked as read successfully");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getUnReadCount")]
        [Permission(Permissions.Test)]
        public async Task<IHttpActionResult> GetUnReadCount()
        {
            short userId = JwtManager.GetUserIdFromToken(Request.Headers.Authorization.Parameter);
            int count = await _inAppNotificationService.GetUnReadCount(userId);
            return Ok(count);
        }
    }
}
