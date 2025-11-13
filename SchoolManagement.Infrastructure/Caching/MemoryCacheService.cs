using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace SchoolManagement.Infrastructure.Caching
{
    public class MemoryCacheService:ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        public async Task<T?> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T? value);
            return await Task.FromResult(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options=new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow=expiration ?? TimeSpan.FromMinutes(10)
            };
            _memoryCache.Set(key, value, options);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            await Task.CompletedTask;
        }

    }
}
