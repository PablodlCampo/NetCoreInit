using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDominio.Enums
{
    public static class EnumUtils
    {
        public static T ParseEnum<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
