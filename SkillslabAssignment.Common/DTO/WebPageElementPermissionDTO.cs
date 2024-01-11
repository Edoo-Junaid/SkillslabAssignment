using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.DTO
{
    public class WebPageElementPermissionDTO
    {
        public int PageId { get; set; }
        public List<int> ListUiComponentId { get; set; }
    }
}
