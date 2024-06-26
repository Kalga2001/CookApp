﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.Entity.Entity
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ? Description { get; set; }
        public Image Image { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
