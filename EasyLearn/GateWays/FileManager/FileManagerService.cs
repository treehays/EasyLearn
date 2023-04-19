using EasyLearn.Services.Interfaces;

namespace EasyLearn.GateWays.FileManager;

public class FileManagerService : IFileManagerService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileManagerService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> GetFileName(IFormFile file, params string[] docPath)
    {
        //string fileRelativePathx = null;
        //var docPathJoined = string.Join('\\', docPath);
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, string.Join('\\', docPath));

        if (file.Length > 0 && filePath != null)
        {
            //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profilePictures"); //ppop
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileName = Guid.NewGuid().ToString().Replace('-', 's') + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(filePath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        return null;
    }



    public async Task<List<string>> GetListOfFileName(List<IFormFile> files, params string[] docPath)
    {
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, string.Join('\\', docPath));
        if (files.Count > 0 && filePath != null)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var fileNames = new List<string>();
            foreach (var item in files)
            {
                var fileName = Guid.NewGuid().ToString().Replace('-', 's') + Path.GetExtension(item.FileName);
                var fullPath = Path.Combine(filePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                fileNames.Add(fileName);
            }
            return fileNames;
        }
        return null;
    }

}
