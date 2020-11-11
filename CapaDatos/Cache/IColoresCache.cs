using Microsoft.Extensions.Caching.Distributed;

namespace CapaDatos.Cache
{
    public interface IColoresCache
    {
        byte[] Get(string key);

        void Set(string key, byte[] value);
    }
}
