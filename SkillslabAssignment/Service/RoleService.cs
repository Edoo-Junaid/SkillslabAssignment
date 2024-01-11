using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class RoleService : GenericService<Role, byte>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> CreateRoleWithPageAndPageElementPermissionAsync(RolePagePermissionDTO rolePagePermission)
        {
            return await _roleRepository.CreateRoleWithPageAndPageElementPermission(rolePagePermission);
        }

        public async Task<IEnumerable<RolePagePermissionDTO>> GetAllRolePagePermission()
        {
            return await _roleRepository.GetAllRolePagePermission();
        }
    }
}
