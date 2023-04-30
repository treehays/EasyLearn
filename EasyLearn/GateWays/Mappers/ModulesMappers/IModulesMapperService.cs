using EasyLearn.Models.DTOs.ModuleDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.ModulesMappers
{
    public interface IModulesMapperService
    {
        ModuleDTO ConvertToModuleResponseModel(Module module);
    }
}
