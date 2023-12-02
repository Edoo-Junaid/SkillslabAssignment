﻿using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;

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

        public static UserDto CreateUserDtoFromPendingAccountAndDTO(PendingAccount pendingAccount, CreateUserDTO createUserDTO)
        {
            return new UserDto()
            {
                DepartmentId = (int)createUserDTO.DepartmentId,
                ManagerId = createUserDTO.ManagerId,
                RoleId = (int)createUserDTO.RoleId,
                NIC = pendingAccount.Nic,
                FirstName = pendingAccount.FirstName,
                LastName = pendingAccount.LastName,
                PhoneNumber = pendingAccount.PhoneNumber,
                Email = pendingAccount.Email,
                Password = pendingAccount.Password,
                PendingAccountId = pendingAccount.Id
            };
        }
    }
}
