using AutoMapper;
using CapaDominio.Entities;
using CapaDominio.Enums;
using CapaDominio.RepositoryInterfaces;
using CapaNegocio.DTOs;
using CapaNegocio.DTOs.TableDTOs;
using CapaNegocio.InterfacesServicios;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio.Servicios
{
    public class ColoresService : IColoresService
    {
        private readonly IColoresRepository _coloresRepository = null;
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper;

        public ColoresService(IColoresRepository coloresRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _coloresRepository = coloresRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PaginatedListDto<ColorDto, ColorOrders> GetPaged(PaginatedListRequest<ColorOrders> request)
        {
            GetPageResponse<Color, ColorFilter, ColorOrder> countries = _coloresRepository.GetPaged
            (
                new GetPageRequest<Color, ColorFilter, ColorOrder>()
                {
                    PageNumber = request.CurrentPage,
                    PageSize = request.PageSize,
                    Filter = new ColorFilter(request.MultiSearch ?? string.Empty),
                    Order = new ColorOrder(request.OrderList),
                }
            );

            return new PaginatedListDto<ColorDto, ColorOrders>(_mapper.Map<List<Color>, List<ColorDto>>(countries.Entities.ToList()), countries.NumPages, countries.NumTotalEntities, request);
        }

        public List<Color> GetColores()
        {
            return _coloresRepository.GetAll().ToList();
        }

        //Lógica de la inserción de colores(habría que validar, comprobar que no se repitan, bla bla...)
        public void InsertColores(List<string> colores)
        {
            var listaColores = new List<Color>();

            foreach (string color in colores)
            {
                listaColores.Add(new Color()
                {
                    Nombre = color
                });
            }

            //Convendría gestionar las execpciones con un try catch...
            _coloresRepository.AddRange(listaColores);
        }

        public Color GetColorById(int Id)
        {
            return _coloresRepository.GetById(Id);
        }

        public void DeleteByCode(int Id)
        {
            _coloresRepository.Remove(Id);
            _unitOfWork.SaveChanges();
        }

        public void Save(Color color)
        {
            if (color.Id == 0)
            {
                _coloresRepository.Insert(color);
            }
            else
            {
                var bdColor = _coloresRepository.GetById(color.Id);
                bdColor.Nombre = color.Nombre;
                _coloresRepository.Update(bdColor);
            }

            _unitOfWork.SaveChanges();
        }

        public void CargarCache()
        {
            //_coloresRepository.CargarCache();
        }
    }
}
