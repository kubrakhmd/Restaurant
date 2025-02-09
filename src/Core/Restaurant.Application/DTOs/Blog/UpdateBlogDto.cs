

namespace Restaurant.Application.DTOs
{ 
   public record UpdateBlogDto (string Article, string Title,  string Image, int GenreId, int AuthorId);
   
}
