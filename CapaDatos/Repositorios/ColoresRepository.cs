using CapaDatos.Cache;
using CapaDatos.Contextos;
using CapaDominio.Entities;
using CapaDominio.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDatos.Repositorios
{
    public class ColoresRepository : PagedRepository<Color, ColorFilter, ColorOrder>, IColoresRepository
    {
        protected readonly IColoresCache _coloresCache;

        public ColoresRepository(GlobalContext dbContext, IColoresCache coloresCache) : base(dbContext)
        {
            _coloresCache = coloresCache;
        }

        public void InsertMany(IEnumerable<Color> colores)
        {
            _dbContext.Colores.AddRange(colores);

            _dbContext.SaveChanges();
        }

        public void Insert(Color colores)
        {
            _dbContext.Colores.Add(colores);

            _dbContext.SaveChanges();
        }

        public Color GetById(int id)
        {
            return _dbContext.Colores.Find(id);
            string color = string.Empty;
            string cacheKey = "COLOR_" + id;

            var value = _coloresCache.Get(cacheKey);

            if (value != null)
            {
                color = Encoding.UTF8.GetString(value);
            }
            else
            {
                var findColor = _dbContext.Colores.Find(id);

                if (findColor != null)
                {
                    color = findColor.Nombre;
                }
                else
                {
                    _coloresCache.Set(cacheKey, Encoding.ASCII.GetBytes(string.Empty));
                }
            }
        }

        public void CargarCache()
        {
            var colores = GetAll();

            foreach (var color in colores)
            {
                string key = "COLOR_" + color.Id;

                _coloresCache.Set(key, Encoding.ASCII.GetBytes(color.Nombre));
            }
        }
    }
}
