using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service.ValidationRule
{
    public interface IValidatorRule<T>
    {
        Task ValidateAsync(T parameter, List<ValidationResult> validationResults);
    }
}

