using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace SkillslabAssignment.Common.Validatora
{
    public static class ParameterValidator<T> where T : class
    {
        public static IEnumerable<ValidationResult> Validate(T parameter)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(parameter);
            Validator.TryValidateObject(parameter, validationContext, validationResults, validateAllProperties: true);
            return validationResults;
        }

        public static bool TryValidate(T parameter, out IEnumerable<ValidationResult> validationResults)
        {
            validationResults = Validate(parameter);
            return validationResults.Count() == 0;
        }

        public static bool TryValidateAndThrow(T parameter)
        {
            if (TryValidate(parameter, out var validationResults))
            {
                return true;
            }
            var validationErrors = validationResults.Select(result => result.ErrorMessage);
            throw new ValidationException($"Validation failed: {string.Join(", ", validationErrors)}");
        }
    }
}
