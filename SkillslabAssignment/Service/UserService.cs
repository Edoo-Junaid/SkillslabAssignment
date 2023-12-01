﻿using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                PendingAccount pendingAccount = _pendingAccountRepository.GetById((int)createUserDTO.PendingAccountId);
                if (pendingAccount == null)
                {
                    throw new Exception("Pending account not found");
                }
                _userRepository.CreateUser(createUserDTO, pendingAccount);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<ManagerDTO> GetAllManagerByDepartment(int departmentId)
        {
            IEnumerable<User> managers = _userRepository.GetUsersByDepartmentAndRole(departmentId, RoleEnum.Manager.ToString());
            return managers.Select(manager => UserMapper.UserToManagerDTO(manager));
        }

    }
}
