using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Mapper
{
    public static class UserMapper
    {
        public static ManagerDTO UserToManagerDTO(User user)
        {
            return new ManagerDTO { Name = $"{user.FirstName} {user.LastName}", Id = user.Id };
        }

        public static User CreateUserDtoToUser(CreateUserDTO createUserDTO)
        {
            return new User()
            {
                RoleId = (int)createUserDTO.RoleId,
                DepartmentId = (int)createUserDTO.DepartmentId,
                ManagerId = createUserDTO.ManagerId
            };
        }

        public static User PendingAccountToUser(PendingAccount pendingAccount)
        {
            return new User()
            {
                FirstName = pendingAccount.FirstName,
                LastName = pendingAccount.LastName,
                NIC = pendingAccount.Nic,
                PhoneNumber = pendingAccount.PhoneNumber,

            };
        }

        public static User CreateUserFromPendingAccountAndDTO(PendingAccount pendingAccount, CreateUserDTO createUserDTO)
        {
            return new User()
            {
                FirstName = pendingAccount.FirstName,
                LastName = pendingAccount.LastName,
                NIC = pendingAccount.Nic,
                PhoneNumber = pendingAccount.PhoneNumber,
                RoleId = (int)createUserDTO.RoleId,
                DepartmentId = (int)createUserDTO.DepartmentId,
                ManagerId = createUserDTO.ManagerId
            };
        }
    }
}
