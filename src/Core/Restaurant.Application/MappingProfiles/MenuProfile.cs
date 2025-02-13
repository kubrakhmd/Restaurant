
using AutoMapper;
using Restaurant.Application.DTOs.Menu;
using Restaurant.Domain.Models;

namespace Restaurant.Application.MappingProfiles
{
    internal class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuItem, Menu>()
            .ForMember(a => a.UpdatedAt, b => b.Ignore());

        }


    }
}