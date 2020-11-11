
using CapaDatos.Cache;
using CapaDatos.Contextos;
using CapaDatos.Repositorios;
using CapaNegocio.Interfaces;
using CapaNegocio.InterfacesServicios;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CapaConsola
{
    public class Program
    {
        private static IColoresService servicioColores = null;

        //Ruta del archivo a leer.
        static readonly string textFile = @"FicherosLectura\Colores.txt";

        static void Main(string[] args)
        {
            InitServices();

            servicioColores.GetColorById(1);

            //List<string> colorList = LeerColores();
            //InsertColores(colorList);
        }

        //Esta clase se encarga de la inyección de dependencias.
        private static void InitServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IColoresService, ColoresService>();
            services.AddTransient<IColoresRepository, ColoresRepository>();
            services.AddTransient<IColoresCache, ColoresCache>();

            string connectionString = "Server=localhost;Database=cuadros;Trusted_Connection=True;";

            services.AddDbContext<ColoresContext>(options => options.UseSqlServer(connectionString));
            services.AddDistributedMemoryCache();

            var serviceProvider = services.BuildServiceProvider();
            servicioColores = serviceProvider.GetService<IColoresService>();

            servicioColores.CargarCache();
        }

        public static List<string> LeerColores()
        {
            List<string> colores = new List<string>();

            //En local la ruta a la que va es: "C:\\Proyectos\\ProyectoPruebas\\CapaConsola\\bin\\Debug\\netcoreapp3.1\\FicherosLectura\\Colores.txt"
            string ruta = AppDomain.CurrentDomain.BaseDirectory + textFile;
       
            string text = File.ReadAllText(ruta);

            foreach (var color in text.Split(","))
            {
                colores.Add(color);
                Console.WriteLine(color);
            }

            return colores;
        }

        public static void InsertColores(List<string> colores)
        {
            servicioColores.InsertColores(colores);
        }
    }
}
