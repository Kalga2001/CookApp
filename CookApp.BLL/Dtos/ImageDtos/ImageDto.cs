using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Dtos.ImageDtos
{
    public class ImageDto
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public byte[] ? Data { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
    }
}
