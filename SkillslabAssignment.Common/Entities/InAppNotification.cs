using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillslabAssignment.Common.Interface;

namespace SkillslabAssignment.Common.Entities
{
    [Table("in_app_notification")]
    public class InAppNotification : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public short UserId { get; set; }
        [Column("message")]
        public string Message { get; set; }

        [Column("body")]
        public string Body { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("is_read")]
        public bool IsRead { get; set; } = false;

    }
}
