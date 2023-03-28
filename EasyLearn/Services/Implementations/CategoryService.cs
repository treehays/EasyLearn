using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using Microsoft.VisualBasic;

namespace EasyLearn.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CategoryService(ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse> Create(CreateCategoryRequestModel model)
        {
            var existByName = await _categoryRepository.ExistByCategoryNameAsync(model.Name);
            if (existByName)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "Category already exist.",
                };
            }

            if (model.FormFile != null && model.FormFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(model.FormFile.FileName);
                model.CategoryImage = "/uploads/images/" + fileName;
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.FormFile.CopyToAsync(stream);
                }
            }



            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                CategoryImage = model.CategoryImage,
                IsAvailable = true,
            };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Status = true,
                Message = "Category created successfully....",
            };
        }

        public async Task<BaseResponse> Delete(string id)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "Category no found....",
                };
            }
            await _categoryRepository.DeleteAsync(category);
            return new BaseResponse
            {
                Status = true,
                Message = "Category deleted successfully....",
            };
        }

        public async Task<BaseResponse> UpdateCategory(UpdateCategoryRequestModel model)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == model.Id);
            if (category == null)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "Category no found....",
                };
            }

            category.Name = model.Name ?? category.Name;
            category.Description = model.Description ?? category.Description;
            category.CategoryImage = model.CategoryImage ?? category.CategoryImage;
            //await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Status = true,
                Message = "Category updated successfully....",
            };
        }

        public async Task<CategoryResponseModel> GetById(string id)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id && x.IsAvailable && !x.IsDeleted);
            if (category == null)
            {
                return new CategoryResponseModel
                {
                    Status = false,
                    Message = "Category no found....",
                };
            }

            var categoryModel = new CategoryResponseModel
            {
                Status = true,
                Message = "Category retrieved successfully...",
                Data = new CategoryDTOs
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CategoryImage = category.CategoryImage,
                }
            };
            return categoryModel;
        }

        public async Task<CategoriesResponseModel> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories == null)
            {
                return new CategoriesResponseModel
                {
                    Message = "Category not found..",
                    Status = false,
                };
            }

            var categoriesModel = new CategoriesResponseModel
            {
                Status = true,
                Message = "Categories successfully retrieved...",
                Data = categories.Select(x => new CategoryDTOs
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryImage = x.CategoryImage,
                    IsAvailable = x.IsAvailable,
                }),
            };
            return categoriesModel;
        }
    }
}
