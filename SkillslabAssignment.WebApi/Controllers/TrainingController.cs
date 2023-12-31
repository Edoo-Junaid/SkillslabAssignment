using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Permission;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/training")]
    [ValidationActionFilter]
    public class TrainingController : ApiController
    {
        private readonly ITrainingService _trainingService;
        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }
        // GET: api/training
        [HttpGet]
        [Route("")]
        [Permission(Permissions.ViewTraining)]
        public async Task<IHttpActionResult> Get() => Ok(await _trainingService.GetAllAsync());
        // GET: api/training/5
        [HttpGet]
        [Route("{id:int}")]
        [Permission(Permissions.ViewTraining)]
        public async Task<Training> Get(short id) => await _trainingService.GetByIdAsync(id);

        // POST: api/training
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CreateTrainingRequestDTO training)
        {
            return Created(Request.RequestUri + "/" +
              training.Id, await _trainingService.CreteTrainingAsync(training));
        }
        // PUT: api/training/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(short id, [FromBody] TrainingDetailsDTO training)
        {

            if (await _trainingService.UpdateTrainingAndPrerequisiteAsync(training))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        // DELETE: api/training/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(short id)
        {
            if (await _trainingService.DeleteTrainingAndPrerequisiteAsync(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Cannot Delete. Users already Registered to training");
            }
        }
        // GET: api/training/trainingDetails/5
        [HttpGet]
        [Route("trainingDetails/{id:int}")]
        [Permission(Permissions.ViewTraining)]
        public async Task<IHttpActionResult> GetTrainingDetails(short id)
        {
            return Ok(await _trainingService.GetTrainingDetailsAsync(id));
        }
        // GET: api/training/getEnrolledTrainingIds/5
        [HttpGet]
        [Route("getEnrolledTrainingIds/{userId:int}")]
        public async Task<IHttpActionResult> GetAllEnrolledTrainingId(short userId)
        {
            return Ok(await _trainingService.GetAllEnrolledTrainingAsync(userId));
        }
    }
}
