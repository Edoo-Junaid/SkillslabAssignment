using System.Collections.Generic;
using System.IO;

namespace SkillslabAssignment.Common.DTO
{
    public class EnrollementRequestDTO
    {
        public short TrainingId { get; set; }
        public short UserId { get; set; }
        public Dictionary<short, Stream> PrerequisiteToAttachment { get; set; }
        public string ContentType { get; set; }
    }
}
