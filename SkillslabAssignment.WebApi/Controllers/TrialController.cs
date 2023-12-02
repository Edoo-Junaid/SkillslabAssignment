using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Service;
using SkillslabAssignment.WebApi.App_Start;
using SkillslabAssignment.WebApi.Attribute;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/trial")]
    public class TrialController : ApiController
    {
        [HttpGet]
        [Route("")]
        [AllowRole(RoleEnum.Admin)]
        public IHttpActionResult Get()
        {
            return Ok("Hello World");
        }
        [HttpGet]
        [Route("{username}/{role}")]
        [AllowAnonymous]
        public IHttpActionResult Get(string username, string role)
        {
            string token = JwtManager.GenerateToken(username, role);
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("upload/image")]
        public async Task<IHttpActionResult> UploadImage()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                var fileData = provider.Contents.FirstOrDefault() ?? throw new HttpResponseException(HttpStatusCode.BadRequest);
                var fileName = fileData.Headers.ContentDisposition.FileName.Trim('\"');
                var fileStream = await fileData.ReadAsStreamAsync();
                FirebaseStorageService storageService = new FirebaseStorageService();
                string downloadUrl = await storageService.SaveImageAsync(fileStream, fileName);
                return Ok(downloadUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error uploading image: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}
