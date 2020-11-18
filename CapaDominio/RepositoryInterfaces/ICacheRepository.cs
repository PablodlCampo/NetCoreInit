using CapaDominio.Entities;
using System.Collections.Generic;

namespace CapaDominio.RepositoryInterfaces
{
    public interface ICacheRepository
    {
        public Color GetById(object id);

        void Set(Color entity);

        void Update(Color entity);

        void Remove(object id);

        void Remove(Color entity);

        void RemoveRange(IEnumerable<Color> entities);
    }
}
