using AutoMapper;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;

namespace Restaurant.Application.MappingProfiles
{
    internal class BlogProfile:Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog,BlogItemDto>();
            CreateMap<Blog, GetBlogDto>()
              
               .ForCtorParam(nameof(GetBlogDto.Tags),
              opt => opt.MapFrom(
                  p => p.BlogTags.Select(pt => new TagItemDto(pt.TagId, pt.Tag.Name)).ToList())
              );
            CreateMap<CreateBlogDto, Blog>()
              
               .ForMember(p => p.BlogTags,
               opt => opt.MapFrom(pDto => pDto.TagIds.Select(ti => new BlogTag { TagId = ti }))
               );

            CreateMap<UpdateBlogDto, Blog>()
               .ForMember(
                p => p.Id,
                opt => opt.Ignore())
                
                 .ForMember(
                p => p.BlogTags,
                opt => opt.MapFrom(pDto => pDto.TagIds.Select(ci => new BlogTag { TagId = ci }))
                )
                 ;
        }
    
}

}
