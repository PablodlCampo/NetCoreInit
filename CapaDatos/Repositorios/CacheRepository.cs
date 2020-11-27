using CapaDatos.Contextos;
using CapaDominio.Entities;
using CapaDominio.RepositoryInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CapaDatos.Repositorios
{
    public class CacheRepository : ICacheRepository
    {
        private static readonly string ENTITY_NAME = "COLORES";
        private readonly IDistributedCache _distributedCache;
        private readonly Repository<Color> _repository;
        protected readonly GlobalContext _dbContext;
        private readonly string STATIC_VERSION = "0";

        public CacheRepository(IDistributedCache distributedCache, Repository<Color> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public Color GetById(object id)
        {
            var cacheKey = GetKey(ENTITY_NAME, id.ToString());
            var value = _distributedCache.Get(cacheKey);

            Color entity;
            if (value == null)
            {
                entity = _repository.GetById(id);

                _distributedCache.Set(cacheKey, ToByteArray(entity));
            }
            else
            {
                entity = FromByteArray<Color>(value);
                _dbContext.Attach(entity);
            }

            return entity;
        }

        private string Quote(string value)
        {
            return value.Replace("#", "##");
        }

        public void Set(Color entity)
        {
            var cacheKey = GetKey(entity.Nombre, entity.Id.ToString());

            _distributedCache.Set(cacheKey, ToByteArray(entity));

            _dbContext.Set<Color>().Add(entity);
        }

        public void Update(Color entity)
        {
            var cacheKey = GetKey(entity.Nombre, entity.Id.ToString());

            _distributedCache.Set(cacheKey, ToByteArray(entity));

            _repository.Update(entity);
        }

        public void Remove(object id)
        {
            Color entity = _repository.GetById(id);

            var cacheKey = GetKey(entity.Nombre, entity.Id.ToString());

            _distributedCache.Set(cacheKey, null);

            _repository.Remove(entity);
        }

        public void Remove(Color entity)
        {
            var cacheKey = GetKey(entity.Nombre, entity.Id.ToString());

            _distributedCache.Set(cacheKey, null);

            _repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Color> entities)
        {
            foreach (var entity in entities)
            {
                var cacheKey = GetKey(entity.Nombre, entity.Id.ToString());
                _distributedCache.Set(cacheKey, null);
            }

            _repository.RemoveRange(entities);
        }
        private byte[] ToByteArray<T>(T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        private string GetKey(string entityName, string identifier)
        {
            return entityName + "#" + STATIC_VERSION + "#" + Quote(identifier);
        }
    }
}
