using AutoMapper;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;


namespace Restaurant.Application.MappingProfiles
{
    internal class GenreProfile:Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre,GenreItemDto>();
            CreateMap<Genre, GetGenreDto>();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto,Genre>();
        }
    }
}
