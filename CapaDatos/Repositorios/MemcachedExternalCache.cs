using CapaDominio.RepositoryInterfaces;
using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos.Repositorios
{
    public class MemcachedExternalCache : IExternalCache
    {
        private const int CACHE_TIME = 9000000;
        private readonly IMemcachedClient client;

        public MemcachedExternalCache(IMemcachedClient client)
        {
            this.client = client;
        }

        public string Get(string key)
        {
            return (string)client.Get(key);
        }
        public void Set(string key, string value)
        {
            var result = client.Set(key, value, CACHE_TIME);
            if (!result) throw new Exception("Key not set");
        }

        public IDictionary<string, string> MultiGet(IEnumerable<string> keys)
        {
            var tasks = keys.ToDictionary(k => k, k => client.Get(k));
            return tasks.ToDictionary(p => p.Key, p => (string)p.Value);
        }

        public void MultiSet(IDictionary<string, string> values)
        {
            var tasks = values.Select(p => client.Set(p.Key, p.Value, CACHE_TIME));
            foreach (var task in tasks)
            {
                if (!task) throw new Exception("Key not set");
            }
        }
    }
}
