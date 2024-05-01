using CookApp.BLL.Dtos.ImageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Dtos.ProductDto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
    }
}
