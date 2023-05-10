using EasyLearn.GateWays.FileManager;
using EasyLearn.GateWays.Mappers.ModulesMappers;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.FIleManagerDTOs;
using EasyLearn.Models.DTOs.ModuleDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IFileManagerService _fileManagerService;
    private readonly IModulesMapperService _modulesMapperService;
    private readonly ICourseRepository _courseRepository;
    private readonly IEnrolmentRepository _enrolmentRepository;
    public ModuleService(IModuleRepository moduleRepository, IFileManagerService fileManagerService, IModulesMapperService modulesMapperService, IEnrolmentRepository enrolmentRepository, ICourseRepository courseRepository)
    {
        _moduleRepository = moduleRepository;
        _fileManagerService = fileManagerService;
        _modulesMapperService = modulesMapperService;
        _enrolmentRepository = enrolmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<BaseResponse> Create(CreateModuleRequestModel model, string instructorId)
    {
        if (model.FormFiles.Count() == 0)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "No video has been uploaded....",
            };
        }
        int i = await _moduleRepository.GetLastElement(x => x.CourseId == model.CourseId) + 1;
        var videoSequence = $"{model.CourseId}{i}";
        var fileNamesAndDuration = await _fileManagerService.GetListOfVideoProperty(model.FormFiles, "uploads", "Videos", "Modules");
        var listOfModules = new List<Module>();
        foreach (var item in fileNamesAndDuration)
        {
            var module = new Module
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Description = model.Description,
                Resources = model.Resources,
                Prerequisites = model.Prerequisites,
                Objective = model.Objective,
                ModuleDuration = item.VideoDuration,
                SequenceOfModule = i++,
                VideoSequence = videoSequence,
                VideoPath = item.FileName,
                CourseId = model.CourseId,
                CreatedBy = instructorId,
                CreatedOn = DateTime.Now,
                IsAvailable = true,
            };

            listOfModules.Add(module);
        }
        await _moduleRepository.AddRangeAsync(listOfModules);
        await _moduleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Status = true,
            Message = "video has been successfully uploaded...",
        };
    }

    public async Task<BaseResponse> Delete(string id, string userId)
    {
        var module = await _moduleRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
        if (module == null)
        {
            return new BaseResponse
            {
                Message = "module not Found...",
                Status = false,
            };
        }

        var date = DateTime.Now;
        var deletedby = userId;

        module.IsDeleted = true;
        module.DeletedOn = date;
        module.DeletedBy = deletedby;

        await _moduleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "module successfully deleted...",
            Status = true,
        };
    }

    public async Task<ModulesResponseModel> GetAll()
    {
        var modules = await _moduleRepository.GetAllAsync();
        if (modules == null)
        {
            return new ModulesResponseModel
            {
                Status = false,
                Message = "No moduls found",
            };
        }
        var modulesModel = new ModulesResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = modules.Select(x => _modulesMapperService.ConvertToModuleResponseModel(x)),
        };
        return modulesModel;
    }

    public async Task<ModuleResponseModel> GetSingleModuleByStudent(string moduleId, string studentId)
    {
        var module = await _moduleRepository.GetAsync(x => x.Id == moduleId && !x.IsDeleted);
        if (module == null)
        {
            return new ModuleResponseModel
            {
                Message = "module not found..",
                Status = false,
            };
        }

        var studentEnrollment = await _enrolmentRepository.GetAsync(x => x.StudentId == studentId && x.CourseId == module.CourseId && x.IsPaid);
        if (studentEnrollment == null)
        {
            return new ModuleResponseModel
            {
                Message = "Student has not enrolled for this course..",
                Status = false,
            };
        }

        var modulModel = new ModuleResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = _modulesMapperService.ConvertToModuleResponseModel(module),
        };
        return modulModel;
    }

    public async Task<ModulesResponseModel> GetCourseContentsByCourseInstructor(string courseId, string instructorId)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == courseId && !x.IsDeleted && x.InstructorId == instructorId);
        if (course == null)
        {
            return new ModulesResponseModel
            {
                Message = "Course not found..",
                Status = false,
            };
        }
        var modules = await _moduleRepository.GetListAsync(x => x.CourseId == courseId && !x.IsDeleted && x.CreatedBy == instructorId);

        if (modules.Count() == 0)
        {
            return new ModulesResponseModel
            {
                Message = "Nothing has been added yet..",
                Status = true,
            };
        }

        var modulesModel = new ModulesResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = modules.Select(x => _modulesMapperService.ConvertToModuleResponseModel(x)),
        };
        return modulesModel;
    }

    public async Task<ModulesResponseModel> GetCourseContentsByEnrolledStudent(string courseId, string studentId)
    {
        var modules = await _moduleRepository.GetListAsync(x => x.CourseId == courseId && !x.IsDeleted);

        if (modules.Count() == 0)
        {
            return new ModulesResponseModel
            {
                Message = "Nothing has been added yet..",
                Status = false,
            };
        }

        var modulesModel = new ModulesResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = modules.Select(x => _modulesMapperService.ConvertToModuleResponseModel(x)),
        };
        return modulesModel;
    }
    public async Task<ModuleResponseModel> GetById(string id)
    {
        var module = await _moduleRepository.GetAsync(x => x.Id == id && !x.IsDeleted);

        if (module == null)
        {
            return new ModuleResponseModel
            {
                Message = "module not found..",
                Status = false,
            };
        }

        var modulModel = new ModuleResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = _modulesMapperService.ConvertToModuleResponseModel(module),
        };
        return modulModel;
    }
    public async Task<ModuleResponseModel> GetByVideoSequesnce(string videoSequence)
    {
        var module = await _moduleRepository.GetAsync(x => x.VideoSequence == videoSequence && !x.IsDeleted);

        if (module == null)
        {
            return new ModuleResponseModel
            {
                Message = "module not found..",
                Status = false,
            };
        }

        var modulModel = new ModuleResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = _modulesMapperService.ConvertToModuleResponseModel(module),
        };
        return modulModel;
    }

    public async Task<ModulesResponseModel> GetNotDeleted()
    {
        var modules = await _moduleRepository.GetListAsync(x => !x.IsDeleted);
        if (modules == null)
        {
            return new ModulesResponseModel
            {
                Status = false,
                Message = "No moduls found",
            };
        }
        var modulesModel = new ModulesResponseModel
        {
            Status = true,
            Message = "modules retrieved successfuly..",
            Data = modules.Select(x => _modulesMapperService.ConvertToModuleResponseModel(x)),
        };
        return modulesModel;
    }

    public async Task<BaseResponse> Update(UpdateModuleRequestModel model, string userId)
    {
        var module = await _moduleRepository.GetAsync(x => x.Id == model.Id && !x.IsDeleted);
        if (module == null)
        {
            return new BaseResponse
            {
                Message = "module not found.",
                Status = false,
            };
        }

        module.Title = model.Title ?? module.Title;
        module.Description = model.Description ?? module.Description;
        module.Resources = model.Resources ?? module.Resources;
        module.Prerequisites = model.Prerequisites ?? module.Prerequisites;
        module.Objective = model.Objective ?? module.Objective;
        module.ModifiedOn = DateTime.Now;
        module.ModifiedBy = userId;
        await _moduleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "module updated successfully..",
            Status = true,
        };
    }

    public async Task<CSVFileResponseModel> GenerateModuleUploaderTemplate(CSVFileRequestModel model, string UserId, string UserEmail)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == model.CourseId);
        var listOfIds = new List<CSVFileManagerDTO>();
        var flag = 0;
        while (flag <= model.NumbersOfVideos)
        {
            var tempCourseId = new CSVFileManagerDTO
            {
                Id = $"{course.Title.Remove(10)}{Guid.NewGuid().ToString().Remove(10)}",
                UploaderEmail = UserEmail,
            };
            listOfIds.Add(tempCourseId);
            flag++;
        }

        var template = _fileManagerService.GenerateModuleUploaderTemplate(listOfIds);
        var createdOn = DateTime.Now;
        course.Modules = listOfIds.Select(x => new Module
        {
            Id = x.Id,
            CreatedOn = createdOn,
            CreatedBy = UserId,
            CourseId = course.Id,

        }).ToList();
        await _courseRepository.SaveChangesAsync();
        return new CSVFileResponseModel
        {
            Status = true,
            FileName = template,
            Message = "Module generated succesfully..",
            Data = listOfIds,
        };
    }

    public async Task<BaseResponse> ModuleUploaderTemplate(string fileName, string userId, string userEmail, string courseId)
    {
        var convertTodb = _fileManagerService.ReadModuleUploader(fileName);
        if (convertTodb.Data.Count() < 1)
        {
            return new BaseResponse
            {
                Message = "nothing found",
                Status = false,
            };
        }
        int i = await _moduleRepository.GetLastElement(x => x.CourseId == courseId) + 1;
        var coursess = await _courseRepository.GetAsync(x => x.Id == courseId);


        //var listOfVaidVideo = convertTodb.Data.Where(x => !string.IsNullOrWhiteSpace(x.VideoName) && !string.IsNullOrWhiteSpace(x.Title) && !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.UploaderEmail)).Select(x => new Module
        //{
        //    Id = x.Id.Trim(),
        //    Title = x.Title.Trim(),
        //    CreatedBy = userId,
        //    CreatedOn = DateTime.Now,
        //    Description = x.Description,
        //    Objective = x.Objective,
        //    Prerequisites = x.Prerequisites,
        //    Resources = x.Resources,
        //    //SequenceOfModule = i++,
        //    //VideoPath = $"{x.Id.Trim()}{x.VideoName.Remove(0, x.VideoName.Length - 4)}",
        //    //VideoSequence = $"{x.Id.Trim()}{x.VideoName.Remove(0, x.VideoName.Length - 4)}{i++}",
        //}).ToList();

        foreach (var x in convertTodb.Data)
        {
            var module = await _moduleRepository.GetAsync(x => x.Id == x.Id);
            if (module != null && !string.IsNullOrWhiteSpace(x.VideoName) && !string.IsNullOrWhiteSpace(x.Title) && !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.Description))
            {
                //module.Id = Guid.NewGuid().ToString();
                module.Title = x.Title.Trim();
                //module.CourseId = x.Title.Trim();
                module.CreatedBy = userId;
                module.CreatedOn = DateTime.Now;
                module.Description = x.Description;
                module.Objective = x.Objective;
                module.Prerequisites = x.Prerequisites;
                module.Resources = x.Resources;
                //module.SequenceOfModule = i++;
                module.VideoPath = x.VideoName;
                //module.VideoPath = $"{x.Id.Trim()}{x.VideoName.Remove(0, x.VideoName.Length - 4)}";
                //module.VideoSequence = $"{x.Id.Trim()}{x.VideoName.Remove(0, x.VideoName.Length - 4)}{i++}";
                //during upload we will check for where courseid and video name ma
            }
            await _moduleRepository.SaveChangesAsync();
        }

        ////coursess.Modules = listOfVaidVideo;
        //await _moduleRepository.UpdateRanges(listOfVaidVideo);
        //await _moduleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Status = true,
            Message = "succcess",
        };
    }

}