using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
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
        public IHttpActionResult Get()
        {
            return Ok(_trainingService.GetAllTrainingDTO());
        }
        // GET: api/training/5
        [HttpGet]
        [Route("{id:int}")]
        public Training Get(int id)
        {
            return _trainingService.GetById(id);
        }
        // POST: api/training
        [HttpPost]
        [Route("")]
        public void Post([FromBody] Training training)
        {
            _trainingService.Add(training);
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
        public void Delete(int id)
        {
            _trainingService.Delete(id);
        }
        // GET: api/training/trainingDetails/5
        [HttpGet]
        [Route("trainingDetails/{id:int}")]
        public IHttpActionResult GetTrainingDetails(int id)
        {
            return Ok(_trainingService.GetTrainingDetails(id));
        }
    }
}
