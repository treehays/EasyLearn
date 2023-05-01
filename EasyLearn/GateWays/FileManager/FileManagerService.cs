
//using Microsoft.WindowsAPICodePack.Shell;
using EasyLearn.Models.DTOs.FIleManagerDTOs;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

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
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, string.Join('\\', docPath));

        if (file != null && filePath != null)
        {
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



    //public async Task<List<string>> GetListOfFileName(List<IFormFile> files, params string[] docPath)
    //{
    //    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, string.Join('\\', docPath));
    //    if (files.Count > 0 && filePath != null)
    //    {
    //        if (!Directory.Exists(filePath))
    //        {
    //            Directory.CreateDirectory(filePath);
    //        }
    //        var fileNames = new List<string>();
    //        foreach (var item in files)
    //        {
    //            var fileName = Guid.NewGuid().ToString().Replace('-', 's') + Path.GetExtension(item.FileName);
    //            var fullPath = Path.Combine(filePath, fileName);
    //            using (var stream = new FileStream(fullPath, FileMode.Create))
    //            {
    //                await item.CopyToAsync(stream);
    //            }
    //            fileNames.Add(fileName);
    //            filePath = Path.Combine(filePath, fileName);
    //        }
    //        return fileNames;
    //    }
    //    return null;
    //}


    public async Task<List<VideoNameAndDurationResponseModel>> GetListOfVideoProperty(List<IFormFile> files, params string[] docPath)
    {
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, string.Join('\\', docPath));
        if (files.Count > 0 && filePath != null)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileNamesAndDurations = new List<VideoNameAndDurationResponseModel>();
            foreach (var item in files)
            {
                var fileName = Guid.NewGuid().ToString().Replace('-', 's') + Path.GetExtension(item.FileName);
                var fullPath = Path.Combine(filePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                var duration = GetVideoLenght(fullPath);

                fileNamesAndDurations.Add(
                    new VideoNameAndDurationResponseModel
                    {
                        FileName = fileName,
                        VideoDuration = duration,
                    });

            }
            return fileNamesAndDurations;
        }
        return null;
    }

    public TimeSpan GetVideoLenght(string videoPath)
    {
        var shellObject = ShellObject.FromParsingName(videoPath);
        var durationProperty = shellObject.Properties.GetProperty(SystemProperties.System.Media.Duration);
        if (durationProperty.ValueAsObject != null && durationProperty.ValueAsObject is ulong)
        {
            var durationValue = (ulong)durationProperty.ValueAsObject;
            var duration = TimeSpan.FromTicks((long)durationValue);
            return duration;
        }
        return new TimeSpan();
    }
}
