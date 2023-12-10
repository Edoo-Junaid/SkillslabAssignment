using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;

namespace SkillslabAssignment.Interface
{
    public interface IAccountService : IGenericService<Account, short>
    {
        LoginResponseDTO Authenticate(LoginRequestDTO loginDTO);
        bool IsEmailUnique(string email);
    }
}
