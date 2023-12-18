using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillslabAssignment.Service
{
    public class ValidatorService<T> : IValidatorService<T>
    {
        private readonly IAccountService _accountService;
        private readonly IPendingAccountService _pendingAccountService;
        private readonly IUserService _userService;
        public List<ValidationResult> validationResults;
        public ValidatorService(IAccountService accountService
            , IPendingAccountService pendingAccountService
            , IUserService userService)
        {
            _accountService = accountService;
            _pendingAccountService = pendingAccountService;
            _userService = userService;
        }
        public IEnumerable<ValidationResult> Validate<U>(U parameter)
        {
            validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(parameter);
            Validator.TryValidateObject(parameter, validationContext, validationResults, validateAllProperties: true);
            if (parameter is PendingAccount)
            {
                PendingAccount pendingAccount = parameter as PendingAccount;
                ValidateUniqueEmail(pendingAccount.Email);
                ValidateUniqueNic(pendingAccount.Nic);
            }
            return validationResults;
        }

        private void ValidateUniqueEmail(string email)
        {
            if (!IsEmailUnique(email))
            {
                validationResults.Add(new ValidationResult("Email must be unique", new[] { "Email" }));
            }
        }

        private void ValidateUniqueNic(string nic)
        {
            if (!IsNicUnique(nic))
            {
                validationResults.Add(new ValidationResult("NIC must be unique", new[] { "Nic" }));
            }
        }
        private bool IsEmailUnique(string email)
        {
            return _accountService.IsEmailUnique(email) && _pendingAccountService.IsEmailUnique(email);
        }

        private bool IsNicUnique(string nic)
        {
            return _pendingAccountService.IsNicUnique(nic) && _userService.IsNicUnique(nic);
        }
    }
}
