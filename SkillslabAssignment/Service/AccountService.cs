using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Security.Authentication;
namespace SkillslabAssignment.Service
{
    public class AccountService : GenericService<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IGenericRepository<Role> roleRepository) : base(accountRepository)
        {
            this._accountRepository = accountRepository;
            this._userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public LoginResponseDTO Authenticate(LoginRequestDTO loginRequest)
        {
            Account account = _accountRepository.GetByEmail(loginRequest.Email);
            if (account != null && _accountRepository.IsAuthenticated(loginRequest.Email, loginRequest.Password))
            {
                User user = _userRepository.GetByAccountId(account.Id);
                Role role = _roleRepository.GetById(user.RoleId);
                return new LoginResponseDTO
                {
                    UserId = user.Id,
                    RoleName = role.Name,
                    Email = account.Email,
                };
            }
            else
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }
    }
}
