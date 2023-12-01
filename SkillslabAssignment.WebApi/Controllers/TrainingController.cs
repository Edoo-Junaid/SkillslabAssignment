using SkillslabAssignment.Common.Entities;
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
    [RoutePrefix("api/training")]
    public class TrainingController : ApiController
    {
        public readonly ITrainingService _trainingService;
        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }
        // GET: api/Training
        [HttpGet]
        [Route("")]
        public IEnumerable<Training> Get()
        {
            return _trainingService.GetAll();
        }

        // GET: api/Training/5
        [HttpGet]
        [Route("{id:int}")]
        public Training Get(int id)
        {
            return _trainingService.GetById(id);
        }

        // POST: api/Training
        [HttpPost]
        [Route("")]
        public void Post([FromBody] Training training)
        {
            _trainingService.Add(training);
        }

        // PUT: api/Training/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody] Training training)
        {
            training.Id = id;
            _trainingService.Update(training);
        }

        // DELETE: api/Training/5
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _trainingService.Delete(id);
        }
    }
}
