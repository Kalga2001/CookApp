using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
