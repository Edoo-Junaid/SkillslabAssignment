using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service.ValidationRule;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class ValidatorService<T> : IValidatorService<T>
    {
        private readonly List<IValidatorRule<T>> _validatorRules;

        public ValidatorService(IEnumerable<IValidatorRule<T>> validatorRules)
        {
            _validatorRules = validatorRules.ToList();
        }

        public async Task<IEnumerable<ValidationResult>> ValidateAsync<U>(U parameter)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(parameter);
            Validator.TryValidateObject(parameter, validationContext, validationResults, validateAllProperties: true);

            foreach (var rule in _validatorRules)
            {
                if (parameter is T typedParameter)
                {
                    await rule.ValidateAsync(typedParameter, validationResults);
                }
            }
            return validationResults;
        }
    }
}
