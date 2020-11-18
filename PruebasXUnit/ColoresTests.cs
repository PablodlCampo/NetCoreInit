using CapaDatos.Cache;
using CapaDatos.Contextos;
using CapaDatos.Repositorios;
using CapaDominio.Entities;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using Xunit;

namespace PruebasXUnit
{
    public class ColoresTests
    {
        private static ColoresService servicioColores = null;
        protected readonly IColoresCache _coloresCache = null;

        public ColoresTests()
        {
            InitServices();
        }

        [Fact]
        public void ObtenerColores()
        {
            var colores = servicioColores.GetColores();

            Assert.Equal("Azul", colores[0].Nombre);
            Assert.Equal("Naranja", colores[1].Nombre);
        }

        private void InitServices()
        {
            GlobalContext context = ObtenerContexto();

            servicioColores = new ColoresService(new ColoresRepository(context, _coloresCache));
        }

        private GlobalContext ObtenerContexto()
        {
            //Indicamos que utilizará base de datos en memoria
            //y que no deseamos que marque error si realizamos
            //transacciones en el código de nuestra aplicación
            var options = new DbContextOptionsBuilder<GlobalContext>()
                            .ConfigureWarnings
                            (x => x.Ignore(InMemoryEventId
                                    .TransactionIgnoredWarning))
                            .UseInMemoryDatabase(databaseName: "Test")
                                    .Options;
            //Inicializamos la configuración de la base de datos
            var context = new GlobalContext(options);

            //Creamos unos datos de prueba.
            Inicializar(context);
            return context;
        }

        private void Inicializar(GlobalContext contexto)
        {
            //Te aseguras que la base de datos haya sido creada
            contexto.Database.EnsureCreated();

            List<Color> colores = new List<Color>()
            {
                new Color()
                {
                    Nombre = "Azul"
                },

                new Color()
                {
                    Nombre = "Naranja"
                }
            };

            foreach (var color in colores)
            {
                contexto.Colores.Add(color);
            }

            contexto.SaveChanges();
        }
    }
}
