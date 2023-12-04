using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
