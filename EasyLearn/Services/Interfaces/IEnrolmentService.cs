using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EnrolmentDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IEnrolmentService
    {
        Task<CreateEnrolmentRequestModel> Create(CreateEnrolmentRequestModel model, string studentId, string userId);
        Task<BaseResponse> Delete(string id);
        Task<BaseResponse> Update(UpdateEnrolmentRequestModel model);
        Task<EnrolmentResponseModel> GetById(string id);
        Task<EnrolmentsResponseModel> GetAll();
        Task<EnrolmentsResponseModel> GetAllPaid();
        Task<EnrolmentsResponseModel> GetNotPaid();
        Task<BaseResponse> UpdateActiveStatus(UpdateEnrolmentRequestModel model);
        Task<EnrolmentsResponseModel> GetByName(string name);
    }
}
