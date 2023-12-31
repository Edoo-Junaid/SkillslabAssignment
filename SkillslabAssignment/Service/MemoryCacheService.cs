using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class MemoryCacheService : ICacheService
    {
        private readonly ObjectCache cache = MemoryCache.Default;

        public void Add(string key, object value, int expirationTimeInSecond)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expirationTimeInSecond)
            };

            cache.Add(key, value, policy);
        }

        public void Clear()
        {
            foreach (var entry in cache)
            {
                cache.Remove(entry.Key);
            }
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}
