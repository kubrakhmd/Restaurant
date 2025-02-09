using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
    public class BlogTag
    {
        public Blog Blog { get; set; }
        public Tag Tag { get; set; }
        public int BlogId { get; set; }
        public int TagId { get; set; }

    }
}
