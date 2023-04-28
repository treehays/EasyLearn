using EasyLearn.Models.DTOs.ModulesDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.ModulesMappers
{
    public interface IModulesMapperService
    {
        ModuleDTO ConvertToModuleResponseModel(Module module);
    }
}
