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
    public class UserService : GenericService<User, short>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<PendingAccount, short> _pendingAccountRepository;
        public UserService(IUserRepository repository, IGenericRepository<PendingAccount, short> pendingAccountRepository) : base(repository)
        {
            _userRepository = repository;
            _pendingAccountRepository = pendingAccountRepository;
        }
        public bool CreateUserAndAccount(CreateUserDTO createUserDTO)
        {
            return _userRepository.CreateUser(createUserDTO);
        }
        public IEnumerable<ManagerDTO> GetAllManagerByDepartment(byte departmentId)
        {
            IEnumerable<User> managers = _userRepository.GetUsersByDepartmentAndRole(departmentId, RoleEnum.Manager.ToString());
            return managers.Select(manager => manager.ToManagerDTO());
        }
        public bool IsNicUnique(string nic) => _userRepository.IsNicUnique(nic);
    }
}
