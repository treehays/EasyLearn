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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ModuleService(IModuleRepository moduleRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _moduleRepository = moduleRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> Create(CreateModuleRequestModel model)
        {
            if (model.FormFiles == null)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "No video has been uploaded...",
                };
            }

            int i = 0;
            string fileRelativePathx = null;
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "videos");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var listOfModules = new List<Module>();
            foreach (var item in model.FormFiles)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(item.FileName);
                fileRelativePathx = "/uploads/videos/" + fileName;
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

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
                    VideoPath = fileRelativePathx,
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
            /*
                    var listOfModules1 = model.FormFiles.Select(async (x,index) => 
                    {
                       var fileName = Guid.NewGuid().ToString() + Path.GetFileName(x.FileName);
                    //fileRelativePathx = "/uploads/videos/" + fileName;
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await x.CopyToAsync(stream);
                    }

                            var module = new Module
                            {
                                Id = Guid.NewGuid().ToString(),
                            Title = model.Title,
                            Description = model.Description,
                            Resources = model.Resources,
                            Prerequisites = model.Prerequisites,
                            Objective = model.Objective,
                            ModuleDuration = model.ModuleDuration,
                            SequenceOfModule = index + 1,
                            VideoPath = "/uploads/videos/" + Guid.NewGuid().ToString() + Path.GetFileName(x.FileName),
                            CourseId = model.CourseId,
                            CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                            CreatedOn = DateTime.Now,
                        };
                        return module;
                    });
        */

            /*                await _moduleRepository.AddAsync(module);
                            await _moduleRepository.SaveChangesAsync();
            */

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
                Data = modules.Select(x => new ModuleDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Resources = x.Resources,
                    Prerequisites = x.Prerequisites,
                    Objective = x.Objective,
                    ModuleDuration = x.ModuleDuration,
                    SequenceOfModule = x.SequenceOfModule,
                    VideoPath = x.VideoPath,
                    CourseId = x.CourseId,
                }),
            };
            return modulesModel;
        }

        public async Task<ModuleResponseModel> GetByCourse(string courseId, string moduleId)
        {
            var module = await _moduleRepository.GetAsync(x => x.Id == courseId && x.CourseId == courseId && !x.IsDeleted);

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
                Data = new ModuleDTO
                {
                    Id = module.Id,
                    Title = module.Title,
                    Description = module.Description,
                    Resources = module.Resources,
                    Prerequisites = module.Prerequisites,
                    Objective = module.Objective,
                    ModuleDuration = module.ModuleDuration,
                    SequenceOfModule = module.SequenceOfModule,
                    VideoPath = module.VideoPath,
                    CourseId = module.CourseId,
                },
            };
            return modulModel;
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
                Data = new ModuleDTO
                {
                    Id = module.Id,
                    Title = module.Title,
                    Description = module.Description,
                    Resources = module.Resources,
                    Prerequisites = module.Prerequisites,
                    Objective = module.Objective,
                    ModuleDuration = module.ModuleDuration,
                    SequenceOfModule = module.SequenceOfModule,
                    VideoPath = module.VideoPath,
                    CourseId = module.CourseId,
                },
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
                Data = modules.Select(x => new ModuleDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Resources = x.Resources,
                    Prerequisites = x.Prerequisites,
                    Objective = x.Objective,
                    ModuleDuration = x.ModuleDuration,
                    SequenceOfModule = x.SequenceOfModule,
                    VideoPath = x.VideoPath,
                    CourseId = x.CourseId,
                }),
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

/*
public async Task<BaseResponse> Create(CreateModuleRequestModel model)
{
    if (model.FormFiles == null)
    {
        return new BaseResponse
        {
            Status = false,
            Message = "No video has been uploaded...",
        };
    }

    int i = 0;
    string fileRelativePathx = null;
    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "videos");
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
    }

    var listOfModules = model.FormFiles.Select(async x => await (AddModule(model, x)));
    var x = listOfModules;
    //await Task.WhenAll(listOfModules);
    await _moduleRepository.AddRangeAsync(x);
    return new BaseResponse
    {
        Status = true,
        Message = "video has been successfully uploaded...",
    };
}
public async Task<Module> AddModule(CreateModuleRequestModel model, IFormFile x)
{
    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "videos");
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
    }

    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(x.FileName);
    //fileRelativePathx = "/uploads/videos/" + fileName;
    var filePath = Path.Combine(uploadsFolder, fileName);
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await x.CopyToAsync(stream);
    }

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
        VideoPath = "/uploads/videos/" + Guid.NewGuid().ToString() + Path.GetFileName(x.FileName),
        CourseId = model.CourseId,
        CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        CreatedOn = DateTime.Now,
    };
    return module;
}*/