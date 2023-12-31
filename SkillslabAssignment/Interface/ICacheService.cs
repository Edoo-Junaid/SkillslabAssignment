using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface ICacheService
    {
        void Add(string key, object value, int expirationTimeInSeconds);
        object Get(string key);
        void Remove(string key);
        void Clear();
    }
}
