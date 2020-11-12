using CapaDatos.Contextos;
using CapaNegocio.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CapaDatos.Repositorios
{
    public class CacheRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDistributedCache _distributedCache;
        private readonly Repository<TEntity> _repository;
        protected readonly GlobalContext _dbContext;
        private readonly string STATIC_VERSION = "0";

        public CacheRepository(IDistributedCache distributedCache, Repository<TEntity> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public TEntity GetById(string entityName, object id)
        {
            TEntity entity = null;
            var cacheKey = GetKey(entityName, id.ToString());
            var value = _distributedCache.Get(cacheKey);

            if (value == null)
            {
                entity = _repository.GetById(id);

                _distributedCache.Set(cacheKey, ToByteArray(entity));
            }
            else
            {
                entity = FromByteArray<TEntity>(value);
                _dbContext.Attach(entity);
            }

            return entity;
        }

        public byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public T FromByteArray<T>(byte[] data)
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

        private string Quote(string value)
        {
            return value.Replace("#", "##");
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAttach(TEntity entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void Remove(object id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            throw new NotImplementedException();
        }
    }
}
