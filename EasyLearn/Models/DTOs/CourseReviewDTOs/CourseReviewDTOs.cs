namespace EasyLearn.Models.DTOs.CourseReviewDTOs
{
    public class CourseReviewDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public bool IsReported { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public bool IsVisibile { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
    }

    public class CreateCourseReviewRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public string CourseId { get; set; }
        //public string StudentId { get; set; }
        //public bool IsReported { get; set; }
        //public bool IsVerifiedPurchase { get; set; }
        //public bool IsVisibile { get; set; }
    }

    public class UpdateCourseReviewRequestModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
    }


    public class CourseReviewsResponseModel : BaseResponse
    {
        public IEnumerable<CourseReviewDTO> Data { get; set; }
    }


    public class CourseReviewResponseModel : BaseResponse
    {
        public CourseReviewDTO Data { get; set; }
    }

}
