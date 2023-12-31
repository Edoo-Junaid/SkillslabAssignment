﻿using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IWebPageService : IGenericService<WebPage, int>
    {
        Task<IEnumerable<WebPage>> GetAllByUserIdAsync(short userId);
    }
}
