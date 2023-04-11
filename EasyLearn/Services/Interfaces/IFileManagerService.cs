namespace EasyLearn.Services.Interfaces;

public interface IFileManagerService
{
    Task<string> GetFileName(IFormFile file, string fileFolderName);
}
