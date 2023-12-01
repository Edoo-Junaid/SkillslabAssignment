using SkillslabAssigment.DAL.DAL;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SkillslabAssignment.Interface;
using System.Web.Http.Cors;
using System.Security.Principal;
using SkillslabAssignment.Common.Validatora;
using System.ComponentModel.DataAnnotations;

namespace SkillslabAssignment.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/pendingAccount")]
    public class PendingAccountController : ApiController
    {
        private readonly IPendingAccountService _pendingAccountService;
        public PendingAccountController(IPendingAccountService pendingAccountService)
        {
            _pendingAccountService = pendingAccountService;
        }
        // GET: api/pendingAccount
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_pendingAccountService.GetAllPendingAccountDTOs());
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return InternalServerError(ex);
            }
        }
        // GET: api/pendingAccount/5
        [HttpGet]
        [Route("{id:int}")]
        public PendingAccount Get(int id)
        {
            return _pendingAccountService.GetById(id);
        }
        // POST: api/pendingAccount
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] PendingAccount account)
        {
            try
            {
                ParameterValidator<PendingAccount>.TryValidateAndThrow(account);
                return Created(Request.RequestUri, _pendingAccountService.Add(account));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        // PUT: api/pendingAccount/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody] PendingAccount account)
        {
            account.Id = id;
            _pendingAccountService.Update(account);
        }
        // DELETE: api/pendingAccount/5
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _pendingAccountService.Delete(id);
        }
    }
}
