using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IInstructorService
{
    Task<BaseResponse> Create(CreateInstructorRequestModel model);
    Task<BaseResponse> Delete(string id);
    Task<BaseResponse> UpdateProfile(UpdateInstructorProfileRequestModel model);
    Task<BaseResponse> UpdateBankDetail(UpdateInstructorBankDetailRequestModel model);
    Task<BaseResponse> UpdateAddress(UpdateInstructorAddressRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateInstructorPasswordRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateInstructorActiveStatusRequestModel model);
    Task<InstructorResponseModel> GetById(string id);
    Task<InstructorResponseModel> GetFullDetailById(string id);
    Task<InstructorResponseModel> GetByEmail(string email);
    Task<InstructorsResponseModel> GetByName(string name);
    Task<InstructorsResponseModel> GetAll();
    Task<InstructorsResponseModel> GetAllActive();
    Task<InstructorsResponseModel> GetAllInActive();
}