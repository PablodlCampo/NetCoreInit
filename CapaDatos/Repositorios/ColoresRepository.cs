using CapaDatos.Contextos;
using CapaNegocio.Entidades;
using CapaNegocio.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos.Repositorios
{
    public class ColoresRepository : IColoresRepository
    {
        protected readonly ColoresContext _dbContext;

        public ColoresRepository(ColoresContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Color> GetAll()
        {
            return _dbContext.Colores.ToList();
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
    }
}
