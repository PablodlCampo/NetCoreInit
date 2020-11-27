using CapaDominio.Entities;
using CapaDominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CapaDominio.RepositoryInterfaces
{
    public class ColorFilter : IFilter<Color>
    {
        public string MultiSearch { get; set; }

        public ColorFilter(string MultiSearch)
        {
            this.MultiSearch = MultiSearch;
        }

        Expression<Func<Color, bool>> IFilter<Color>.Where()
        {
            return
                b => b.Nombre.Trim().ToLower().Contains(MultiSearch.Trim().ToLower());
        }
    }

    public class ColorOrder : IOrder<Color>
    {
        public List<Tuple<ColorOrders, bool>> Order { get; set; }

        public ColorOrder(List<Tuple<ColorOrders, bool>> Order)
        {
            this.Order = Order;
        }

        List<Tuple<Expression<Func<Color, object>>, bool>> IOrder<Color>.OrderBy()
        {
            List<Tuple<Expression<Func<Color, object>>, bool>> list = new List<Tuple<Expression<Func<Color, object>>, bool>>();

            foreach (Tuple<ColorOrders, bool> order in Order)
            {
                Expression<Func<Color, object>> expression = order.Item1 switch
                {
                    ColorOrders.NAME => b => b.Nombre,
                    _ => throw new Exception(),
                };
                list.Add(new Tuple<Expression<Func<Color, object>>, bool>(expression, order.Item2));
            }

            return list;
        }
    }

    public interface IColoresRepository : IPagedRepository<Color, ColorFilter, ColorOrder>
    {
        void InsertMany(IEnumerable<Color> colores);

        void Insert(Color colores);

        public Color GetById(int id);

        public void CargarCache();
    }
}
