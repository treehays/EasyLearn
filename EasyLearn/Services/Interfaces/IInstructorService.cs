﻿using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.Services.Interfaces;

public interface IInstructorService
{
    Task<BaseResponse> InstructorRegistration(CreateUserRequestModel model, string baseUrl);
    Task<BaseResponse> Delete(string id);
    Task<InstructorsResponseModel> GetAll();
    Task<InstructorsResponseModel> GetAllActive();
    Task<InstructorsResponseModel> GetAllInActive();
    Task<InstructorResponseModel> GetById(string id);
    Task<InstructorResponseModel> GetByEmail(string email);
    Task<InstructorResponseModel> InstructorDetail(string instructorId);

    //Task<InstructorsResponseModel> GetByName(string name);
    Task<PaymentDetailRequestModel> GetByPaymentDetail(string id);
    Task<InstructorResponseModel> GetFullDetailById(string id);
    Task<PaymentsDetailRequestModel> GetListOfInstructorBankDetails(string userId);
    Task<InstructorsResponseModel> GetByName(string name);
    Task<IList<User>> PaginatedSample();
    Task<BaseResponse> UpdateBankDetail(UpdateInstructorBankDetailRequestModel model);
    Task<BaseResponse> UpdateProfile(UpdateInstructorProfileRequestModel model);
    //Task<BaseResponse> EmailVerification(string emailToken);

    Task<BaseResponse> UpdateAddress(UpdateInstructorAddressRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateInstructorPasswordRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateInstructorActiveStatusRequestModel model);
}