using CapaNegocio.Entidades;
using System.Collections.Generic;

namespace CapaNegocio.InterfacesServicios
{
    public interface IColoresService
    {
        List<Color> GetColores();

        void InsertColores(List<string> colores);

        string GetColorById(int Id);

        void CargarCache();
    }
}
