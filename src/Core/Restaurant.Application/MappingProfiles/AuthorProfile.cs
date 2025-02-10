using AutoMapper;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;




namespace Restaurant.Application.MappingProfiles
{
    internal class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author,AuthorItemDto>();
            CreateMap<Author,GetAuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
        }
    }
}
