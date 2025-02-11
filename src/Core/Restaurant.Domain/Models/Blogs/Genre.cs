

namespace Restaurant.Domain.Models
{
    public class Genre:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
