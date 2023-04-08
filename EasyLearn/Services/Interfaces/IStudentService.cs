using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.StudentDTOs;
using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IStudentService
{
    Task<BaseResponse> StudentRegistration(CreateUserRequestModel model, string baseUrl);
    Task<BaseResponse> Delete(string id);
    Task<BaseResponse> UpdateProfile(UpdateStudentProfileRequestModel model);
    Task<BaseResponse> UpdateBankDetail(UpdateStudentBankDetailRequestModel model);
    Task<BaseResponse> UpdateAddress(UpdateStudentAddressRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateStudentPasswordRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateStudentActiveStatusRequestModel model);
    Task<StudentResponseModel> GetById(string id);
    Task<StudentResponseModel> GetFullDetailById(string id);
    Task<StudentResponseModel> GetByEmail(string email);
    Task<StudentsResponseModel> GetByName(string name);
    Task<StudentsResponseModel> GetAllStudent();
    Task<StudentsResponseModel> GetAllActive();
    Task<StudentsResponseModel> GetAllInActive();
}