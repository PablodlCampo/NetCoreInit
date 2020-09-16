﻿using CapaDatos.Contextos;
using CapaDatos.Repositorios;
using CapaNegocio.Entidades;
using CapaNegocio.Interfaces;
using CapaNegocio.InterfacesServicios;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PruebasXUnit
{
    public class ColoresTests
    {
        private static ColoresService servicioColores = null;

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
            ColoresContext context = ObtenerContexto();

            servicioColores = new ColoresService(new ColoresRepository(context));
        }

        private ColoresContext ObtenerContexto()
        {
            //Indicamos que utilizará base de datos en memoria
            //y que no deseamos que marque error si realizamos
            //transacciones en el código de nuestra aplicación
            var options = new DbContextOptionsBuilder<ColoresContext>()
                            .ConfigureWarnings
                            (x => x.Ignore(InMemoryEventId
                                    .TransactionIgnoredWarning))
                            .UseInMemoryDatabase(databaseName: "Test")
                                    .Options;
            //Inicializamos la configuración de la base de datos
            var context = new ColoresContext(options);

            //Creamos unos datos de prueba.
            Inicializar(context);
            return context;
        }

        private void Inicializar(ColoresContext contexto)
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