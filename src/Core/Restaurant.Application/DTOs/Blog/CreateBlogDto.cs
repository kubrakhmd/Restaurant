

namespace Restaurant.Application.DTOs
{
    public record CreateBlogDto(string Article,string Title,string Image,int AuthorId,int GenreId);
   
}
