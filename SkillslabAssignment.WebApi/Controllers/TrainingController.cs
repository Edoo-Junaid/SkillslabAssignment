using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Interface;
using SkillslabAssignment.WebApi.Attribute;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/training")]
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
        [AllowRole(RoleEnum.Employee)]
        public IHttpActionResult Get() => Ok(_trainingService.GetAll());

        // GET: api/training/5
        [HttpGet]
        [Route("{id:int}")]
        public Training Get(int id) => _trainingService.GetById(id);

        // POST: api/training
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] CreateTrainingRequestDTO training)
        {
            return Created(Request.RequestUri + "/" + training.Id, _trainingService.CreteTraining(training));
        }

        // PUT: api/training/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody] Training training)
        {
            training.Id = id;
            _trainingService.Update(training);
        }

        // DELETE: api/training/5
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id) => _trainingService.Delete(id);

        // GET: api/training/trainingDetails/5
        [HttpGet]
        [Route("trainingDetails/{id:int}")]
        public IHttpActionResult GetTrainingDetails(int id) => Ok(_trainingService.GetTrainingDetails(id));

        // GET: api/training/getEnrolledTrainingIds/5
        [HttpGet]
        [Route("getEnrolledTrainingIds/{userId:int}")]
        public IHttpActionResult GetAllEnrolledTrainingId(int userId) => Ok(_trainingService.GetAllEnrolledTraining(userId));
    }
}
