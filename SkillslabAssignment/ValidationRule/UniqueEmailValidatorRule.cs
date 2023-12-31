using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SkillslabAssignment.Service.ValidationRule
{
    public class UniqueEmailValidatorRule : IValidatorRule<PendingAccount>
    {
        private readonly IAccountService _accountService;
        private readonly IPendingAccountService _pendingAccountService;

        public UniqueEmailValidatorRule(IAccountService accountService, IPendingAccountService pendingAccountService)
        {
            _accountService = accountService;
            _pendingAccountService = pendingAccountService;
        }

        public async Task ValidateAsync(PendingAccount parameter, List<ValidationResult> validationResults)
        {
            if (!await _accountService.IsEmailUniqueAsync(parameter.Email) || !await _pendingAccountService.IsEmailUniqueAsync(parameter.Email))
            {
                validationResults.Add(new ValidationResult("Email must be unique", new[] { "Email" }));
            }
        }
    }
}