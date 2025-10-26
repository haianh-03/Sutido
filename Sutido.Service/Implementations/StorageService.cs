using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Supabase;
using Sutido.Model.Settings;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class StorageService : IStorageService
    {
        private readonly SupabaseSettings _settings;
        private readonly Client _client;

        public StorageService(IOptions<SupabaseSettings> options)
        {
            _settings = options.Value;
            _client = new Client(_settings.Url, _settings.ServiceRoleKey);
        }

        // Upload file
        public async Task<string> UploadFileAsync(IFormFile file, bool isPublic, string folder = "")
        {
            var bucketName = isPublic ? _settings.PublicBucket : _settings.PrivateBucket;
            var storage = _client.Storage.From(bucketName);

            // Tạo tên file duy nhất
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = string.IsNullOrEmpty(folder)
                ? fileName
                : $"{folder}/{fileName}";

            // Chuyển file sang byte[]
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            // Upload lên Supabase
            await storage.Upload(fileBytes, filePath, new Supabase.Storage.FileOptions
            {
                Upsert = true,
                CacheControl = "3600",
                ContentType = file.ContentType
            });

            return filePath;
        }

        public async Task<string> GetFileUrlAsync(string filePath, bool isPublic)
        {
            var bucketName = isPublic ? _settings.PublicBucket : _settings.PrivateBucket;
            var storage = _client.Storage.From(bucketName);

            if (isPublic)
            {
                return storage.GetPublicUrl(filePath);
            }
            else
            {
                // Ảnh private thì tạo Signed URL (7 ngày = 604800s)
                var signedUrl = await storage.CreateSignedUrl(filePath, 604800);

                // Loại bỏ ký tự '?' dư nếu có (ngăn lỗi InvalidJWT)
                if (signedUrl.EndsWith("?"))
                    signedUrl = signedUrl.TrimEnd('?');

                return signedUrl;
            }
        }

        public async Task<bool> DeleteFileAsync(string filePath, bool isPublic)
        {
            var bucketName = isPublic ? _settings.PublicBucket : _settings.PrivateBucket;
            var storage = _client.Storage.From(bucketName);

            var paths = new List<string> { filePath };
            var result = await storage.Remove(paths);

            // result trả về danh sách lỗi (nếu có)
            return result != null && result.Count > 0;
        }

        public async Task<bool> DeleteFolderAsync(string folderPath, bool isPublic)
        {
            var bucketName = isPublic ? _settings.PublicBucket : _settings.PrivateBucket;
            var storage = _client.Storage.From(bucketName);

            // Lấy danh sách tất cả file trong folder
            var list = await storage.List(folderPath);

            if (list == null || list.Count == 0)
                return true; // Không có gì để xóa

            // Tạo danh sách đường dẫn đầy đủ
            var filePaths = list.Select(item => $"{folderPath}/{item.Name}").ToList();

            var result = await storage.Remove(filePaths);

            return result != null && result.Count > 0;
        }

    }
}
