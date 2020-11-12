﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        TEntity GetById(object id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        void Remove(TEntity entity);

        void RemoveAttach(TEntity entityToDelete);

        void Remove(object id);

        void RemoveRange(IEnumerable<TEntity> entity);
    }
}
