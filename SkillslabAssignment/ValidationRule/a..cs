using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service.ValidationRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.ValidationRule
{
    public class a : IA
    {
        private readonly IEnumerable<IValidatorRule<PendingAccount>> _validatorRules;
        public a(IEnumerable<IValidatorRule<PendingAccount>> validatorRules)
        {
            _validatorRules = validatorRules;
        }
    }
}
