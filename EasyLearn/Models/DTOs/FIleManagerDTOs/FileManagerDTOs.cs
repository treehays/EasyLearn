namespace EasyLearn.Models.DTOs.FIleManagerDTOs;

public class FileManagerDTOs
{
    public string FileName { get; set; }
    public TimeSpan VideoDuration { get; set; }
}


public class VideoNameAndDurationResponseModel
{
    public string FileName { get; set; }
    public TimeSpan VideoDuration { get; set; }
}

/// <summary>
/// arrangment of th the propertoes matters
/// </summary>

public class CSVFileManagerDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Objective { get; set; }
    public string Resources { get; set; }
    public string Prerequisites { get; set; }
    public string VideoName { get; set; }
    public string UploaderEmail { get; set; }
}


public class CSVFileResponseModel : BaseResponse
{
    public string FileName { get; set; }
    public ICollection<CSVFileManagerDTO> Data { get; set; }
}

public class CSVFileRequestModel
{
    public string CourseId { get; set; }
    public int NumbersOfVideos { get; set; }
    public string FileName { get; set; }
    public ICollection<CSVFileManagerDTO> Data { get; set; }
}


