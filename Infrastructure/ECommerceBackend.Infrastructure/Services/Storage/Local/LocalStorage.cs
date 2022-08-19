using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
                datas.Add((file.Name, $"{path}\\{file.Name}"));
            }
            return datas;
        }

        public async Task DeleteAsync(string fileName, string path) =>  File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dirInfo = new(path);
            return dirInfo.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string fileName, string path) => File.Exists($"{path}\\{fileName}");

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                //todo log!
                throw;
            }
        }
    }

}
