using CapaDatos.CsvMappers;
using CapaDominio.Entities;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace CapaDatos.Contextos
{
    public static class ModelBuilderExtensions
    {
        public static void SeedColours(this ModelBuilder modelBuilder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Color> colors = new List<Color>();
            Stream stream = assembly.GetManifestResourceStream("CapaDatos.SeedData.colours.csv");
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            CsvReader csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            csv.Configuration.Delimiter = ",";
            csv.Configuration.MissingFieldFound = null;
            while (csv.Read())
            {
                Color color = csv.GetRecord<Color>();
                colors.Add(color);
            }
            modelBuilder.Entity<Color>().HasData(colors);
        }
    }
}
