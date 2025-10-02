using AutoMapper;
using Usuario.Core.DTOs;
using Usuario.API.Models;

namespace Usuario.API.Mappers
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioDto, Users>().ReverseMap();
        }
    }
}