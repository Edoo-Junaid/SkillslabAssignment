using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class PermissionService : GenericService<Permission, short>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ICacheService _cacheService;
        public PermissionService(IPermissionRepository permissionRepository, ICacheService cacheService) : base(permissionRepository)
        {
            _permissionRepository = permissionRepository;
            _cacheService = cacheService;
        }
        public async Task<IEnumerable<Permission>> GetAllByUserIdAsync(short userId)
        {
            return await _permissionRepository.GetAllByUserIdAsync(userId);
        }
        public async Task<bool> HasPermissionAsync(string userId, string permission)
        {
            string cacheKey = $"{userId}_{permission}";
            bool cachedValue = _cacheService.Get(cacheKey) as bool? ?? false;
            if (cachedValue)
            {
                return cachedValue;
            }
            bool hasPermission = await _permissionRepository.HasPermissionAsync(userId, permission);
            _cacheService.Add(cacheKey, hasPermission, expirationTimeInSeconds: 300);
            return hasPermission;
        }
    }
}
