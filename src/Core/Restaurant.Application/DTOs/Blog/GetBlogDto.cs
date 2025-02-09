


namespace Restaurant.Application.DTOs
{
   public record GetBlogDto(int Id, string Article, string Title, string Image,GetAuthorDto Author,GetGenreDto Genre);
}
