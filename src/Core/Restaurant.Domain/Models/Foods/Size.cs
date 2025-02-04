using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models.Foods
{
    public class Size : BaseEntity
    {
        public string Name { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
    }
}
