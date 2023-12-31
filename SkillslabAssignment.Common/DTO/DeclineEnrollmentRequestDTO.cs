using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.DTO
{
    public class DeclineEnrollmentRequestDTO
    {
        [Required]
        public int EnrollmentId { get; set; }
        [Required]
        public string DeclineReason { get; set; }
    }
}
