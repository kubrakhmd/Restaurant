﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.DTOs.Menu
{
    public record MenuItem(int Id,string Name, string Description, decimal Price);

}
