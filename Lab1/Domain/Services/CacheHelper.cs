using Domain.Entities;
using Domain.Shared.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IMemoryCache _iMemoryCache;
        public CacheHelper(IMemoryCache memoryCache)
        {
            _iMemoryCache = memoryCache;
        }
        public bool CreateAsync<TEntity>(TEntity entity, string key) where TEntity : class
        {
            try
            {
                var data = entity;
                var cacheMemoryOptions = new MemoryCacheEntryOptions()
                                            .SetSlidingExpiration(TimeSpan.FromDays(1));
                _iMemoryCache.Set(key, entity, cacheMemoryOptions);
                return true;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public TEntity GetAsync<TEntity>(string key) where TEntity : class
        {
            return _iMemoryCache.Get<TEntity>(key);
        }

        public bool Remove(string key)
        {
            _iMemoryCache.Remove(key);
            return true;
        }
    }
}
