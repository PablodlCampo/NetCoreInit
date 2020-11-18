using CapaDominio.Entities;
using System.Collections.Generic;

namespace CapaDominio.RepositoryInterfaces
{
    public interface IColoresRepository
    {
        public IEnumerable<Color> GetAll();

        void InsertMany(IEnumerable<Color> colores);

        void Insert(Color colores);

        public string GetById(int id);

        public void CargarCache();
    }
}
