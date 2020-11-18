using CapaDominio.Entities;
using CapaNegocio.InterfacesServicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CapaPresentacion.Controllers
{
    public class ColoresController : Controller //Heredar de Controller permite al ColoresController utilizar sus funciones como IActionResult.
    {
        private readonly IColoresService _coloresService = null;

        public ColoresController(IColoresService coloresService)
        {
            _coloresService = coloresService;
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
    }
}
