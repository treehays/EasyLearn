namespace EasyLearn.Services.Interfaces;

public interface IFileManagerService
{
    Task<string> GetFileName(IFormFile file, params string[] docPath);
    Task<List<string>> GetListOfFileName(List<IFormFile> formFiles, params string[] docPath);
}
