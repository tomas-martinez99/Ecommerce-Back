namespace Web.Service
{
    using Application.Interfaces.Services;
    using Microsoft.AspNetCore.Hosting;

    public class LocalImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _env;

        public LocalImageStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(Stream content, string originalFileName, string? subfolder = null, CancellationToken ct = default)
        {
            // _env.WebRootPath apunta a wwwroot
            var folder = Path.Combine(_env.WebRootPath, "images", subfolder ?? "uploads");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await content.CopyToAsync(stream, ct);
            }

            return $"/images/{(subfolder ?? "uploads")}/{fileName}";
        }

        public Task<bool> DeleteAsync(string relativeUrl, CancellationToken ct = default)
        {
            var fullPath = Path.Combine(_env.WebRootPath, relativeUrl.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
