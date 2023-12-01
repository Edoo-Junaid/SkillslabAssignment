using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Validator<T>
    {
        public IList<ValidationResult> Validate(T obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, serviceProvider: null, items: null);

            Validator.TryValidateObject(obj, validationContext, validationResults, validateAllProperties: true);

            return validationResults;
        }

        public bool TryValidate(T obj, out IList<ValidationResult> validationResults)
        {
            validationResults = Validate(obj);
            return validationResults.Count == 0;
        }
    }

}
