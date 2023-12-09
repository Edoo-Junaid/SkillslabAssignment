using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SkillslabAssignment.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<PendingAccount> _pendingAccountRepository;
        public UserService(IUserRepository repository, IGenericRepository<PendingAccount> pendingAccountRepository) : base(repository)
        {
            _userRepository = repository;
            _pendingAccountRepository = pendingAccountRepository;
        }
        public bool CreateUserAndAccount(CreateUserDTO createUserDTO)
        {
            PendingAccount pendingAccount = _pendingAccountRepository.GetById((int)createUserDTO.PendingAccountId);
            if (pendingAccount is null) return false;
            UserDto user = pendingAccount.ToUserDto(createUserDTO);
            return _userRepository.CreateUser(user);
        }
        public IEnumerable<ManagerDTO> GetAllManagerByDepartment(int departmentId)
        {
            IEnumerable<User> managers = _userRepository.GetUsersByDepartmentAndRole(departmentId, RoleEnum.Manager.ToString());
            return managers.Select(manager => manager.ToManagerDTO());
        }
        public bool IsNicUnique(string nic) => _userRepository.IsNicUnique(nic);
    }
}
