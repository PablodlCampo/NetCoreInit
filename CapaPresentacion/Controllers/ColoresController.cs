using CapaDominio.Entities;
using CapaDominio.Enums;
using CapaNegocio.DTOs;
using CapaNegocio.DTOs.TableDTOs;
using CapaNegocio.InterfacesServicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CapaPresentacion.Controllers
{
    public class ColoresController : Controller //Heredar de Controller permite al ColoresController utilizar sus funciones como IActionResult.
    {
        public const int ITEMS_PER_PAGE = 10;
        public const int LIMIT_PAGES = 100;

        private readonly IColoresService _coloresService = null;

        public ColoresController(IColoresService coloresService)
        {
            _coloresService = coloresService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPartialResults(PartialResultRequestDto data)
        {
            data.SortOrder = string.IsNullOrEmpty(data.SortOrder) ? "Name" : data.SortOrder;

            ViewData["CurrentSort"] = data.SortOrder;
            ViewData["CurrentDesc"] = data.AscOrDescOrder;
            ViewData["ItemsPerPage"] = data.ItemsPerPage ?? ITEMS_PER_PAGE;

            if (data.SortOrder == "Id")
            {
                ViewData["IdAsc"] = !data.AscOrDescOrder;
                ViewData["IdStyle"] = data.AscOrDescOrder ? "fa fa-sort-desc" : "fa fa-sort-asc";
            }

            if (data.SortOrder == "Nombre")
            {
                ViewData["NombreDesc"] = !data.AscOrDescOrder;
                ViewData["NombreStyle"] = data.AscOrDescOrder ? "fa fa-sort-desc" : "fa fa-sort-asc";
            }

            if (string.IsNullOrEmpty(data.SearchString))
            {
                data.SearchString = data.CurrentFilter;
            }

            List<Tuple<ColorOrders, bool>> orderList = new List<Tuple<ColorOrders, bool>>();

            if (!string.IsNullOrEmpty(data.SortOrder))
            {
                orderList.Add(new Tuple<ColorOrders, bool>(EnumUtils.ParseEnum<ColorOrders>(data.SortOrder), data.AscOrDescOrder));
            }

            data.ItemsPerPage = data.ItemsPerPage > LIMIT_PAGES ? LIMIT_PAGES : data.ItemsPerPage;

            PaginatedListDto<ColorDto, ColorOrders> page = _coloresService.GetPaged(
                            new PaginatedListRequest<ColorOrders>()
                            {
                                CurrentPage = data.PageNumber,
                                PageSize = data.ItemsPerPage ?? ITEMS_PER_PAGE,
                                MultiSearch = data.SearchString,
                                OrderList = orderList
                            }
                        );

            return PartialView("_Results", page);
        }

        public List<Color> GetColores()
        {
            return _coloresService.GetColores();
        }

        //Esta función, al pintar la vista ira a buscarla a la ruta controlador/función, es decir en la carpeta Colores buscara la vista Colores.(Dentro de la carpeta Views)
        public IActionResult Colores()
        {
            var colores = GetColores();

            return View(colores);
        }

        public IActionResult Edit(int code)
        {
            var data = _coloresService.GetColorById(code);

            return PartialView("_Edit", data);
        }

        public void Save(Color color)
        {
           _coloresService.Save(color);
        }

        public void Delete(int code)
        {
            _coloresService.DeleteByCode(code);
        }
    }
}
