
//using Microsoft.WindowsAPICodePack.Shell;
using EasyLearn.Services.Interfaces;
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



                filePath = Path.Combine(filePath, fileName);

                ShellObject shellObject = ShellObject.FromParsingName(filePath);
                var durationProperty = shellObject.Properties.GetProperty(SystemProperties.System.Media.Duration);

                if (durationProperty.ValueAsObject != null && durationProperty.ValueAsObject is ulong)
                {
                    ulong durationValue = (ulong)durationProperty.ValueAsObject;
                    TimeSpan duration = TimeSpan.FromTicks((long)durationValue);
                    Console.WriteLine("Duration: " + duration.ToString());
                }
                else
                {
                    Console.WriteLine("Duration information not available.");
                }




                //var file = Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(pathhh);
                //var title = file.Properties.System.Title.Value;
                //var duration = file.Properties.GetProperty();
                //var duration = file.Properties.Media.Duration.Value;



                ////get video duration
                //// Retrieve the video duration
                //var mediaInfo = new MediaInfo.DotNetWrapper.MediaInfo();
                ////mediaInfo.Open($"{filePath}{}");
                //var pathhh = Path.Combine(filePath, fileName);
                //mediaInfo.Open(pathhh);
                //var duration = mediaInfo.Get(StreamKind.Video, 0, "Duration");
                //var durationInSeconds = double.Parse(duration) / 1000;
                //var videoDuration = TimeSpan.FromSeconds(durationInSeconds);
            }
            return fileNames;
        }
        return null;
    }

}
