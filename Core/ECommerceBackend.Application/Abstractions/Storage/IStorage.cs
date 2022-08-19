using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);

        Task DeleteAsync(string fileName, string pathOrContainerName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string fileName,string pathOrContainerName);
    }
}
