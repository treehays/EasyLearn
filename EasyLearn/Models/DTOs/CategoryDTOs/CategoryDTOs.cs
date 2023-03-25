namespace EasyLearn.Models.DTOs.CategoryDTOs
{
    public class CategoryDTOs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImage { get; set; }
        public bool IsAvailable { get; set; }
        public string Id { get; set; }
    }

    public class CreateCategoryRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImage { get; set; }
        public IFormFile formFile { get; set; }

    }

    public class UpdateCategoryRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImage { get; set; }
    }

    public class CategoriesResponseModel : BaseResponse
    {

        public IEnumerable<CategoryDTOs> Data { get; set; }
    }


    public class CategoryResponseModel : BaseResponse
    {

        public CategoryDTOs Data { get; set; }
    }


}
