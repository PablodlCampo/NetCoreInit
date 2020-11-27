using CapaDominio.Entities;
using CapaDominio.Enums;
using CapaNegocio.DTOs;
using CapaNegocio.DTOs.TableDTOs;
using System.Collections.Generic;

namespace CapaNegocio.InterfacesServicios
{
    public interface IColoresService
    {
        PaginatedListDto<ColorDto, ColorOrders> GetPaged(PaginatedListRequest<ColorOrders> request);

        List<Color> GetColores();

        void InsertColores(List<string> colores);

        void Save(Color color);

        void DeleteByCode(int Id);

        Color GetColorById(int Id);

        void CargarCache();
    }
}
