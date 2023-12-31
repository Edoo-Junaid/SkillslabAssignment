using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class UserService : GenericService<User, short>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }
        public async Task<bool> CreateUserAndAccountAsync(CreateUserDTO createUserDTO)
        {
            return await _userRepository.CreateUserAsync(createUserDTO);
        }
        public async Task<IEnumerable<ManagerDTO>> GetAllManagerByDepartmentAsync(byte departmentId)
        {
            IEnumerable<User> managers = await _userRepository.GetUsersByDepartmentAndRoleAsync(departmentId, RoleEnum.Manager.ToString());
            return managers.Select(manager => manager.ToManagerDTO());
        }
        public async Task<bool> IsNicUniqueAsync(string nic) => await _userRepository.IsNicUniqueAsync(nic);
    }
}
