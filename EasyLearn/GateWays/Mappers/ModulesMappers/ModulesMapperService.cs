using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.DTOs.ModulesDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.ModulesMappers
{
    public class ModulesMapperService : IModulesMapperService
    {
        public ModuleDTO ConvertToModuleResponseModel(Module module)
        {
            var courseModel = new ModuleDTO
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
            };
            return courseModel;
        }
    }
}
