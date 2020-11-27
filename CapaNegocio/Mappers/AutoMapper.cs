using AutoMapper;
using CapaDominio.Entities;
using CapaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Color, ColorDto>();
            CreateMap<ColorDto, Color>();
        }
    }
}
