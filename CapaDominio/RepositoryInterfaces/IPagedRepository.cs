using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CapaDominio.RepositoryInterfaces
{
    public interface IPagedRepository<TEntity, TFilter, TOrder> : IRepository<TEntity>
        where TEntity : class
        where TFilter : IFilter<TEntity>
        where TOrder : IOrder<TEntity>
    {
        public GetPageResponse<TEntity, TFilter, TOrder> GetPaged(GetPageRequest<TEntity, TFilter, TOrder> listProps);
    }

    public interface IFilter<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Where();
    }

    public interface IOrder<TEntity> where TEntity : class
    {
        public List<Tuple<Expression<Func<TEntity, object>>, bool>> OrderBy();
    }
    public class GetPageRequest<TEntity, TFilter, TOrder>
               where TEntity : class
               where TFilter : IFilter<TEntity>
               where TOrder : IOrder<TEntity>
    {

        public int PageNumber;
        public int PageSize;
        public TFilter Filter { get; set; }
        public TOrder Order { get; set; }
    }

    public class GetPageResponse<TEntity, TFilter, TOrder> where TEntity : class
                where TFilter : IFilter<TEntity>
        where TOrder : IOrder<TEntity>
    {
        public GetPageResponse(GetPageRequest<TEntity, TFilter, TOrder> Request, int NumTotalEntities, IEnumerable<TEntity> Entities)
        {
            this.Request = Request;
            this.NumTotalEntities = NumTotalEntities;
            this.Entities = Entities;
        }

        public GetPageRequest<TEntity, TFilter, TOrder> Request { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }

        public int NumTotalEntities { get; set; }
        public int NumPages
        {
            get
            {
                return (int)Math.Ceiling(NumTotalEntities / (double)Request.PageSize);
            }
        } 
    }
}
