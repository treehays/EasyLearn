using EasyLearn.Services.Implementations;

namespace EasyLearn.Services.Interfaces;

public class FileManagerService : IFileManagerService
{
    public async Task<string> GetFileName(IFormFile file, string filePath)
    {
        //string fileRelativePathx = null;

        if (file.Length > 0)
        {
            //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profilePictures"); //ppop
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
            var fullPath = Path.Combine(filePath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return ("/uploads/profilePictures/" + fileName);
        }
        return null;
    }
}
