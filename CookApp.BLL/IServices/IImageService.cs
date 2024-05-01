using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
        Task<bool> DeleteImageAsync(string imagePath);
    }
}
