using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.DTO
{
    public class EnrollmentDetailsDto
    {
        [Column("training_id")]
        public int TrainingId { get; set; }
        [Column("training_name")]
        public string TrainingName { get; set; }
        [Column("training_start_date")]
        public DateTime TrainingStartDate { get; set; }
        [Column("enrollment_status")]
        public string Status { get; set; }
    }
}
