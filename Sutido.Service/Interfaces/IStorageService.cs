using Microsoft.AspNetCore.Http;

namespace Sutido.Service.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, bool isPublic, string folder = "");
        Task<string> GetFileUrlAsync(string filePath, bool isPublic);
        Task<bool> DeleteFileAsync(string filePath, bool isPublic);
        Task<bool> DeleteFolderAsync(string folderPath, bool isPublic);
    }
}
