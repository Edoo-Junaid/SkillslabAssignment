using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
namespace SkillslabAssignment.Service
{
    public class AccountService : GenericService<Account, short>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IRoleRepository roleRepository) : base(accountRepository)
        {
            this._accountRepository = accountRepository;
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }
        public async Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO loginRequest)
        {
            Account account = await _accountRepository.GetByEmailAsync(loginRequest.Email);
            if (!IsValidCredentials(account, loginRequest))
            {
                return null;
            }
            User user = await _userRepository.GetByAccountIdAsync(account.Id);
            Role role = _roleRepository.GetByUserIdAsync(user.Id).FirstOrDefault();
            return new LoginResponseDTO
            {
                UserId = user.Id,
                RoleName = role?.Name,
                Email = account.Email,
            };
        }
        public async Task<bool> IsEmailUniqueAsync(string email) => await _accountRepository.GetByEmailAsync(email) == null;

        private bool IsValidCredentials(Account account, LoginRequestDTO loginRequest)
        {
            return account != null && _accountRepository.IsAuthenticated(loginRequest.Email, loginRequest.Password);
        }
    }
}
