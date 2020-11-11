using CapaNegocio.Entidades;
using CapaNegocio.Interfaces;
using CapaNegocio.InterfacesServicios;
using System.Collections.Generic;
using System.Linq;


namespace CapaNegocio.Servicios
{
    public class ColoresService : IColoresService
    {
        private IColoresRepository _coloresRepository = null;

        public ColoresService(IColoresRepository coloresRepository)
        {
            _coloresRepository = coloresRepository;
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
            _coloresRepository.InsertMany(listaColores);
        }

        public string GetColorById(int Id)
        {
            return _coloresRepository.GetById(Id);
        }

        public void CargarCache()
        {
            _coloresRepository.CargarCache();
        }
    }
}
