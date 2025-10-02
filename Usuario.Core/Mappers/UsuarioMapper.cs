using AutoMapper;
using Usuario.Core.DTOs;
using Usuario.Infrastructure.Data.Models;

namespace Usuario.Core.Mappers
{
    public class UsuarioMapper: Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioData, UsuarioDto>().ReverseMap();
        }
    }
}