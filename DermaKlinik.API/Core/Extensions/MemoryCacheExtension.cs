using Microsoft.Extensions.Caching.Memory;

namespace DermaKlinik.API.Core.Extensions
{
    public static class MemoryCacheExtension
    {
        public static async Task<TItem> GetOrCreateAsync<TItem>(this IMemoryCache cache, object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            if (!cache.TryGetValue(key, out object result))
            {
                var entry = cache.CreateEntry(key);
                result = await factory(entry);
                entry.SetValue(result);
                // need to manually call dispose instead of having a using
                // in case the factory passed in throws, in which case we
                // do not want to add the entry to the cache
                entry.Dispose();
            }

            return (TItem)result;
        }
    }
}
