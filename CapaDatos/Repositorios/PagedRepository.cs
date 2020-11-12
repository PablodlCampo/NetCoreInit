using CapaDatos.Contextos;
using CapaNegocio.Interfaces;
using System.Linq;

namespace CapaDatos.Repositorios
{
    public class PagedRepository<TEntity, TFilter, TOrder> : Repository<TEntity>, IPagedRepository<TEntity, TFilter, TOrder>
        where TEntity : class
        where TFilter : IFilter<TEntity>
        where TOrder : IOrder<TEntity>
    {
        public PagedRepository(GlobalContext context) : base(context) { }

        public GetPageResponse<TEntity, TFilter, TOrder> GetPaged(GetPageRequest<TEntity, TFilter, TOrder> request)
        {
            int pageSize = request.PageSize;
            int pageNumber = request.PageNumber;

            var query = from s in _dbContext.Set<TEntity>() select s;

            query = query.Where(request.Filter.Where());
            int totalItems = query.Count();

            foreach (var order in request.Order.OrderBy())
            {
                if (order.Item2)
                {
                    query = query.OrderByDescending(order.Item1);
                }
                else
                {
                    query = query.OrderBy(order.Item1);
                }
            }

            var entities = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new GetPageResponse<TEntity, TFilter, TOrder>(request, totalItems, entities) { };
        }
    }
}
