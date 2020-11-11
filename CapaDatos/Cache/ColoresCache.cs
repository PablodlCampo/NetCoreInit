using Microsoft.Extensions.Caching.Distributed;

namespace CapaDatos.Cache
{
    //En esta clase implemento solamente los dos métodos que me interesan de DistributedCache.
    public class ColoresCache : IColoresCache
    {
        IDistributedCache _distributedCache;

        public ColoresCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public byte[] Get(string key)
        {
            return _distributedCache.Get(key);
        }

        public void Set(string key, byte[] value)
        {
            // DistributedCacheEntryOptions se pasa nuevo en este caso porque los tiempos de expiracion no nos interesan.
            _distributedCache.Set(key, value, new DistributedCacheEntryOptions());
        }
    }
}
