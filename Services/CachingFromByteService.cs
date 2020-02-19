using EasyCaching.Core;
using Microsoft.Extensions.Caching.Distributed;
using Registration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class CachingFromByteService : ICacheService
    {
        private readonly IDistributedCache distributedCache;

        public CachingFromByteService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<string> GetItem(string key)
        {
            var cachedData = await distributedCache.GetAsync(key);
            return Encoding.UTF8.GetString(cachedData);
        }

        public async Task<bool> IsExist(string key)
        {
            var cachedData = await distributedCache.GetAsync(key);
            return cachedData != null;
        }

        public async Task SetItem(string key, string value)
        {
            var data = Encoding.UTF8.GetBytes(value);
            await distributedCache.SetAsync(key, data);
        }
    }
}
