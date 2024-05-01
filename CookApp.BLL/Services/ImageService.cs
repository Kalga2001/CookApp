using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imagesPath;
        public ImageService(string imagesPath)
        {
            _imagesPath = imagesPath;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            try
            {
                string imagePath = Path.Combine(_imagesPath, fileName);
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageStream.CopyToAsync(fileStream);
                }
                return imagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке изображения: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteImageAsync(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении изображения: {ex.Message}");
                return false;
            }
        }

   
    }
}
