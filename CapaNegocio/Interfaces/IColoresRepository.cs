using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Interfaces
{
    public interface IColoresRepository
    {
        public IEnumerable<Color> GetAll();

        void InsertMany(IEnumerable<Color> colores);

        void Insert(Color colores);
    }
}
