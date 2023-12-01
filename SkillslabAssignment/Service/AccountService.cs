using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
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

        public LoginResponseDTO Authenticate(LoginRequestDTO loginDTO)
        {
            Account account = _accountRepository.GetByEmail(loginDTO.Email);

            if (account != null && _accountRepository.IsAuthenticated(loginDTO.Email, loginDTO.Password))
            {
                User user = _userRepository.GetByAccountId(account.Id);
                Role role = _roleRepository.GetById(user.RoleId);

                if (role != null)
                {
                    return new LoginResponseDTO
                    {
                        UserId = user.Id,
                        RoleName = role.Name,
                    };
                }
                else
                {
                    throw new Exception("Role not found for the user.");
                }
            }
            else
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }

    }
}
