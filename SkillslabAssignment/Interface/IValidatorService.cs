using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillslabAssignment.Interface
{
    public interface IValidatorService<T>
    {
        IEnumerable<ValidationResult> Validate<U>(U parameter);
    }
}