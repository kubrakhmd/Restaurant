
using AutoMapper;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;


namespace Restaurant.Application.MappingProfiles
{
    internal class TagProfile :Profile
    {
        public TagProfile()
        {

            CreateMap<Tag, GetTagDto>().ReverseMap();
            CreateMap<Tag, TagItemDto>();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();
        }
    }
}
