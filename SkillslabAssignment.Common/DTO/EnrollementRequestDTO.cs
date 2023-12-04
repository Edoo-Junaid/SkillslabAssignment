using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SkillslabAssignment.Common.DTO
{
    public class EnrollementRequestDTO
    {
        public int TrainingId { get; set; }
        public int UserId { get; set; }
        public Dictionary<int, Stream> PrerequisiteToAttachment { get; set; }
    }
}
