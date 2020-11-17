using System.Collections.Generic;

namespace CapaDominio.RepositoryInterfaces
{
    public interface IExternalCache
    {
        public string Get(string key);
        public void Set(string key, string value);
        public IDictionary<string, string> MultiGet(IEnumerable<string> keys);
        public void MultiSet(IDictionary<string, string> values);
    }
}
