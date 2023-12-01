using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trial
{
    public class Trial : IEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
    }
}
