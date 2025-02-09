using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class Blog: BaseEntity
    { 
        public string Name { get; set; }
        public string Article { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public ICollection<BlogTags> BlogTags { get; set; }
    }
}
