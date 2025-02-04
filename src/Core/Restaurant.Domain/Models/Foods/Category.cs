using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models.Foods
{
   
        public class Category :BaseEntity

        {
            [Required(ErrorMessage = "Ad mutleqdir")]
            [MaxLength(30, ErrorMessage = "Uzunlugu 30dan cox ola bilmez")]

            public string Name { get; set; }
            public List<Product>? Products { get; set; }

        }
    }
