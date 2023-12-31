using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        // GET: api/prerequisite
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Prerequisite>> Get() => await _prerequisiteService.GetAllAsync();

        // GET: api/prerequisite/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<Prerequisite> Get(short id) => await _prerequisiteService.GetByIdAsync(id);

        // POST: api/prerequisite
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Prerequisite prerequisite)
        {
            return Created(Request.RequestUri, await _prerequisiteService.AddAsync(prerequisite));
        }

        // PUT: api/prerequisite/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task Put(short id, [FromBody] Prerequisite prerequisite)
        {
            prerequisite.Id = id;
            await _prerequisiteService.UpdateAsync(prerequisite);
        }

        // DELETE: api/prerequisite/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(short id) => Ok(await _prerequisiteService.DeleteAsync(id));

        [HttpGet]
        [Route("getAllPrerequisiteByTrainingId/{id:int}")]
        public async Task<IHttpActionResult> GetAllPrerequisiteByTrainingId(short id) => Ok(await _prerequisiteService.GetAllByTrainingIdAsync(id));
    }
}
