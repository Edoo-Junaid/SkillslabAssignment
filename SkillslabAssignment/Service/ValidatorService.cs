using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<ValidationResult>> ValidateAsync<U>(U parameter)
        {
            validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(parameter);
            Validator.TryValidateObject(parameter, validationContext, validationResults, validateAllProperties: true);
            if (parameter is PendingAccount)
            {
                PendingAccount pendingAccount = parameter as PendingAccount;
                await ValidateUniqueEmailAsync(pendingAccount.Email);
                await ValidateUniqueNic(pendingAccount.Nic);
            }
            return validationResults;
        }

        private async Task ValidateUniqueEmailAsync(string email)
        {
            if (!await IsEmailUnique(email))
            {
                validationResults.Add(new ValidationResult("Email must be unique", new[] { "Email" }));
            }
        }

        private async Task ValidateUniqueNic(string nic)
        {
            if (!await IsNicUnique(nic))
            {
                validationResults.Add(new ValidationResult("NIC must be unique", new[] { "Nic" }));
            }
        }
        private async Task<bool> IsEmailUnique(string email)
        {
            return await _accountService.IsEmailUniqueAsync(email) && await _pendingAccountService.IsEmailUniqueAsync(email);
        }

        private async Task<bool> IsNicUnique(string nic)
        {
            return await _pendingAccountService.IsNicUniqueAsync(nic) && await _userService.IsNicUniqueAsync(nic);
        }
    }
}
