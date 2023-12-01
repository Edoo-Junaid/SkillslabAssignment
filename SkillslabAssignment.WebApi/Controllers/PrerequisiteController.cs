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
    [RoutePrefix("api/prerequisite")]
    public class PrerequisiteController : ApiController
    {
        private readonly IPrerequisiteService _prerequisiteService;
        public PrerequisiteController(IPrerequisiteService prerequisiteService)
        {
            _prerequisiteService = prerequisiteService;
        }

        // GET: api/PreRequisite
        [HttpGet]
        [Route("")]
        public IEnumerable<Prerequisite> Get()
        {
            return _prerequisiteService.GetAll();
        }

        // GET: api/PreRequisite/5
        [HttpGet]
        [Route("{id:int}")]
        public Prerequisite Get(int id)
        {
            return _prerequisiteService.GetById(id);
        }

        // POST: api/PreRequisite
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Prerequisite prerequisite)
        {
            try
            {
                _prerequisiteService.Add(prerequisite);
                return Created(Request.RequestUri + "/" + prerequisite.Id, prerequisite);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/PreRequisite/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody] Prerequisite prerequisite)
        {
            prerequisite.Id = id;
            _prerequisiteService.Update(prerequisite);
        }

        // DELETE: api/PreRequisite/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _prerequisiteService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("getAllPrerequisiteByTrainingId/{id:int}")]
        public IHttpActionResult GetAllPrerequisiteByTrainingId(int id)
        {
            try
            {
                IEnumerable<Prerequisite> prerequisites = _prerequisiteService.GetAllByTrainingId(id);
                return Ok(prerequisites);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
