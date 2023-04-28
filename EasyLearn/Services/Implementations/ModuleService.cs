using EasyLearn.GateWays.Mappers.ModulesMappers;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.ModulesDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileManagerService _fileManagerService;
        private readonly IModulesMapperService _modulesMapperService;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrolmentRepository _enrolmentRepository;
        public ModuleService(IModuleRepository moduleRepository, IHttpContextAccessor httpContextAccessor, IFileManagerService fileManagerService, IModulesMapperService modulesMapperService, IEnrolmentRepository enrolmentRepository, ICourseRepository courseRepository)
        {
            _moduleRepository = moduleRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileManagerService = fileManagerService;
            _modulesMapperService = modulesMapperService;
            _enrolmentRepository = enrolmentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<BaseResponse> Create(CreateModuleRequestModel model)
        {
            if (model.FormFiles.Count() == 0)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "No video has been uploaded....",
                };
            }
            int i = await _moduleRepository.GetLastElement() + 1;
            var fileNames = await _fileManagerService.GetListOfFileName(model.FormFiles, "uploads", "Videos", "Modules");
            var listOfModules = new List<Module>();
            foreach (var item in fileNames)
            {
                var module = new Module
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = model.Title,
                    Description = model.Description,
                    Resources = model.Resources,
                    Prerequisites = model.Prerequisites,
                    Objective = model.Objective,
                    ModuleDuration = model.ModuleDuration,
                    SequenceOfModule = i++,
                    VideoPath = item,
                    CourseId = model.CourseId,
                    CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    CreatedOn = DateTime.Now,
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

        public async Task<BaseResponse> Delete(string id)
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
            var deletedby = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

        public async Task<BaseResponse> Update(UpdateModuleRequestModel model)
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
            module.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _moduleRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "module updated successfully..",
                Status = true,
            };
        }

    }
}