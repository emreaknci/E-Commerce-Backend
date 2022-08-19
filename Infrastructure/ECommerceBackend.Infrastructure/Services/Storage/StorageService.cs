using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.UploadAsync(pathOrContainerName, files);

        public async Task DeleteAsync(string fileName, string pathOrContainerName)
            => await _storage.DeleteAsync(fileName, pathOrContainerName);

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string fileName, string pathOrContainerName)
            => _storage.HasFile(fileName, pathOrContainerName);

        public string StorageName { get => _storage.GetType().Name; }
    }
}
