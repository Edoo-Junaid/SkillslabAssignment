using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IAccountService : IGenericService<Account, short>
    {
        Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO loginDTO);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
