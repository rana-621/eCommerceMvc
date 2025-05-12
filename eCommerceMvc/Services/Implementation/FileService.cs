using eCommerceMvc.Services.Interfaces;

namespace eCommerceMvc.Services.Implementation;

public class FileService : IFileService
{
    private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    public FileService()
    {
        if (!Directory.Exists(_uploadFolder))
            Directory.CreateDirectory(_uploadFolder);
    }
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_uploadFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
            await file.CopyToAsync(stream);


        return $"/uploads/{fileName}";

    }
}
