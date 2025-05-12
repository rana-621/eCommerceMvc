namespace eCommerceMvc.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file);
}
