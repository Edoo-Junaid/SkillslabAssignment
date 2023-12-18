using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Linq;
using System.Security.Authentication;
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
        public LoginResponseDTO Authenticate(LoginRequestDTO loginRequest)
        {
            Account account = _accountRepository.GetByEmail(loginRequest.Email);
            if (!IsValidCredentials(account, loginRequest))
            {
                return null;
            }
            User user = _userRepository.GetByAccountId(account.Id);
            Role role = _roleRepository.GetByUserId(user.Id).FirstOrDefault();
            return new LoginResponseDTO
            {
                UserId = user.Id,
                RoleName = role?.Name,
                Email = account.Email,
            };
        }
        public bool IsEmailUnique(string email) => _accountRepository.GetByEmail(email) == null;

        private bool IsValidCredentials(Account account, LoginRequestDTO loginRequest)
        {
            return account != null && _accountRepository.IsAuthenticated(loginRequest.Email, loginRequest.Password);
        }
    }
}
