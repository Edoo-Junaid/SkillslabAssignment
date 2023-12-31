using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service.ValidationRule
{
    public class UniqueNicValidatorRule : IValidatorRule<PendingAccount>
    {
        private readonly IPendingAccountService _pendingAccountService;
        private readonly IUserService _userService;

        public UniqueNicValidatorRule(IPendingAccountService pendingAccountService, IUserService userService)
        {
            _pendingAccountService = pendingAccountService;
            _userService = userService;
        }

        public async Task ValidateAsync(PendingAccount parameter, List<ValidationResult> validationResults)
        {
            if (!await _pendingAccountService.IsNicUniqueAsync(parameter.Nic) || !await _userService.IsNicUniqueAsync(parameter.Nic))
            {
                validationResults.Add(new ValidationResult("NIC must be unique", new[] { "Nic" }));
            }
        }
    }
}