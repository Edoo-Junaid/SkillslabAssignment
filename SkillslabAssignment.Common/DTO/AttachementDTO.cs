using SkillslabAssignment.Common.Entities;

namespace SkillslabAssignment.Common.DTO
{
    public class AttachementDTO
    {
        public int AttachmentId { get; set; }
        public string Url { get; set; }
        public int EnrollmentId { get; set; }
        public Prerequisite Prerequisite { get; set; }
    }
}
