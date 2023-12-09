using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssignment.Common.DTO
{
    public class CreateTrainingRequestDTO : Training
    {
        public IEnumerable<int> PrerequisitesId { get; set; }
    }
}
