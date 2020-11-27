using CapaDominio.Entities;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CapaDatos.CsvMappers
{
    public sealed class ColoursMapper : ClassMap<Color>
    {
        public ColoursMapper()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
        }
    }
}
