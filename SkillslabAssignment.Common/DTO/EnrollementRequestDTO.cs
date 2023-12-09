using System.Collections.Generic;
using System.IO;

namespace SkillslabAssignment.Common.DTO
{
    public class EnrollementRequestDTO
    {
        public int TrainingId { get; set; }
        public int UserId { get; set; }
        public Dictionary<int, Stream> PrerequisiteToAttachment { get; set; }
    }
}
