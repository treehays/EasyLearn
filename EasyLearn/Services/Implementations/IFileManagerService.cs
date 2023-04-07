namespace EasyLearn.Services.Implementations;

public interface IFileManagerService
{
    Task<string> GetFileName(IFormFile file, string fileFolderName);
}
