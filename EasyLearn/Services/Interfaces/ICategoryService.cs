using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CategoryDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse> Create(CreateCategoryRequestModel model);
        Task<BaseResponse> Delete(string id);
        Task<BaseResponse> UpdateCategory(UpdateCategoryRequestModel model);
        Task<CategoryResponseModel> GetById(string id);
        Task<CategoriesResponseModel> GetAll();

    }
}
