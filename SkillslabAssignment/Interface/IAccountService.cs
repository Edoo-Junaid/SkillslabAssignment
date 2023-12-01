using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SkillslabAssignment.Interface
{
    public interface IAccountService : IGenericService<Account>
    {
        LoginResponseDTO Authenticate(LoginRequestDTO loginDTO);
    }
}
