using CapaDatos.Contextos;
using CapaDominio.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CapaDatos.Repositorios
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly GlobalContext _dbContext;

        public Repository(GlobalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            _dbContext.Set<TEntity>().AddRange(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public TEntity GetById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(query, parameters);
        }

        public void RemoveAttach(TEntity entityToDelete)
        {

            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)

            {

                _dbContext.Set<TEntity>().Attach(entityToDelete);

            }

            _dbContext.Set<TEntity>().Remove(entityToDelete);

        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Remove(object id)
        {
            TEntity entityToDelete = _dbContext.Set<TEntity>().Find(id);

            RemoveAttach(entityToDelete);
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _dbContext.Set<TEntity>().RemoveRange(entity);
        }
    }
}
