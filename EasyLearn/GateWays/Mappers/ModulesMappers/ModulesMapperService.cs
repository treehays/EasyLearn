using EasyLearn.Models.DTOs.ModuleDTOs;
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
                //ModuleDuration = module.ModuleDuration.ToString("hh\\:mm\\:ss") $"{module.ModuleDuration.Hours:00}h",
                ModuleDuration = $"{module.ModuleDuration.Hours:00}h:{module.ModuleDuration.Minutes:00}m:{module.ModuleDuration.Seconds:00}s:",
                SequenceOfModule = module.SequenceOfModule,
                VideoPath = module.VideoPath,
                CourseId = module.CourseId,
                VideoSequence = module.VideoSequence,
            };
            return courseModel;
        }
    }
}
