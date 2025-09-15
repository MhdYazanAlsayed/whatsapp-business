using Microsoft.AspNetCore.Http;
using SprintBuisness.Contracts.Markers;

namespace SprintBuisness.Contracts.Services
{
    public interface IFileManager : IScopedDependency
    {
        Task<string> SaveAsync(string folderName, IFormFile file);
        void Delete(string folderName, string fileName);
    }
}
