﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Models
{
     public class Tag
    {
        public string Name { get; set; }    
        public ICollection<BlogTags> BlogTags { get; set; }
    }
}
