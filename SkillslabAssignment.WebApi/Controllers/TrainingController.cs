using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
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
        public async Task<IHttpActionResult> Get() => Ok(await _trainingService.GetAllAsync());

        // GET: api/training/5
        [HttpGet]
        [Route("{id:int}")]
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
        public async Task Put(short id, [FromBody] Training training)
        {
            training.Id = id;
            await _trainingService.UpdateAsync(training);
        }

        // DELETE: api/training/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task Delete(short id) => await _trainingService.DeleteAsync(id);

        // GET: api/training/trainingDetails/5
        [HttpGet]
        [Route("trainingDetails/{id:int}")]
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
