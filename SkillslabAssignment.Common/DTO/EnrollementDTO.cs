﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.DTO
{
    public class EnrollementDTO
    {
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("training_name")]
        public string TrainingName { get; set; }
        [Column("enrollement_id")]
        public string EnrollmentId { get; set; }
    }
}
