using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models.Foods
{
    public class ProductImage
    {
        public string Image { get; set; }

        public bool? IsPrimary { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
