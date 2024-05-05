using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Dtos.MenuDto
{
    public class MenuDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public int ImageId { get; set; }
        public decimal Price { get; set; }
        public string FileName { get; set; }
    }
}
