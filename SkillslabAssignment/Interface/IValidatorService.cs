using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IValidatorService<T>
    {
        Task<IEnumerable<ValidationResult>> ValidateAsync<U>(U parameter);
    }
}